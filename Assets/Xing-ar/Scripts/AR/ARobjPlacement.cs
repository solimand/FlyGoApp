using Google.Maps;
using Google.Maps.Coord;
using Google.Maps.Event;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(MapsService))]
public class ARobjPlacement : MonoBehaviour
{
    //LOGGER-----------
    private const string kTAG = "ARobjPlacement";
    private static ILogger mLogger = Debug.unityLogger;

    // Game Objects (use pulbic field for unity editor)-----------
    private GameObject VoidObject;
    public GameObject alberoMuscolosoObj; //obj to place 
    public static GameObject AlberoMuscoloso { get; set; }
    public GameObject ragnopalmaObj; //obj to place
    public static GameObject Ragnopalma { get; set; }

    /// <summary>
    /// All <see cref="GameObject"/>s to be moved when the world's Floating Origin is moved.
    /// </summary>
    /// <remarks>
    /// If this array is not set by calling <see cref="SetAdditionalGameObjects"/>, then this array
    /// is initialized with <see cref="Camera.main"/> during <see cref="Awake"/>. This is so, by
    /// default, the scene's <see cref="Camera"/> is moved when the Floating Origin is recentered,
    /// resulting in a seamless recentering of the world that should be invisible to the user.
    /// </remarks>
    private GameObject[] AdditionalGameObjects;

    // AR Classes-----------
    private ARRaycastManager _arRaymMn;
    //private ARAnchor anchor;
    private ARPlaneManager arpm;
    //private ARAnchorManager m_AnchorManager;
    //static List<ARRaycastHit> hits =new List<ARRaycastHit>();

    // LOCATION-----------
    private static LatLng originMapsPos;
    //private Vector3 fixedObjPos;
    public static MapsService MyMapsService { get; set; }
    private S2Geofence s2geo;
    public static string geoFenceCell;
    public static string GeoFencePrevCell { get; set; }
    private const int DESIRED_LVL = 19; // S2Cell precision level
    //private const string alberoCell19 = "477fd4ee07c";
    //private const float alberoLat = 44.483005f; private const float alberoLon = 11.375767f;
    //private const float alberoLat = 44.487467f; private const float alberoLon = 11.329513f;
    private const string ragnoPalmaCell19 = "477e2b3bd6c";

    //FLOATING ORIGIN-----------
    ///Distance in meters the Camera should move before the world's Floating Origin is reset
    public float FloatingOriginRange = 10f;
    //[Tooltip("Script for controlling Camera movement. Used to detect when the Camera has moved.")]
    //public CameraController CameraController;
    public Vector3 FloatingOrigin { get; private set; }
  

    // Start is called before the first frame update
    private void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");
        s2geo = new S2Geofence();
        GeoFencePrevCell = "";
        //Entry point Maps SDK and init initial position
        MyMapsService = GetComponent<MapsService>();
        //InitWorld();
    }

    private void Awake()
    {
        mLogger.Log(kTAG, "Awake");
        _arRaymMn = GetComponent<ARRaycastManager>();
        //m_AnchorManager = GetComponent<ARAnchorManager>();
        arpm = GetComponent<ARPlaneManager>();
        // Store the initial position of the Camera on the ground plane.
        FloatingOrigin = GetCameraPositionOnGroundPlane();
        //mLogger.Log(kTAG, $"My floating origin {FloatingOrigin}");

        // If no additional GameObjects have been set (to be moved when the world's Floating Origin is
        // recentered), set this array to be just Camera.main's GameObject. This is so that, by
        // default, the scene's Camera is moved when the world is recentered, resulting in a seamless
        // recentering of the world that should be invisible to the user.        
        if (AdditionalGameObjects == null)
        {
            AdditionalGameObjects = new[] { Camera.main.gameObject };
        }
    }

    // Update is called once per frame
    void Update()
    {
        geoFenceCell = s2geo.AmIinCellId(LocationService.Instance.latitude,
                    LocationService.Instance.longitude, DESIRED_LVL);

        if (geoFenceCell == "N")    //out of geofence
        {
            // TODO delete all objects           
            if (AlberoMuscoloso != null)
            {
                Destroy(AlberoMuscoloso);
                mLogger.Log(kTAG, $"obj {AlberoMuscoloso} destroyed");
            }
            if (Ragnopalma != null)
            {
                Destroy(Ragnopalma);
                mLogger.Log(kTAG, $"obj {Ragnopalma} destroyed");
            }
            //mLogger.Log(kTAG, $"No target S2Cell id {geoFenceCell}");
            return;
        }
        else  //In cellID
        {
            // TODO delete objects not belonging to geofence
            switch (geoFenceCell)
            {
                case StaticLocations.alberoCell19:
                    if (AlberoMuscoloso != null)
                    {
                        //I am in the same previous geofence and the related obj exists
                        //mLogger.Log(kTAG, $" DBG I am in same S2Cell id {geoFenceCell} and obj is not null {Medusa}");
                        return;
                    }
                    else
                    {
                        //GeoFencePrevCell = geoFenceCell;
                        if (AlberoMuscoloso == null)
                        {
                            //mLogger.Log(kTAG, $" DBG I am in new valid S2Cell id {geoFenceCell}");
                            //AlberoMuscoloso = InstantiateAt(this.arpm, this.alberoMuscolosoObj, 2);
                            AlberoMuscoloso = InstantiateAtGPS(alberoMuscolosoObj,
                                StaticLocations.alberoLat, StaticLocations.alberoLon);
                        }
                    }
                    break;

                case ragnoPalmaCell19:
                    if (Ragnopalma != null)
                    {
                        //I am in the same previous geofence and the related obj exists
                        //mLogger.Log(kTAG, $" DBG I am in same S2Cell id {geoFenceCell} and obj is not null {Medusa}");
                        return;
                    }
                    else
                    {
                        //GeoFencePrevCell = geoFenceCell;
                        if (Ragnopalma == null)
                        {
                            //mLogger.Log(kTAG, $" DBG I am in new valid S2Cell id {geoFenceCell}");
                            Ragnopalma = InstantiateAt(this.arpm, this.ragnopalmaObj, 10);
                        }
                    }
                    break;
                default:
                    mLogger.Log(kTAG, "ERROR Nothing to instantiate");
                    break;
            }
        }        
    }

    private GameObject InstantiateAtGPS(GameObject objRef, float lat, float lon)
    {
        // reset maps origin before gps instantiation
        if (LocationService.Instance.latitude == 0 || LocationService.Instance.longitude == 0) //not set yet
            return null;
        MyMapsService.InitFloatingOrigin(new LatLng(LocationService.Instance.latitude, 
            LocationService.Instance.longitude));
        mLogger.Log(kTAG, "My latlng floating origin updated");

        // convert coordinates
        LatLng worldPos = new LatLng(lat, lon);
        Vector3 unityPos = MyMapsService.Projection.FromLatLngToVector3(worldPos);
        unityPos -= new Vector3(0, 0.5f, 0);
        GameObject result = Instantiate(objRef, unityPos, transform.rotation * Quaternion.identity) 
            as GameObject;

        if (result.GetComponent<ARAnchor>() == null)
        {
            result.AddComponent<ARAnchor>();
        }
        mLogger.Log(kTAG, $"Obj {objRef} placed at {unityPos}" +
            $" with anchor {result.GetComponent<ARAnchor>()}");
        
        AudioSource audioSource = result.GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.Play(0);
        mLogger.Log(kTAG, $"Audio Started with rolloff mode  {audioSource.rolloffMode}" +
            $" maxdist {audioSource.maxDistance} and mindist {audioSource.minDistance} ");

        return result;
    }

    //TODO altitude from ground + position related to user movement
    private GameObject InstantiateAt(ARPlaneManager arpm, GameObject objRef, int meters)
    {
        GameObject result;
        if (arpm)
        {
            // TODO fix altitude
            Vector3 forward = transform.TransformDirection(Vector3.forward) * meters;
            forward -= new Vector3(0, 0.5f, 0);
          
            //detect plane to trigger the placement
            if (arpm.trackables.count == 0)
                return null;
            //mLogger.Log(kTAG, $"planes {arpm.trackables.count}");
            foreach (var plane in arpm.trackables)
            {
                //mLogger.Log(kTAG, "AAA");
                break;
            }
            //place object at X meters
            result = Instantiate(objRef, forward, 
                transform.rotation * Quaternion.identity) as GameObject;
            // Add an ARAnchor component if it doesn't have one already
            if (result.GetComponent<ARAnchor>() == null)
            {
                result.AddComponent<ARAnchor>();
            }
            mLogger.Log(kTAG, $"Obj {objRef} placed at {forward}" +
                $" with anchor {result.GetComponent<ARAnchor>()}");

            // TODO FIX spatial audio            
            AudioSource audioSource = result.GetComponent<AudioSource>();
            if (audioSource != null)
                audioSource.Play(0);
            mLogger.Log(kTAG, $"Audio Started with rolloff mode  {audioSource.rolloffMode}" +
                $" maxdist {audioSource.maxDistance} and mindist {audioSource.minDistance} ");
            
            return result;
        }
        else
        {
            mLogger.Log(kTAG, "ARPlanemanager problems");
            return null;
        }
    }
   
    public void SetAdditionalGameObjects(ICollection<GameObject> objects)
    {
        // Check to see if the main Camera's GameObject is already a part of this given set of
        // GameObjects, adding it if not and storing as the array of GameObjects to move when the
        // world's Floating Origin is recentered.
        GameObject cameraGameObject = Camera.main.gameObject;
        List<GameObject> objectList = new List<GameObject>(objects);

        if (!objects.Contains(cameraGameObject))
        {
            objectList.Add(cameraGameObject);
        }

        AdditionalGameObjects = objectList.ToArray();
    }

    private Vector3 GetCameraPositionOnGroundPlane()
    {
        Vector3 result = Camera.main.transform.position;
        // Ignore the Y value since the floating origin only really makes sense on the ground plane.
        result.y = 0;
        return result;
    }

    private bool TryMoveFloatingOrigin(/*Vector3 moveAmount*/)
    {
        Vector3 newFloatingOrigin = GetCameraPositionOnGroundPlane();
        float distance = Vector3.Distance(FloatingOrigin, newFloatingOrigin);

        // Reset the world's Floating Origin if (and only if) the Camera has moved far enough.
        if (distance < FloatingOriginRange) //if (distance < 2.0)
        {
            return false;
        }
        // The Camera's current position is given to MapsService's MoveFloatingOrigin function,
        // along with any GameObjects to move along with the world (which will at least be the the
        // Camera itself). This is so that the world, the Camera, and any extra GameObjects can all be
        // moved together, until the Camera is over the origin again. Note that the MoveFloatingOrigin
        // function automatically moves all geometry loaded by the Maps Service.
        Vector3 originOffset =
            MyMapsService.MoveFloatingOrigin(newFloatingOrigin, AdditionalGameObjects);
        // Set the new Camera origin. This ensures that we can accurately tell when the Camera has
        // moved away from this new origin, and the world needs to be recentered again.
        FloatingOrigin = newFloatingOrigin;

        // Optionally print a debug message, saying how much the Floating Origin was moved by.
        Debug.Log($"Floating Origin moved: world moved by {originOffset}, " +
            $"with distance {distance} and range {FloatingOriginRange}");

        return true;
    }

    bool GetPos(out Vector2 touchPos)
    {
        if (Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }
        touchPos = default;
        return false;
    }

}
