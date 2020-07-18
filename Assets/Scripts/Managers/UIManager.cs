using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player _player;
    
    [Header("Panels")]
    [SerializeField] private GameObject _pauseMenuPanel = null;
    [SerializeField] private GameObject  _defeatMenuPanel = null;

    [Header("Record on defeat panel")]
    [SerializeField] private Text _defeatRecordText;

    private int _record;

    private void OnEnable()
    {
        _player.OnPlayerDefeated += Defeat;    
    }

    private void OnDisable()
    {
        _player.OnPlayerDefeated -= Defeat;
    }

    private void Defeat()
    {        
        Time.timeScale = 0.5f;
        Invoke("OpenDefeatMenu", 1.5f);
    }

    private void OpenDefeatMenu()
    {
        _record = PlayerPrefs.GetInt("Record", 0);
        _defeatRecordText.text = _record.ToString();
        
        _defeatMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    #region Buttons
    public void PauseBtn()
    {        
        _pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Resume()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
    #endregion
}
