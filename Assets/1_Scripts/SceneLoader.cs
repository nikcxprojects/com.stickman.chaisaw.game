using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;

    private SceneLoader()
    {}

    public static SceneLoader getInstance()
    {
        if (instance == null) instance = new SceneLoader();
        return instance;
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
