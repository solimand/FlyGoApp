using UnityEngine;
using System.Collections;

public class LocationService : MonoBehaviour
{    
    private const string kTAG = "LocationService";

    public static LocationService Instance { set; get; }
    public float latitude, longitude;

    //LOGGER
    private static ILogger mLogger = Debug.unityLogger;

    private void Start()
    {
        Instance = this;
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "LocationService Start.");
        //Do not destroy the target Object when loading a new Scene.
        DontDestroyOnLoad(gameObject);
        StartCoroutine(StartLocationService());
    }
    private IEnumerator StartLocationService()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            mLogger.Log(kTAG, "Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            mLogger.Log(kTAG, "Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            mLogger.Log(kTAG, "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();

        yield break;

    }
}