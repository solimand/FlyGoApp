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
    //LOGGER-----------
    private const string kTAG = "ARtapToPlace";
    private static ILogger mLogger = Debug.unityLogger;

    // Game Objects (use pulbic field for unity editor)-----------
    public GameObject goToPlace; //obj to place with tap
    //public GameObject goPOI; //obj to place with pos
    //private GameObject _fixedGo;
    public static GameObject MyGo { get; set; }
    //Anchor prefab-----------
    [SerializeField]
    GameObject m_Prefab;
    public GameObject prefab { get; set; }
    /*{
        get => m_Prefab;
        set => m_Prefab = value;
    }*/

    // AR Classes-----------
    private ARRaycastManager _arRaymMn;
    private ARAnchor anchor;
    private ARPlaneManager arpm;
    private ARAnchorManager m_AnchorManager;
    static List<ARRaycastHit> hits =new List<ARRaycastHit>();

    // LOCATION-----------
    //private static LatLng testpos;
    //private Vector3 fixedObjPos;
    private MapsService mapsService;
    private S2Geofence s2geo;

    

    // Start is called before the first frame update
    private void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");     

        //testpos = new LatLng(testLat, testLon);

        //Entry point Maps SDK and init initial position
        mapsService = GetComponent<MapsService>();

        // Register a listener to be notified when the map is loaded
        //mapsService.Events.MapEvents.Loaded.AddListener(OnLoaded);
        //mLogger.Log(kTAG, $"FloatingOrigin set to lat = {initPos.Lat.ToString()} " +
        //  $"and lon {initPos.Lng.ToString()}");
        // Load map with default options
        //mapsService.LoadMap(ExampleDefaults.DefaultBounds, ExampleDefaults.DefaultGameObjectOptions);

        s2geo = new S2Geofence();
        anchor = null;        
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
        m_AnchorManager = GetComponent<ARAnchorManager>();
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
        //render object at 2 meters if I am in cellID
        //if (s2geo.AmIinCellId(LocationService.Instance.latitude,
        //            LocationService.Instance.longitude))
        //{            
            arpm = GetComponent<ARPlaneManager>();
        //mLogger.Log(kTAG, "ARPlaneManager created");

        if (arpm)
        {
            if (anchor == null) 
            { 
                mLogger.Log(kTAG, "Creating anchor attachment");
                var oldPrefab = m_AnchorManager.anchorPrefab;
                m_AnchorManager.anchorPrefab = prefab;
                // create a ray (vector 2 meters from camera) and caputre (raycast) intersection with plane
                var ray = Camera.main.ScreenPointToRay(new Vector3(200, 200, 0));
                mLogger.Log(kTAG, "CCC");
                if (_arRaymMn.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
                {
                    mLogger.Log(kTAG, $"My Ray {ray} hitted the plane at {hits[0].pose}");
                    foreach (var plane in arpm.trackables)
                    {
                        anchor = m_AnchorManager.AttachAnchor(plane, hits[0].pose);
                        break;
                    }
                    m_AnchorManager.anchorPrefab = oldPrefab;
                    mLogger.Log(kTAG, $"anchor created {anchor}");
                }
            }
        }
        else
        {
            mLogger.Log(kTAG, "Sorry ARPlanemanager problems");
        }
        //}

        /*
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
        /*
            if (MyGo == null)// && !uiClicked)
            {
                //check geofence s2 cell id               
                s2geo.AmIinGeofence(LocationService.Instance.latitude,
                    LocationService.Instance.longitude);
                
                //mLogger.Log(kTAG, $"I am in S2 Cell id {currCellID}");

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
                /*
                AudioSource audioSource = MyGo.GetComponent<AudioSource>();
                if (audioSource != null)
                    audioSource.Play(0);
                mLogger.Log(kTAG, $"Audio Started with rolloff mode  {audioSource.rolloffMode}" +
                    $" maxdist {audioSource.maxDistance} and mindist {audioSource.minDistance} ");
                */
     /*       }
    
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
                    //Instantiate(goToPlace, hitPose.position,transform.rotation * Quaternion.Euler(0f, 180f, 0f)) as GameObject;

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


        //}

    }
}
