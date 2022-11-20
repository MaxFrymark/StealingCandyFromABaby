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
        if(currentSceneIndex == 0)
        {
            SceneManager.LoadScene(1);
        }

        else if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }

        else
        {
            SceneManager.LoadScene(currentSceneIndex);
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadingScene());
    }

    private IEnumerator LoadingScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(currentSceneIndex++);
    }
}
