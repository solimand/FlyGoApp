using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    GameObject m_BackButton;
    public GameObject backButton
    {
        get => m_BackButton;
        set => m_BackButton = value;
    }

    void Start()
    {
        if (Application.CanStreamedLevelBeLoaded("Menu"))
        {
            m_BackButton.SetActive(true);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButtonPressed();
        }
    }

    public void BackButtonPressed()
    {
        // TODO stop location service

        if (Application.CanStreamedLevelBeLoaded("Menu"))
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            //UnityEngine.XR.ARFoundation.LoaderUtility.Deinitialize();
        }
    }
}
