using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadSceneMainScene();
        }       
    }
    public void LoadSceneMainScene()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadSceneIntructions()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadSceneSetting()
    {
        SceneManager.LoadScene(2);
    }
    public void LoadScene3()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadScene4()
    {
        SceneManager.LoadScene(4);
    }
}
