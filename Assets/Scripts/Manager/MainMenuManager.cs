using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("TestGameplay");
    }
}
