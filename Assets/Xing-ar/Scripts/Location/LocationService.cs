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

    // Location Parameters
    /// <summary>
    /// desired service accuracy in meters. 
    /// Using higher value like 500 usually does not require to turn GPS chip on and thus saves battery power. 
    /// Values like 5-10 could be used for getting best accuracy. Default value is 10 meters.
    /// </summary>
    private const float desiredAccuracyInMeters = 1;
    /// <summary>
    /// minimum distance (measured in meters) a device must move laterally before Input.location property is updated. 
    /// Higher values like 500 imply less overhead. Default is 10 meters
    /// </summary>
    private const float updateDistanceInMeters = 0.1f;
    private const float UPDATE_TIME = 5f;

    /*
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
    */

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

    IEnumerator Start()
    //private IEnumerator StartLocationService()
    {
        Instance = this;
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start.");

        //apc = new AndroidPermissionChecker();
        //apc.AskAndroidPermission();
        //Do not destroy the target Object when loading a new Scene.
        //DontDestroyOnLoad(gameObject);


        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            //TODO make toast enable location
            yield break;

        // Start service before querying location
        Input.compass.enabled = true;
        Input.location.Start(desiredAccuracyInMeters, updateDistanceInMeters);
        
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
                " Lon: " + Input.location.lastData.longitude + 
                " Alt: " + Input.location.lastData.altitude + 
                " horizAccuracy: " + Input.location.lastData.horizontalAccuracy +
                " verticalAccuracy: " + Input.location.lastData.verticalAccuracy +
                " time: " + Input.location.lastData.timestamp);
        }

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        altitude = Input.location.lastData.altitude;
        horizAccuracy = Input.location.lastData.horizontalAccuracy;
        vertAccuracy = Input.location.lastData.verticalAccuracy;

        StartCoroutine(updateGPS());
        yield break;
    }
    IEnumerator updateGPS()
    {        
        WaitForSeconds updateTime = new WaitForSeconds(UPDATE_TIME);

        while (true)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            altitude = Input.location.lastData.altitude;
            horizAccuracy = Input.location.lastData.horizontalAccuracy;
            vertAccuracy = Input.location.lastData.verticalAccuracy;

            mLogger.Log(kTAG, "Location: Lat" + Input.location.lastData.latitude +
                " Lon: " + Input.location.lastData.longitude);
            //longitudeText.text = "Longitude: " + Input.location.lastData.longitude;
            //latitudeText.text = "Latitude: " + Input.location.lastData.latitude;
            yield return updateTime;
        }
    }


        /*
        // Stop service if there is no need to query location updates continuously
        void stopGPS()
        {
            Input.location.Stop();
            StopCoroutine(StartLocationService());
        }
        void OnDisable()
        {
            stopGPS();
        }
        */

    }

