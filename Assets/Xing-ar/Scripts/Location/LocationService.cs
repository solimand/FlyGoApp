using UnityEngine;
using System.Collections;
using Google.Maps.Coord;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class LocationService : MonoBehaviour
{    
    private const string kTAG = "LocationService";

    public static LocationService Instance { set; get; }
    public LatLng CurrPos { get => currPos; set => currPos = value; }

    public float latitude, longitude, altitude;
    public float horizAccuracy, vertAccuracy;
    //public Vector3 ucsTest;

    //LOGGER
    private static ILogger mLogger = Debug.unityLogger;

    //private LocationTrans locTrans = LocationTrans.GetInstance();
    //private GameObject dialog = null;
    private LatLng currPos;
    private bool getOrigin;

    private void Start()
    {
        Instance = this;
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start.");
        
        // location permission
        #if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                //dialog = new GameObject();
            }
        #endif
        
        //Do not destroy the target Object when loading a new Scene.
        //DontDestroyOnLoad(gameObject);
        
        //TODO PermissionsRationaleDialog :
        /*
        #if PLATFORM_ANDROID
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                // The user denied permission to use the location.
                // Display a message explaining why you need it with Yes/No buttons.
                // If the user says yes then present the request again
                // Display a dialog here.
                    // request again
                    // quit if deny or if deny and dont ask again
                //dialog.AddComponent<PermissionsRationaleDialog>();
                //return;
            }
            else if (dialog != null)
            {
                Destroy(dialog);
            }
        #endif
        */
        StartCoroutine(StartLocationService());
    }

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

        // The first position will be the floating origin for Maps SDL
        if (!getOrigin)
        {
            CurrPos = new LatLng(latitude, longitude);
            mLogger.Log(kTAG, $"My origin pos {CurrPos}");
            getOrigin = true;
        }

        /*
        if (counter==0)
        {
            //TODO LocationTrans.SetLocalOrigin()
            // TEST with fixed coordinates
            //ucsTest = LocationTrans.GPSToUCS((float)41.834171, (float)15.571149);
        }
        */

        // Stop service if there is no need to query location updates continuously
        //Input.location.Stop();

        yield break;

    }
}

/*
public class PermissionsRationaleDialog : MonoBehaviour
{
    const int kDialogWidth = 300;
    const int kDialogHeight = 100;
    private bool windowOpen = true;

    void DoMyWindow(int windowID)
    {
        GUI.Label(new Rect(10, 20, kDialogWidth - 20, kDialogHeight - 50),
            "Please let me use the location.");
        GUI.Button(new Rect(10, kDialogHeight - 30, 100, 20), "No");
        if (GUI.Button(new Rect(kDialogWidth - 110, kDialogHeight - 30, 100, 20), "Yes"))
        {
        #if PLATFORM_ANDROID
            Permission.RequestUserPermission(Permission.FineLocation);
        #endif
            windowOpen = false;
        }
    }

    void OnGUI ()
    {
        if (windowOpen)
        {
            Rect rect = new Rect((Screen.width / 2) - (kDialogWidth / 2), (Screen.height / 2) - (kDialogHeight / 2), kDialogWidth, kDialogHeight);
            GUI.ModalWindow(0, rect, DoMyWindow, "Permissions Request Dialog");
        }
    }
}
*/