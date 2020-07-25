using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneNames.Game);
    }

    public void Lobby()
    {
        SceneManager.LoadScene(SceneNames.Lobby);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
