using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
//#if PLATFORM_ANDROID
//using UnityEngine.Android;
//#endif

public class SceneSelection : MonoBehaviour
{
    private static ILogger mLogger = Debug.unityLogger;
    private const string kTAG = "SceneSelection";
    //private GameObject dialog = null;
    private AndroidPermissionChecker apc;
    //private bool comeBackFromPermission = false;

    [SerializeField]
    Scrollbar m_HorizontalScrollBar;
    public Scrollbar horizontalScrollBar
    {
        get => m_HorizontalScrollBar;
        set => m_HorizontalScrollBar = value;
    }

    [SerializeField]
    Scrollbar m_VerticalScrollBar;
    public Scrollbar verticalScrollBar
    {
        get => m_VerticalScrollBar;
        set => m_VerticalScrollBar = value;
    }

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
        ScrollToStartPosition();
    }

    void ScrollToStartPosition()
    {
        m_HorizontalScrollBar.value = 0;
        m_VerticalScrollBar.value = 1;
    }

    /*
    private void OnApplicationFocus(bool focus)
    {
        mLogger.Log(kTAG, "onAppFocus");
        if (comeBackFromPermission)
        {
            if (apc == null)
                apc = new AndroidPermissionChecker();
            
            if (!apc.SimplyCheckPermisison())
            {
                mLogger.Log(kTAG, "permission denied, asking for rationale...");
                //apc.RationaleAndroidPermission();
            }
            if (!apc.SimplyCheckPermisison())
            {
                mLogger.Log(kTAG, "permission denied, exiting...");
                //apc.ExitPermissionNotGranted();
            }
        }
        comeBackFromPermission = false;
    }
    */

    public void DbgARButtonPressed() 
    {
        if (apc == null)
            apc = new AndroidPermissionChecker();

        //comeBackFromPermission = true;
        apc.AskAndroidPermission();

        //TODO wait in case of user allow permission
        //  there is a delay in which user granted permission but check gives false
        //wait until user take a decision
        StartCoroutine(WaitDialog());

        mLogger.Log(kTAG, "control returned to scenesel");
        if(apc.SimplyCheckPermisison())
            LoadScene("DbgARScene");
        else
            mLogger.Log(kTAG, "you need permission for this scene");
    }
    IEnumerator WaitDialog()
    {
        yield return new WaitUntil(() => apc.DecisionTaken==true);
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
