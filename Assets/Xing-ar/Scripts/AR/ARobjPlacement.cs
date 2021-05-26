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
    public GameObject medusaObject; //obj to place 
    public static GameObject Medusa { get; set; }
    public GameObject chimera1Object; //obj to place
    public static GameObject Chimera1 { get; set; }

    //public GameObject goPOI; //obj to place with pos
    //private GameObject _fixedGo;

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

    //Anchor prefab-----------
    //[SerializeField]
    //GameObject m_Prefab;
    //public GameObject prefab { get; set; }

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
    private string geoFenceCell;
    public static string GeoFencePrevCell { get; set; }
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
    }


    private void Awake()
    {
        mLogger.Log(kTAG, "Awake");
        _arRaymMn = GetComponent<ARRaycastManager>();
        //m_AnchorManager = GetComponent<ARAnchorManager>();
        arpm = GetComponent<ARPlaneManager>();
        // Store the initial position of the Camera on the ground plane.
        FloatingOrigin = GetCameraPositionOnGroundPlane();
        mLogger.Log(kTAG, $"My floating origin {FloatingOrigin}");

        // If no additional GameObjects have been set (to be moved when the world's Floating Origin is
        // recentered), set this array to be just Camera.main's GameObject. This is so that, by
        // default, the scene's Camera is moved when the world is recentered, resulting in a seamless
        // recentering of the world that should be invisible to the user.        
        if (AdditionalGameObjects == null)
        {
            AdditionalGameObjects = new[] { Camera.main.gameObject };
        }
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

    private Vector3 GetCameraPositionOnGroundPlane()
    {
        Vector3 result = Camera.main.transform.position;
        // Ignore the Y value since the floating origin only really makes sense on the ground plane.
        result.y = 0;
        return result;
    }

    bool GetPos(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }
        touchPos = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        //setting init floating origin (only first time)
        if (!MyMapsService.Projection.IsFloatingOriginSet)
        {
            originMapsPos = LocationService.Instance.CurrPos;
            MyMapsService.InitFloatingOrigin(originMapsPos);
            mLogger.Log(kTAG, $"My latlng floating origin {originMapsPos}");
        }

        //Exit if user does not move (for first time, I have to move!)
        //if (!TryMoveFloatingOrigin()) // TODO OR object already created...
        //return;
        //mLogger.Log(kTAG, "user is moving");

        // TODO different object for different geoFenceCell
        geoFenceCell = s2geo.AmIinCellId(LocationService.Instance.latitude,
                    LocationService.Instance.longitude);

        if (geoFenceCell == "N")                    //out of geofence
        {
            // TODO delete objects not belonging to geofence 
            //mLogger.Log(kTAG, $"No target S2Cell id {geoFenceCell}");
            return;
        }
        else
        {
            if ((geoFenceCell == GeoFencePrevCell) && (Medusa != null))
            {
                //I am in the same previous geofence and the related obj exists
                //mLogger.Log(kTAG, $" DBG I am in same S2Cell id {geoFenceCell} and obj is not null {Medusa}");
                return;
            }
            else        //I am in new geofence->create new obj
            {
                GeoFencePrevCell = geoFenceCell;
                if (Medusa == null)
                {
                    //mLogger.Log(kTAG, $" DBG I am in new valid S2Cell id {geoFenceCell}");
                    //TODO make instantiation a method
                    Medusa = InstantiateAtTwoMt(this.arpm, this.medusaObject);                    
                }
            }
        }
    }

    GameObject InstantiateAtTwoMt(ARPlaneManager arpm, GameObject objRef)
    {
        GameObject result;
        if (arpm)
        {
            // TODO fix altitude
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 2;
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
            //place object at two meters //OR firstPlane.center
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
}
