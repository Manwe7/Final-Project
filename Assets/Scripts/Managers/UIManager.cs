using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuPanel = null, DefeatMenuPanel = null;

    private void Awake()
    {
        Player.defeated += Defeat;    
    }

    private void OnDisable()
    {
        Player.defeated -= Defeat;
    }
    
    public void PauseBtn()
    {
        if (!PauseMenuPanel.activeSelf)
        {
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void Defeat()
    {
        Time.timeScale = 0.5f;
        Invoke("OpenDefeatMenu", 1.5f);
    }

    private void OpenDefeatMenu()
    {
        //Open defeat menu
        DefeatMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        GameManager.gameManagerInstance.SaveRecord();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        GameManager.gameManagerInstance.SaveRecord();
        SceneManager.LoadScene("Menu");
    }
}
