using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuPanel = null, DefeatMenuPanel = null;

    public void PauseBtn()
    {
        if (!PauseMenuPanel.activeSelf)
        {
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (PauseMenuPanel.activeSelf)
        {
            PauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    private void Update()
    {
        if (GameManager.Instance._player.activeSelf == false)
        {
            GameManager.Instance.SaveRecord();
            Time.timeScale = 0.5f;
            Invoke("OpenDefeatMenu", 1.5f);
        }
    }

    private void OpenDefeatMenu()
    {
        //Open defeat menu
        GameManager.Instance.SaveRecord();
        DefeatMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        GameManager.Instance.SaveRecord();
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
        GameManager.Instance.SaveRecord();
        SceneManager.LoadScene("Menu");
    }
}
