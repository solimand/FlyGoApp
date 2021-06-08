using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class SceneSelection : MonoBehaviour
{
    private static ILogger mLogger = Debug.unityLogger;
    private const string kTAG = "SceneSelection";
    private const string MainSceneName = "MainARScene";
    //private GameObject dialog = null;
    private AndroidPermissionChecker apc;
    //private bool comeBackFromPermission = false;

    [SerializeField]
    GameObject m_AllMenu;
    public GameObject allMenu
    {
        get => m_AllMenu;
        set => m_AllMenu = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");
        apc = new AndroidPermissionChecker();
        //ScrollToStartPosition();
    }
    public void DeclineButtonPressed()
    {
        Application.Quit();
    }

    public void MainARButtonPressed() 
    {
#if PLATFORM_ANDROID
        if (apc == null)
            apc = new AndroidPermissionChecker();

        //comeBackFromPermission = true;
        apc.AskAndroidPermission(Permission.FineLocation);
        // I need a coroutine to wait...see docs
        StartCoroutine(WaitDialog(Permission.FineLocation));

        /*
        // TODO test other permissions
        apc.AskAndroidPermission(Permission.Camera);
        StartCoroutine(WaitDialog(Permission.Camera));
        apc.AskAndroidPermission(Permission.ExternalStorageWrite);
        StartCoroutine(WaitDialog(Permission.ExternalStorageWrite));*/

        //mLogger.Log(kTAG, "control returned to scenesel");
#endif
#if UNITY_IOS
        mLogger.Log(kTAG, "IOS deployment");
        LoadScene(MainSceneName);
#endif
    }

    /// <summary>Method <c>WaitDialog</c> waits for user taking a decision.
    /// This is useful in case of user allows permission, 
    /// there is a delay in which user granted permission but check gives false. </summary>
    IEnumerator WaitDialog(string permission)
    {
        //waiting user decision
        while (!apc.DecisionTaken)
        {
            yield return new WaitForSeconds(1);
            if (apc.DecisionTaken)
                break;
        }
#if PLATFORM_ANDROID
        //I have to check, decisionTaken is true also in case of permission denied
        //TODO FIX check fails in case of permission granted on rationale dialog
        if (apc.SimplyCheckPermisison(permission))
            LoadScene(MainSceneName);
        else
            mLogger.Log(kTAG, $"you need {permission} for this scene");
#endif
    }

    static void LoadScene(string sceneName)
    {
        LoaderUtility.Initialize();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }



    /*TODO Check device support    
    public void CheckSupportButtonPressed()
    {
        LoadScene("Check Support");
    }
    */
    /* TODO double back for exit
    public void BackButtonPressed()
    {
        ActiveMenu.currentMenu = MenuType.Main;
        m___Menu.SetActive(false);
        m_AllMenu.SetActive(true);
        ScrollToStartPosition();
    }
    */
}
