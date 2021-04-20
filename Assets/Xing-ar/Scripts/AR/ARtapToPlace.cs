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

    public GameObject goToPlace; //obj to place with tap
    public GameObject goPOI; //obj to place with tap

    private GameObject _myGo;
    private GameObject _fixedGo;
    private ARRaycastManager _arRaymMn;
    static List<ARRaycastHit> hits =new List<ARRaycastHit>();
    private static LatLng testpos;
    private Vector3 fixedObjPos;
    private MapsService mapsService;
    
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

    // Start is called before the first frame update
    private void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");

        testpos = new LatLng(testLat, testLon);

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

            if (_myGo == null)
            {
                //create object on touch
                _myGo = Instantiate(goToPlace, hitPose.position, hitPose.rotation) as GameObject;
                mLogger.Log(kTAG, $"Obj placed at {hitPose.position}");
            }
            else
            {
                //update object position on touch
                _myGo.transform.position = hitPose.position;
            }
            if (_fixedGo == null)
            {
                // TODO test altitude of device
                //_fixedGo = Instantiate(goPOI, LocationService.Instance.ucsTest, Quaternion.identity) as GameObject;
                //_fixedGo = Instantiate(goPOI, projctn.FromLatLngToVector3(initPos),Quaternion.identity) as GameObject;

                //setting init floating origin (only first time)
                if (!mapsService.Projection.IsFloatingOriginSet)
                {
                    mLogger.Log(kTAG, $"My latlng pos {LocationService.Instance.CurrPos}");
                    mapsService.InitFloatingOrigin(LocationService.Instance.CurrPos);
                }

                // TODO geofencing
                if ((LocationService.Instance.latitude > minLat) &&
                        (LocationService.Instance.latitude < maxLat) &&
                        (LocationService.Instance.longitude > minLon) &&
                        (LocationService.Instance.longitude < maxLon)) {
                    mLogger.Log(kTAG, "Congratualtions!! You are in the right zone");
                    fixedObjPos = mapsService.Projection.FromLatLngToVector3(testpos);

                    // TODO adjust altitude of instantiate
                    _fixedGo = Instantiate(goPOI, fixedObjPos, Quaternion.identity) as GameObject;
                    //adjust the rotation
                    _fixedGo.transform.Rotate(0f, 180f, 0f);

                    mLogger.Log(kTAG, $"Obj 2 placed at {fixedObjPos}");
                }
                else
                {
                    mLogger.Log(kTAG, "Sorry, no ARt here...");
                }

            }
            else
            {
                // TODO I should not update the position
                _fixedGo.transform.position = fixedObjPos;
            }
        }
                
    }
}
