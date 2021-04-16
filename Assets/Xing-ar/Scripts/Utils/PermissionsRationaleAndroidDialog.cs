using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class PermissionsRationaleAndroidDialog : MonoBehaviour
{
    const int kDialogWidth = 600;
    const int kDialogHeight = 200;
    private bool windowOpen = true;

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
            Permission.RequestUserPermission(Permission.FineLocation);
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
}