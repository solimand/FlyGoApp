using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionConfig : MonoBehaviour
{
    //LOGGER
    private const string kTAG = "ARSessionConfig";
    private static ILogger mLogger = Debug.unityLogger;

    // Start is called before the first frame update
    void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");
    }

    // Update is called once per frame
    void Update()
    {
        // UTILS - the session is in initializing during first and subsequent inits (eg reset)
        /*
        if (ARSession.state == ARSessionState.SessionInitializing)
        {
            mLogger.Log(kTAG, "AR Session initializing...");
        }
        */
    }

    // NOTE: in case of Reset, if I destroy the instantiated GameObj,
        // I do not need to reset the originPosition (MapService)
        // because the absence of GO instance trigger a new origin calibration
    public void ResetButtonPressed()
    {
        if (ARobjPlacement.AlberoMuscoloso != null)
        {
            Destroy(ARobjPlacement.AlberoMuscoloso);
            mLogger.Log(kTAG, $"obj {ARobjPlacement.AlberoMuscoloso} destroyed, resetting...");
        }
        ARobjPlacement.GeoFencePrevCell = "";
        mLogger.Log(kTAG, "geofenceprevcell destroyed, resetting...");

        ARSession arSess = GetComponent<ARSession>();
        arSess.Reset();
        mLogger.Log(kTAG, "Session Resetted");
    }
}
