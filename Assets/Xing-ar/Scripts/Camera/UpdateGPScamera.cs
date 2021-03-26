using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGPScamera : MonoBehaviour
{
    public Text coordinates;

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }
    */

    // Update is called once per frame
    void Update()
    {
        coordinates.text = "Lat: " + LocationService.Instance.latitude.ToString() + 
            ", Lon: " + LocationService.Instance.longitude.ToString() +
            ", Alt: " + LocationService.Instance.altitude.ToString() +
            ", horizAccuracy: " + LocationService.Instance.horizAccuracy.ToString() +
            ", horizAccuracy: " + LocationService.Instance.vertAccuracy.ToString();
    }
}
