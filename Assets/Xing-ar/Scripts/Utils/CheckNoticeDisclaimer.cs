using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;

public class CheckNoticeDisclaimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Player accepted the disclaimer
        if (PlayerPrefs.GetInt(SceneSelection.NOTICE) == 1)
            LoadScene(SceneSelection.MAIN_SCENE);
        else
            LoadScene(SceneSelection.DISCLAIMER_SCENE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static void LoadScene(string sceneName)
    {
        LoaderUtility.Initialize();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
