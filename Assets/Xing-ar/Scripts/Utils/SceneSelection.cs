using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class SceneSelection : MonoBehaviour
{
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
        ScrollToStartPosition();
    }
    

    void ScrollToStartPosition()
    {
        m_HorizontalScrollBar.value = 0;
        m_VerticalScrollBar.value = 1;
    }

    public void DbgARButtonPressed()
    {
        LoadScene("DbgAR");
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
