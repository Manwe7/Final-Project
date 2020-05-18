using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuPanel = null, DefeatMenuPanel = null;
    [SerializeField] Text ScoreText = null, pauseRecordText = null, defeatRecordText = null;
    
    private bool _gameIsPaused = false, _recordIsNew = false;
    private GameObject _player;    
    private float _oldRecord;

    public static float CurrentScore;

    private void Start()
    {
        Time.timeScale = 1f;
        _oldRecord = PlayerPrefs.GetFloat("ScoreRecord", 0);
        _player = GameObject.FindGameObjectWithTag("Player");

        CurrentScore = 0;
    }

    private void Update()
    {
        //If record is broken, update it 
        if (_oldRecord < CurrentScore)
        {
            _oldRecord = CurrentScore;
            _recordIsNew = true;
        }

        pauseRecordText.text = _oldRecord.ToString();
        defeatRecordText.text = _oldRecord.ToString();

        ScoreText.text = CurrentScore.ToString();

        if(_player.activeSelf == false)
        {
            SaveRecord();
            Time.timeScale = 0.5f;
            Invoke("OpenDefeatMenu", 1.5f);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && _gameIsPaused == false)
        {
            PauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
            _gameIsPaused = true;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && _gameIsPaused == true)
        {
            PauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
            _gameIsPaused = false;
        }
    }

    private void OpenDefeatMenu()
    {
        //Open defeat menu
        SaveRecord();
        DefeatMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SaveRecord();
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
        SaveRecord();
        SceneManager.LoadScene("Menu");
    }

    private void SaveRecord()
    {
        if (_recordIsNew == true)
        {
            PlayerPrefs.SetFloat("ScoreRecord", CurrentScore);
        }
    }
}
