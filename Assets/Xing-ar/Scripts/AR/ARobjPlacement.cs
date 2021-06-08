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
    public GameObject alberoMuscolosoObj; //obj to place 
    public static GameObject AlberoMuscoloso { get; set; }
    public GameObject ragnopalmaObj;
    public static GameObject Ragnopalma { get; set; }
    public GameObject arciereObj;
    public static GameObject Arciere { get; set; }
    public GameObject chimeraObj;
    public static GameObject Chimera { get; set; }
    public GameObject chimera2Obj;
    public static GameObject Chimera2 { get; set; }
    public GameObject chimera3Obj;
    public static GameObject Chimera3 { get; set; }
    public GameObject scagliaPietraObj;
    public static GameObject ScagliaPietra { get; set; }
    public GameObject scagliaPietra2Obj;
    public static GameObject ScagliaPietra2 { get; set; }
    public GameObject pietreForateObj;
    public static GameObject PietreForate { get; set; }
    public GameObject medusaObj;
    public static GameObject Medusa { get; set; }
    public GameObject spiritoFuocoObj;
    public static GameObject SpiritoFuoco { get; set; }
    public GameObject serpentePietraObj;
    public static GameObject SerpentePietra { get; set; }
    public GameObject buddhaObj;
    public static GameObject Buddha { get; set; }

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
    public static MapsService MyMapsService { get; set; }
    private S2Geofence s2geo;
    public static string geoFenceCell;
    private const int DESIRED_LVL = 18; // S2Cell precision level

    //FLOATING ORIGIN-----------
    ///Distance in meters the Camera should move before the world's Floating Origin is reset
    public float FloatingOriginRange = 2f;
    //[Tooltip("Script for controlling Camera movement. Used to detect when the Camera has moved.")]
    //public CameraController CameraController;
    public Vector3 FloatingOrigin { get; private set; }

    int frameCounter;

    // Start is called before the first frame update
    private void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");
        s2geo = new S2Geofence();
        //Entry point Maps SDK and init initial position
        MyMapsService = GetComponent<MapsService>();
        frameCounter = 0;
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
        if (frameCounter < 60) //slow down, do nothing for 60 frames
        {
            frameCounter++;
            return;
        }

        /*mLogger.Log(kTAG, $"My current location lat {LocationService.Instance.latitude}" +
            $" lon {LocationService.Instance.longitude}" +
            $" S2cell {s2geo.CellIdFromCoord(LocationService.Instance.latitude, LocationService.Instance.longitude,19)}");*/

        geoFenceCell = s2geo.AmIinCellId(LocationService.Instance.latitude,
                    LocationService.Instance.longitude, DESIRED_LVL);

        if (geoFenceCell == "N")    //out of geofence
        {            
            DestroyAllObj();
            return;
        }
        else  //In cellID
        {
            switch (geoFenceCell)
            {
                // TODO update rotation in case of Front==true
                case StaticLocations.alberoCell18:
                    if (AlberoMuscoloso == null)
                    {
                        //enabled = false;
                        //enabled = true;
                        //TODO otherwise maps=null - s2geo=null and reInstantiate
                        
                        AlberoMuscoloso = InstantiateAt(this.arpm, this.alberoMuscolosoObj, 2,2f);
                        //AlberoMuscoloso = InstantiateAtGPS(alberoMuscolosoObj,
                            //StaticLocations.alberoLat, StaticLocations.alberoLon, StaticLocations.alberoAlt);
                        DestroyAllObjExceptOne(AlberoMuscoloso);
                    }
                    break;

                case StaticLocations.ragnoCell18:
                    if (Ragnopalma == null)
                    {
                        //enabled = false;
                        //enabled = true;

                        //Ragnopalma = InstantiateAt(this.arpm, this.ragnopalmaObj, StaticLocations.ragnoDist, StaticLocations.ragnoAlt);
                        Ragnopalma = InstantiateAt(this.arpm, this.ragnopalmaObj, 2, 2f);
                        DestroyAllObjExceptOne(Ragnopalma);
                    }
                    break;
                case StaticLocations.AlberoMuscolosoCell19:
                    break;
                case StaticLocations.ArciereCell19:
                    break;
                case StaticLocations.ChimeraCell19:
                    break;
                case StaticLocations.Chimera2Cell19:
                    break;
                case StaticLocations.Chimera3Cell19:
                    break;
                case StaticLocations.ScagliaPietraCell19:
                    break;
                case StaticLocations.ScagliaPietra2Cell19:
                    break;
                case StaticLocations.PietreForateCell19:
                    break;
                case StaticLocations.MedusaCell19:
                    break;
                case StaticLocations.SpiritoFuocoCell19:
                    break;
                case StaticLocations.SerpentePietraCell19:
                    break;
                case StaticLocations.BuddhaCell19:
                    break;
                default:
                    mLogger.Log(kTAG, "ERROR Nothing to instantiate");
                    break;
            }
        }        
    }

    private void DestroyAllObjExceptOne(GameObject goToSave)
    {
        if (AlberoMuscoloso != null && AlberoMuscoloso != goToSave)
        {
            Destroy(AlberoMuscoloso);
            mLogger.Log(kTAG, $"obj {AlberoMuscoloso} destroyed");
        }
        if (Ragnopalma != null && Ragnopalma != goToSave)
        {
            Destroy(Ragnopalma);
            mLogger.Log(kTAG, $"obj {Ragnopalma} destroyed");
        }
        if (SerpentePietra != null && SerpentePietra != goToSave)
        {
            Destroy(SerpentePietra);
            mLogger.Log(kTAG, $"obj {SerpentePietra} destroyed");
        }
        if (Buddha != null && Buddha != goToSave)
        {
            Destroy(Buddha);
            mLogger.Log(kTAG, $"obj {Buddha} destroyed");
        }
        if (Chimera != null && Chimera != goToSave)
        {
            Destroy(Chimera);
            mLogger.Log(kTAG, $"obj {Chimera} destroyed");
        }
        if (Chimera2 != null && Chimera2 != goToSave)
        {
            Destroy(Chimera2);
            mLogger.Log(kTAG, $"obj {Chimera2} destroyed");
        }
        if (Chimera3 != null && Chimera3 != goToSave)
        {
            Destroy(Chimera3);
            mLogger.Log(kTAG, $"obj {Chimera3} destroyed");
        }
        if (SpiritoFuoco != null && SpiritoFuoco != goToSave)
        {
            Destroy(SpiritoFuoco);
            mLogger.Log(kTAG, $"obj {SpiritoFuoco} destroyed");
        }
        if (Medusa != null && Medusa != goToSave)
        {
            Destroy(Medusa);
            mLogger.Log(kTAG, $"obj {Medusa} destroyed");
        }
        if (ScagliaPietra != null && ScagliaPietra != goToSave)
        {
            Destroy(ScagliaPietra);
            mLogger.Log(kTAG, $"obj {ScagliaPietra} destroyed");
        }
        if (ScagliaPietra2 != null && ScagliaPietra2 != goToSave)
        {
            Destroy(ScagliaPietra2);
            mLogger.Log(kTAG, $"obj {ScagliaPietra2} destroyed");
        }
        if (PietreForate != null && PietreForate != goToSave)
        {
            Destroy(PietreForate);
            mLogger.Log(kTAG, $"obj {PietreForate} destroyed");
        }
        if (Arciere != null && Arciere != goToSave)
        {
            Destroy(Arciere);
            mLogger.Log(kTAG, $"obj {Arciere} destroyed");
        }
    }

    public static void DestroyAllObj()
    {
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
        if (SerpentePietra != null)
        {
            Destroy(SerpentePietra);
            mLogger.Log(kTAG, $"obj {SerpentePietra} destroyed");
        }
        if (Buddha != null)
        {
            Destroy(Buddha);
            mLogger.Log(kTAG, $"obj {Buddha} destroyed");
        }
        if (Chimera != null)
        {
            Destroy(Chimera);
            mLogger.Log(kTAG, $"obj {Chimera} destroyed");
        }
        if (Chimera2 != null)
        {
            Destroy(Chimera2);
            mLogger.Log(kTAG, $"obj {Chimera2} destroyed");
        }
        if (Chimera3 != null)
        {
            Destroy(Chimera3);
            mLogger.Log(kTAG, $"obj {Chimera3} destroyed");
        }
        if (SpiritoFuoco != null)
        {
            Destroy(SpiritoFuoco);
            mLogger.Log(kTAG, $"obj {SpiritoFuoco} destroyed");
        }
        if (Medusa != null)
        {
            Destroy(Medusa);
            mLogger.Log(kTAG, $"obj {Medusa} destroyed");
        }
        if (ScagliaPietra != null)
        {
            Destroy(ScagliaPietra);
            mLogger.Log(kTAG, $"obj {ScagliaPietra} destroyed");
        }
        if (ScagliaPietra2 != null)
        {
            Destroy(ScagliaPietra2);
            mLogger.Log(kTAG, $"obj {ScagliaPietra2} destroyed");
        }
        if (PietreForate != null)
        {
            Destroy(PietreForate);
            mLogger.Log(kTAG, $"obj {PietreForate} destroyed");
        }
        if (Arciere != null)
        {
            Destroy(Arciere);
            mLogger.Log(kTAG, $"obj {Arciere} destroyed");
        }
    }        
    private GameObject InstantiateAtGPS(GameObject objRef, float lat, float lon, float alt)
    {
        // reset maps origin before gps instantiation
        if (LocationService.Instance.latitude == 0 || LocationService.Instance.longitude == 0) //not set yet
            return null;

        // set float origin 
        if (!MyMapsService.Projection.IsFloatingOriginSet)
        {
            MyMapsService.InitFloatingOrigin(new LatLng(LocationService.Instance.latitude,
            LocationService.Instance.longitude));
        }
        else
        {
            TryMoveFloatingOrigin();
        }
        //mLogger.Log(kTAG, "My latlng floating origin updated");

        // convert coordinates
        LatLng worldPos = new LatLng(lat, lon);
        Vector3 unityPos = MyMapsService.Projection.FromLatLngToVector3(worldPos);
        //unityPos -= new Vector3(0, 0.5f, 0);
        unityPos += new Vector3(0, alt, 0);
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

    private GameObject InstantiateAt(ARPlaneManager arpm, GameObject objRef, int meters, float altitude)
    {
        
        // reset maps origin before gps instantiation
        if (LocationService.Instance.latitude == 0 || LocationService.Instance.longitude == 0) //not set yet
            return null;

        // set float origin 
        if (!MyMapsService.Projection.IsFloatingOriginSet)
        {
            MyMapsService.InitFloatingOrigin(new LatLng(LocationService.Instance.latitude,
                LocationService.Instance.longitude));
        }
        else
        {
            TryMoveFloatingOrigin();
        }
        
        
        GameObject result;
        if (arpm)
        {
            // TODO fix altitude
            Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward) * meters;
            //forward -= new Vector3(0, 1f, 0);
            forward += new Vector3(0, 1f, 0);
            //forward += new Vector3(0, altitude, 0); 
          
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
                //Quaternion.identity) as GameObject;
                //result.transform.LookAt(Camera.main.transform);
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
        //if (distance < FloatingOriginRange) 
        if (distance < 2.0)
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
            //MyMapsService.MoveFloatingOrigin(new LatLng(LocationService.Instance.latitude,
                //LocationService.Instance.longitude), AdditionalGameObjects);
        // Set the new Camera origin. This ensures that we can accurately tell when the Camera has
        // moved away from this new origin, and the world needs to be recentered again.
        FloatingOrigin = newFloatingOrigin;

        // Optionally print a debug message, saying how much the Floating Origin was moved by.
        mLogger.Log(kTAG, $"Floating Origin moved: world moved by {originOffset}, " +
            $"with distance {distance} and range {FloatingOriginRange}");

        return true;
    }
    
}
