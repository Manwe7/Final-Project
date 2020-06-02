using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject SettingsPanel=null;
    [SerializeField] Text record = null;
    private float _oldRecord;

    private void Start()
    {
        _oldRecord = PlayerPrefs.GetFloat("ScoreRecord", 0);
        record.text = _oldRecord.ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void PlayO()
    {
        SceneManager.LoadScene("GameOnline");
    }

    public void Settings()
    {
        SettingsPanel.SetActive(true);
    }

    public void Back()
    {
        SettingsPanel.SetActive(false);
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        _oldRecord = PlayerPrefs.GetFloat("ScoreRecord", 0);
        record.text = _oldRecord.ToString();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
