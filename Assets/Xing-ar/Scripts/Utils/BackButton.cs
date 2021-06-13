using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    //LOGGER
    private const string kTAG = "BackButton";
    private static ILogger mLogger = Debug.unityLogger;

    /*
    [SerializeField]
    GameObject m_BackButton;
    public GameObject backButton
    {
        get => m_BackButton;
        set => m_BackButton = value;
    }
    */

    void Start()
    {
        mLogger = new Logger(new MyLogHandler());
       /*
        if (Application.CanStreamedLevelBeLoaded("Menu"))
        {
            m_BackButton.SetActive(true);
        }
       */
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }

    public void ReloadButtonPressed()
    {
        if (Application.CanStreamedLevelBeLoaded("MainARScene"))
        {
            ARobjPlacement.DestroyAllObj();
            mLogger.Log(kTAG, "all object destroyed, reloading...");

            SceneManager.LoadScene("MainARScene", LoadSceneMode.Single);
        }
    }

    public void BackButtonPressed()
    {
        if (Application.CanStreamedLevelBeLoaded("Menu"))
        {
            //TODO check what i have to destroy in the main scene
            if (ARobjPlacement.AlberoMuscoloso != null)
            {
                Destroy(ARobjPlacement.AlberoMuscoloso);
                mLogger.Log(kTAG, $"back button pressed, " +
                    $"obj {ARobjPlacement.AlberoMuscoloso} destroyed");
            }
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            //UnityEngine.XR.ARFoundation.LoaderUtility.Deinitialize();
        }
    }
}
