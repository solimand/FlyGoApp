using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARtapToPlace : MonoBehaviour
{
    
    public GameObject goToPlace; //obj to place
    private GameObject myGo;
    private ARRaycastManager _arRaymMn;
    private Vector2 touchPos; 
    static List<ARRaycastHit> hits =new List<ARRaycastHit>();

    // Start is called before the first frame update
    private void Awake()
    {
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

            if (myGo == null)
            {
                myGo = Instantiate(goToPlace, hitPose.position, hitPose.rotation);
            }
            else
            {
                myGo.transform.position = hitPose.position;
            }
        }
    }
}
