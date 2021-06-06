using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionConfig : MonoBehaviour
{
    //LOGGER
    private const string kTAG = "ARSessionConfig";
    private static ILogger mLogger = Debug.unityLogger;

    void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");
    }

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
        ARobjPlacement.DestroyAllObj();
        mLogger.Log(kTAG, "all object destroyed, resetting...");

        ARSession arSess = GetComponent<ARSession>();
        arSess.Reset();
        mLogger.Log(kTAG, "Session Resetted");
    }
}
