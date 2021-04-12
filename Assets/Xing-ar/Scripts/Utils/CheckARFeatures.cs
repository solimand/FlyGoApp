using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
#if UNITY_IOS
using UnityEngine.XR.ARKit;
#endif
using UnityEngine.XR.Management;

public class CheckARFeatures : MonoBehaviour
{
    //LOGGER
    private const string kTAG = "CheckARFeatures";
    private static ILogger mLogger = Debug.unityLogger;

    [SerializeField]
    Button m_DbgAR;
    public Button dbgAR
    {
        get => m_DbgAR;
        set => m_DbgAR = value;
    }

    /* TODO config Chooser
    [SerializeField]
    Button m_ConfigChooser;
    public Button configChooser
    {
        get => m_ConfigChooser;
        set => m_ConfigChooser = value;
    }
    */

    void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "ARtapToPlace Start");

        var planeDescriptors = new List<XRPlaneSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(planeDescriptors);

        var rayCastDescriptors = new List<XRRaycastSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(rayCastDescriptors);

        var faceDescriptors = new List<XRFaceSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(faceDescriptors);

        var imageDescriptors = new List<XRImageTrackingSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(imageDescriptors);

        var envDescriptors = new List<XREnvironmentProbeSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(envDescriptors);

        var anchorDescriptors = new List<XRAnchorSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(anchorDescriptors);

        var objectDescriptors = new List<XRObjectTrackingSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(objectDescriptors);

        var participantDescriptors = new List<XRParticipantSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(participantDescriptors);

        var depthDescriptors = new List<XRDepthSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(depthDescriptors);

        var occlusionDescriptors = new List<XROcclusionSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(occlusionDescriptors);

        var cameraDescriptors = new List<XRCameraSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(cameraDescriptors);

        var sessionDescriptors = new List<XRSessionSubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(sessionDescriptors);

        var bodyTrackingDescriptors = new List<XRHumanBodySubsystemDescriptor>();
        SubsystemManager.GetSubsystemDescriptors(bodyTrackingDescriptors);

        //enable all interactable scenes
        if (planeDescriptors.Count > 0 && rayCastDescriptors.Count > 0)
        {
            mLogger.Log(kTAG, "Check features: plane and raycast enabled!!");
            m_DbgAR.interactable = true;
        }
           
    }
}
