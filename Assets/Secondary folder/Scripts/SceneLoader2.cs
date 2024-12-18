using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader2 : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadSceneRestart()
    {
        SceneManager.LoadScene(1);
    }
}
