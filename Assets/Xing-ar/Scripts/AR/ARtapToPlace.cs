using Google.Maps;
using Google.Maps.Coord;
using Google.Maps.Event;
using System.Collections.Generic;
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
    //private static LatLng testpos;
    //private Vector3 fixedObjPos;
    private MapsService mapsService;
    private S2Geofence s2geo;
    private string geoFenceCell;
    

    // Start is called before the first frame update
    private void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");
        s2geo = new S2Geofence();
        //anchor = null;
        //testpos = new LatLng(testLat, testLon);

        //Entry point Maps SDK and init initial position
        mapsService = GetComponent<MapsService>();

        // Register a listener to be notified when the map is loaded
        //mapsService.Events.MapEvents.Loaded.AddListener(OnLoaded);
        //mLogger.Log(kTAG, $"FloatingOrigin set to lat = {initPos.Lat.ToString()} " +
        //  $"and lon {initPos.Lng.ToString()}");
        // Load map with default options
        //mapsService.LoadMap(ExampleDefaults.DefaultBounds,
            //ExampleDefaults.DefaultGameObjectOptions);
    }

    /* Google Maps Related
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
        //m_AnchorManager = GetComponent<ARAnchorManager>();
        arpm = GetComponent<ARPlaneManager>();
        //mLogger.Log(kTAG, "ARPlaneManager created");
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
        //render object at 2 meters if I am in a cellID
        if ((geoFenceCell = s2geo.AmIinCellId(LocationService.Instance.latitude,
                    LocationService.Instance.longitude)) != "N") // I am in some geofence for 2mt
        {

            //mLogger.Log(kTAG, $"I am in S2 Cell id {geoFenceCell}");

            if (MyGo == null)
            {
                // TODO connect object and plane otherwise avoid plane detection
                //now Im using it only for triggering the instantiation
                if (arpm)
                {
                    // TODO fix altitude
                    Vector3 forward = transform.TransformDirection(Vector3.forward) * 2;
                    forward -= new Vector3(0, 0.5f, 0);
                    //forward -= new Vector3(0, 1, 0);

                    if (arpm.trackables.count > 0) //plane detected
                    {
                        ARPlane firstPlane = null;
                        foreach (var plane in arpm.trackables)
                        {
                            firstPlane = plane;
                            break;
                        }
                        //place object at two meters
                        MyGo = Instantiate(goToPlace, forward, //OR firstPlane.center
                            transform.rotation * Quaternion.Euler(0f, 180f, 0f)) as GameObject;
                        // Add an ARAnchor component if it doesn't have one already.
                        if (MyGo.GetComponent<ARAnchor>() == null)
                        {
                            MyGo.AddComponent<ARAnchor>();
                        }
                        mLogger.Log(kTAG, $"Obj {goToPlace} placed at {forward}" +
                            $" with anchor {MyGo.GetComponent<ARAnchor>()}");

                        // TODO FIX spatial audio
                        /*
                        AudioSource audioSource = MyGo.GetComponent<AudioSource>();
                        if (audioSource != null)
                            audioSource.Play(0);
                        mLogger.Log(kTAG, $"Audio Started with rolloff mode  {audioSource.rolloffMode}" +
                            $" maxdist {audioSource.maxDistance} and mindist {audioSource.minDistance} ");
                        */

                    }                    
                }
                else
                {
                    mLogger.Log(kTAG, "ARPlanemanager problems");
                }
            }
            /*
            else
            {                    
                //update object position and roation on touch
                // TODO FIX rotation in front of camera and normal to ground
                    
                MyGo.transform.position = hitPose.position;
                MyGo.transform.LookAt(Camera.main.transform, transform.up);

                // If animation -- restart it
                if (MyGo.GetComponent<Animator>() != null)
                    MyGo.GetComponent<Animator>().Play("Run", -1, 0);

                mLogger.Log(kTAG, $"Obj {goToPlace} updated pos {hitPose.position}");     
                    
            }
            */
        }//else if if ((geoFenceCell = s2geo.AnotherID...
    }

    void InstantiateAtTwoMt()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 2;
        forward -= new Vector3(0, 1, 0);
        if (MyGo == null)
        {
            //place object at two meters
            MyGo = Instantiate(goToPlace, forward, //OR firstPlane.center
                transform.rotation * Quaternion.Euler(0f, 180f, 0f)) as GameObject;
            // Add an ARAnchor component if it doesn't have one already.
            if (MyGo.GetComponent<ARAnchor>() == null)
            {
                MyGo.AddComponent<ARAnchor>();
            }
            mLogger.Log(kTAG, $"Obj {goToPlace} placed at {forward}" +
                $" with anchor {MyGo.GetComponent<ARAnchor>()}");

            // TODO FIX spatial audio
            /*
            AudioSource audioSource = MyGo.GetComponent<AudioSource>();
            if (audioSource != null)
                audioSource.Play(0);
            mLogger.Log(kTAG, $"Audio Started with rolloff mode  {audioSource.rolloffMode}" +
                $" maxdist {audioSource.maxDistance} and mindist {audioSource.minDistance} ");
            */
        }
        /*
        else
        {                    
            //update object position and roation on touch
            // TODO FIX rotation in front of camera and normal to ground

            MyGo.transform.position = hitPose.position;
            MyGo.transform.LookAt(Camera.main.transform, transform.up);

            // If animation -- restart it
            if (MyGo.GetComponent<Animator>() != null)
                MyGo.GetComponent<Animator>().Play("Run", -1, 0);

            mLogger.Log(kTAG, $"Obj {goToPlace} updated pos {hitPose.position}");     

        }
        */
    }
}


#region GPS placement test

/*
if (_fixedGo == null)
{
    //setting init floating origin (only first time)
    if (!mapsService.Projection.IsFloatingOriginSet)
    {
        mLogger.Log(kTAG, $"My latlng pos {LocationService.Instance.CurrPos}");
        mapsService.InitFloatingOrigin(LocationService.Instance.CurrPos);
    }                

    fixedObjPos = mapsService.Projection.FromLatLngToVector3(testpos);

        // TODO adjust altitude and rotation
        _fixedGo = Instantiate(goPOI, fixedObjPos, Quaternion.identity) as GameObject;

        //adjust the rotation
        //_fixedGo.transform.Rotate(0f, 180f, 0f);

        mLogger.Log(kTAG, $"Obj 2 placed at {fixedObjPos}");
    }

}
else
{
    // TODO I should update something if object exists...
    _fixedGo.transform.position = fixedObjPos;
}
*/
#endregion