using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;
    }



    public void RetryLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        int next =            SceneManager.GetActiveScene().buildIndex + 1;

        if (next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
        else
        {
            Debug.Log("Game Complete");
        }
    }

    public void Home()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}
