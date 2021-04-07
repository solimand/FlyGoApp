using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARtapToPlace : MonoBehaviour
{
    //private const string OBJ_UNITY_TAG = "CubeObject"; //name of the prefab in unity
    private const string kTAG = "ARtapToPlace";

    //LOGGER
    private static ILogger mLogger = Debug.unityLogger;

    public GameObject goToPlace; //obj to place with tap
    public GameObject goPOI; //obj to place with tap

    private GameObject _myGo;
    private GameObject _fixedGo;
    private ARRaycastManager _arRaymMn;
    //private Vector2 touchPos; 
    static List<ARRaycastHit> hits =new List<ARRaycastHit>();

    // Start is called before the first frame update
    private void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        //mLogger.Log(kTAG, "ARtapToPlace Start.");
    }

    private void Awake()
    {
        mLogger.Log(kTAG, "ARtapToPlace Awake.");
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
                // TODO test altitude of device
                _fixedGo = Instantiate(goPOI, LocationService.Instance.ucsTest, Quaternion.identity) as GameObject;
            }
            else
            {
                //update object position on touch
                _myGo.transform.position = hitPose.position;
            }
        }
        
        //Test instantiate fixedobj
        
    }
}
