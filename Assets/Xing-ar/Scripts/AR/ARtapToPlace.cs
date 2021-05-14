using Google.Maps;
using Google.Maps.Coord;
using Google.Maps.Event;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[RequireComponent(typeof(MapsService))]
public class ARtapToPlace : MonoBehaviour
{
    //LOGGER
    private const string kTAG = "ARtapToPlace";
    private static ILogger mLogger = Debug.unityLogger;

    //Playables GOs
        // public field for using object in unity scene
    public GameObject goToPlace; //obj to place with tap
    //public GameObject goPOI; //obj to place with pos
    //private GameObject _fixedGo;

    private ARRaycastManager _arRaymMn;
    static List<ARRaycastHit> hits =new List<ARRaycastHit>();
    //private static LatLng testpos;
    //private Vector3 fixedObjPos;
    private MapsService mapsService;

    public static GameObject MyGo { get; set; }

    /*
    // TEST Static Locations
    private static double testLat = 44.482657;
    private static double testLon = 11.375136;
    // TEST Geofencing HOME
    private static double minLat = 44.48217611761878;
    private static double maxLat = 44.48343912715564;
    private static double minLon = 11.37425397971663;
    private static double maxLon = 11.375975957904192;
    // TEST Geofencing !HOME
    //private static double minLat = 44.48261626040989;
    //private static double maxLat = 44.48420457417349;
    //private static double minLon = 11.371915093455645;
    //private static double maxLon = 11.373857012782427;
    */

    // Start is called before the first frame update
    private void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");

        //goToPlace = Resources.Load<GameObject>("zombieChar");        

        //testpos = new LatLng(testLat, testLon);

        //Entry point Maps SDK and init initial position
        mapsService = GetComponent<MapsService>();
        
        // Register a listener to be notified when the map is loaded
        //mapsService.Events.MapEvents.Loaded.AddListener(OnLoaded);
        //mLogger.Log(kTAG, $"FloatingOrigin set to lat = {initPos.Lat.ToString()} " +
        //  $"and lon {initPos.Lng.ToString()}");

        // Load map with default options
        //mapsService.LoadMap(ExampleDefaults.DefaultBounds, ExampleDefaults.DefaultGameObjectOptions);

    }

    /*
    public void OnLoaded(MapLoadedArgs args)
    {
        // The Map is loaded - you can start/resume gameplay from that point.
        // The new geometry is added under the GameObject that has MapsService as a component.
    }
    */

    private void Awake()
    {
        mLogger.Log(kTAG, "Awake");
        _arRaymMn = GetComponent<ARRaycastManager>();
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
        if (!GetPos(out Vector2 touchPos))
            return;

        if (_arRaymMn.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            //var uiClicked = false;

            // TODO FIX avoid Detect Clicks Through UI 
                // a click takes 3-4 frames and Update() is executed once per frame
            /*
            if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                mLogger.Log(kTAG, "EvSys detected a click on UI");
                uiClicked = true;
            }
            */

            if (MyGo == null)// && !uiClicked)
            {
                //create object on touch
                //_myGo = Instantiate(goToPlace, hitPose.position, hitPose.rotation) as GameObject;
                MyGo = Instantiate(goToPlace, hitPose.position, 
                    transform.rotation * Quaternion.Euler(0f, 180f, 0f)) as GameObject;
                mLogger.Log(kTAG, $"Obj {goToPlace} placed at {hitPose.position}");

                // Add an ARAnchor component if it doesn't have one already.
                if (MyGo.GetComponent<ARAnchor>() == null)
                {
                    MyGo.AddComponent<ARAnchor>();
                }
                mLogger.Log(kTAG, $"My AR Anchor {MyGo.GetComponent<ARAnchor>()}");

                // TODO FIX spatial audio
                AudioSource audioSource = MyGo.GetComponent<AudioSource>();
                if (audioSource != null)
                    audioSource.Play(0);
                mLogger.Log(kTAG, $"Audio Started with rolloff mode  {audioSource.rolloffMode}" +
                    $" maxdist {audioSource.maxDistance} and mindist {audioSource.minDistance} ");
            }
            else// if (!uiClicked)
            {                
                //update object position and roation on touch
                // TODO FIX rotation in front of camera and normal to ground
                MyGo.transform.position = hitPose.position;
                MyGo.transform.LookAt(Camera.main.transform, transform.up);
                
                // If animation -- restart it
                if (MyGo.GetComponent<Animator>() != null)
                    MyGo.GetComponent<Animator>().Play("Run", -1, 0);

                mLogger.Log(kTAG, $"Obj {goToPlace} placed at updated pos {hitPose.position}");
            }

            #region geolocation test
            /*if (_fixedGo == null)
            {
                //setting init floating origin (only first time)
                if (!mapsService.Projection.IsFloatingOriginSet)
                {
                    mLogger.Log(kTAG, $"My latlng pos {LocationService.Instance.CurrPos}");
                    mapsService.InitFloatingOrigin(LocationService.Instance.CurrPos);
                }

                // TODO improve geofencing
                if ((LocationService.Instance.latitude > minLat) &&
                        (LocationService.Instance.latitude < maxLat) &&
                        (LocationService.Instance.longitude > minLon) &&
                        (LocationService.Instance.longitude < maxLon)) {
                    mLogger.Log(kTAG, "Congratualtions!! You are in the right zone");
                    fixedObjPos = mapsService.Projection.FromLatLngToVector3(testpos);

                    // TODO adjust altitude and rotation
                    _fixedGo = Instantiate(goPOI, fixedObjPos, Quaternion.identity) as GameObject;
                    //adjust the rotation
                    //_fixedGo.transform.Rotate(0f, 180f, 0f);

                    mLogger.Log(kTAG, $"Obj 2 placed at {fixedObjPos}");
                }
                else
                {
                    mLogger.Log(kTAG, "Sorry, no ARt here...");
                }

            }
            else
            {
                // TODO I should not update the position but the rotation
                _fixedGo.transform.position = fixedObjPos;
            }*/
            #endregion
        }

    }
}
