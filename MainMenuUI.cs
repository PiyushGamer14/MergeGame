using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]
    private string FirstLevelName;

    public void PlayGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(FirstLevelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
