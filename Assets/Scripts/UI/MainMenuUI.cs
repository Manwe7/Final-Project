using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //static class with const
    public void Play()
    {
        SceneManager.LoadScene(StaticStringNames.Menu);
    }

    public void Lobby()
    {
        SceneManager.LoadScene(StaticStringNames.Lobby);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
