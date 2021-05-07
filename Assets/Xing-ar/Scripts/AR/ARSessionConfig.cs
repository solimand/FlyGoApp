using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARSessionConfig : MonoBehaviour
{
    //LOGGER
    private const string kTAG = "ARSessionConfig";
    private static ILogger mLogger = Debug.unityLogger;

    //Names of what I want delete with reset
    private static readonly string GO_NAME = "zombieChar(Clone)"; 

    // NOTE: in case of Reset, if I destroy the instantiated GameObj,
        // I do not need to reset the originPosition (MapService)
        // because the absence of GO instance trigger a new origin calibration

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

    public void ResetButtonPressed()
    {
        GameObject goToDel;
        if ((goToDel = GameObject.Find(GO_NAME)) != null)
        {
            mLogger.Log(kTAG, $"Destroying GO {GO_NAME}");
            Object.Destroy(goToDel);
            mLogger.Log(kTAG, $"{GO_NAME} destroyed");

        }

        ARSession arSess = GetComponent<ARSession>();
        arSess.Reset();
    }
}
