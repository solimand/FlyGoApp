using UnityEngine;
using System.Collections;
using Google.Maps.Coord;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class LocationService : MonoBehaviour
{
    //LOGGER
    private static ILogger mLogger = Debug.unityLogger;
    private const string kTAG = "LocationService";

    public static LocationService Instance { set; get; }
    //public LatLng CurrPos { get => currPos; set => currPos = value; }

    public float latitude, longitude, altitude;
    public float horizAccuracy, vertAccuracy;

    private void Start()
    {
        Instance = this;
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start.");

        //apc = new AndroidPermissionChecker();
        //apc.AskAndroidPermission();

        //Do not destroy the target Object when loading a new Scene.
        //DontDestroyOnLoad(gameObject);

        StartCoroutine(StartLocationService());
    }

    /*
    void OnGUI()
    {
        if (apc == null)
        {
            apc = new AndroidPermissionChecker();
        }
        apc.RationaleAndroidPermission();
    }
    */

    private IEnumerator StartLocationService()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            //TODO make toast enable location
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
            mLogger.Log(kTAG, "Location: Lat" + Input.location.lastData.latitude +
                "Lon: " + Input.location.lastData.longitude + 
                "Alt: " + Input.location.lastData.altitude + 
                "horizAccuracy: " + Input.location.lastData.horizontalAccuracy +
                "verticalAccuracy: " + Input.location.lastData.verticalAccuracy +
                "time: " + Input.location.lastData.timestamp);
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        altitude = Input.location.lastData.altitude;
        horizAccuracy = Input.location.lastData.horizontalAccuracy;
        vertAccuracy = Input.location.lastData.verticalAccuracy;        

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();

        yield break;

    }

}

