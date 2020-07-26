using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _pauseMenuPanel = null;

    [SerializeField] private GameSpeed _gameSpeed; 

    public void Resume()
    {
        _pauseMenuPanel.SetActive(false);
        _gameSpeed.ToNormal();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _gameSpeed.ToNormal();
    }

    public void Exit()
    {
        SceneManager.LoadScene(SceneNames.Menu);
    }
}
