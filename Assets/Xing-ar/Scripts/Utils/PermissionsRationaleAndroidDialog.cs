using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class PermissionsRationaleAndroidDialog : MonoBehaviour
{
    const int kDialogWidth = 600;
    const int kDialogHeight = 200;
    private bool windowOpen = true;
    private static ILogger mLogger = Debug.unityLogger;
    private const string kTAG = "PermissionsRationaleAndroidDialog";

    //TODO add definition/permission
    void DoMyWindow(int windowID)
    {
        GUI.Label(new Rect(10, 20, kDialogWidth - 20, kDialogHeight - 50), "Please let me use the location");
        if(GUI.Button(new Rect(10, kDialogHeight - 30, 100, 20), "No-EXIT"))
        {
            Application.Quit();
        }
        if (GUI.Button(new Rect(kDialogWidth - 110, kDialogHeight - 30, 100, 20), "Yes"))
        {
            #if PLATFORM_ANDROID
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
            callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
            callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
            Permission.RequestUserPermission(Permission.FineLocation,callbacks);
        #endif
            windowOpen = false;
        }
    }

    void OnGUI()
    {
        if (windowOpen)
        {
            Rect rect = new Rect((Screen.width / 2) - (kDialogWidth / 2), (Screen.height / 2) - (kDialogHeight / 2), kDialogWidth, kDialogHeight);
            GUI.ModalWindow(0, rect, DoMyWindow, "Permissions Request Dialog");
        }
    }

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        mLogger.Log(kTAG, $"{permissionName} PermissionDeniedAndDontAskAgain");
        Application.Quit();
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        mLogger.Log(kTAG, $"{permissionName} PermissionCallbacks_PermissionGranted");
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        mLogger.Log(kTAG, $"{permissionName} PermissionCallbacks_PermissionDenied--second time--QUIT");
        Application.Quit();
    }
}