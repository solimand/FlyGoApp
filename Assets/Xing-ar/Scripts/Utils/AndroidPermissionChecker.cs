using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

class AndroidPermissionChecker
{
    private static ILogger mLogger = Debug.unityLogger;
    private const string kTAG = "AndroidPermissionChecker";
    private GameObject dialog;

    public AndroidPermissionChecker()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");
        dialog = new GameObject();
    }

    /** 
     * called in onGUI
     * 
     */
    public void RationaleAndroidPermission()
    {        
        //mLogger.Log(kTAG, "Asking rationale to user");

        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            // The user denied permission to use the location.
            // Display a message explaining why you need it with Yes/No buttons.
            // If the user says yes then present the request again
            // Display a dialog here.
            dialog.AddComponent<PermissionsRationaleAndroidDialog>();
            return;
        }
        else if (dialog != null)
        {
            Object.Destroy(dialog);
        }
        //else
        //{
        //    mLogger.Log(kTAG, "Problems in asking rationale");
        //}
        #endif
    }

    public void ExitPermissionNotGranted()
    {
        dialog.AddComponent<ExitAndroidDialog>();
    }

    public void AskAndroidPermission()
    {
        mLogger.Log(kTAG, "asking permission");

        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            //adding permission callbacks
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
            callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
            callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;

            Permission.RequestUserPermission(Permission.FineLocation);
            //dialog = new GameObject();
        }
        #endif
        else mLogger.Log(kTAG, "Permission already granted");
    }

    public bool SimplyCheckPermisison()
    {
        return Permission.HasUserAuthorizedPermission(Permission.FineLocation);
    }

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        mLogger.Log(kTAG, $"{permissionName} PermissionDeniedAndDontAskAgain");
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        mLogger.Log(kTAG, $"{permissionName} PermissionCallbacks_PermissionGranted");
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        mLogger.Log(kTAG, $"{permissionName} PermissionCallbacks_PermissionDenied");
    }
}
