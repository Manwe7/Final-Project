using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Player _player;
    [SerializeField] private GamePlayUI _gamePlayUI;
    
    private bool _isNewRecord = false;
    private int _record;
    private int _score;   
    
    private void Awake()
    {
        Application.targetFrameRate = 60;

        _player.OnPlayerDefeated += SaveRecord;        
    }

    private void Start()
    {
        Time.timeScale = 1f;
        _score = 0;
    }    

    private void OnDisable()
    {
        _player.OnPlayerDefeated -= SaveRecord;
    }
    
    private void SaveRecord()
    {        
        if (_isNewRecord)
        {
            PlayerPrefs.SetInt("Record", _score);
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        _gamePlayUI.SetScoreText(_score);

        if (_record < _score)
        {
            _record = _score;
            _isNewRecord = true;
        }        
    }
}
