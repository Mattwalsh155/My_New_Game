using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadStatsScreen()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOverDelay());
    }

    IEnumerator GameOverDelay()
    {
        var delayTime = 1f;
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("Game Over Scene");
    }

    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);

    }
}
