using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int currentSceneIndex;
    
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadNewScene()
    {
        if (currentSceneIndex == 0 || currentSceneIndex == 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1, LoadSceneMode.Single);
        }

        else if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }

        else
        {
            SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Single);
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadingScene());
    }

    private IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(currentSceneIndex + 1, LoadSceneMode.Single);
    }
}
