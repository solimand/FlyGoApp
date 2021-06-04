using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPScamera : MonoBehaviour
{
    public Text coordinates;

    /*
    void Start()
    {
        
    }
    */

    void Update()
    {
        coordinates.text = "Lat: " + LocationService.Instance.latitude.ToString() +
                           ", Lon: " + LocationService.Instance.longitude.ToString() +
                           " S2 Cell: " + ARobjPlacement.geoFenceCell;
            /*
            + ", Alt: " + LocationService.Instance.altitude.ToString() +
            ", horizAccuracy: " + LocationService.Instance.horizAccuracy.ToString() +
            ", horizAccuracy: " + LocationService.Instance.vertAccuracy.ToString();
            */
    }
}
