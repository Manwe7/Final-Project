using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuPanel = null, DefeatMenuPanel = null;

    #region (Un)Subscribe to player defeat event
    private void OnEnable()
    {
        Player.defeated += Defeat;    
    }

    private void OnDisable()
    {
        Player.defeated -= Defeat;
    }
    #endregion

    private void Defeat()
    {
        Time.timeScale = 0.5f;
        Invoke("OpenDefeatMenu", 1.5f);
    }

    private void OpenDefeatMenu()
    {
        DefeatMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    #region Buttons
    public void PauseBtn()
    {
        if (!PauseMenuPanel.activeSelf)
        {
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Restart()
    {
        ScoreManager.Instance.SaveRecord();
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
        ScoreManager.Instance.SaveRecord();
        SceneManager.LoadScene("Menu");
    }
    #endregion
}
