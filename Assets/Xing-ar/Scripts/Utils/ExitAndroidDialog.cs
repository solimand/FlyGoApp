using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class ExitAndroidDialog : MonoBehaviour
{
    const int kDialogWidth = 600;
    const int kDialogHeight = 200;
    private bool windowOpen = true;

    void DoMyWindow(int windowID)
    {
        GUI.Label(new Rect(10, 20, kDialogWidth - 20, kDialogHeight - 50), "Set permission in phone settings");
        if (GUI.Button(new Rect(kDialogWidth - 110, kDialogHeight - 30, 100, 20), "EXIT"))
        {
            Application.Quit();
            windowOpen = false;
        }
    }

    void OnGUI()
    {
        if (windowOpen)
        {
            Rect rect = new Rect((Screen.width / 2) - (kDialogWidth / 2), (Screen.height / 2) - (kDialogHeight / 2), kDialogWidth, kDialogHeight);
            GUI.ModalWindow(0, rect, DoMyWindow, "Exit Dialog");
        }
    }
}