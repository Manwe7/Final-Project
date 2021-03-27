using BasePlayer;
using UI;
using UnityEngine;

public class GameSessionScore : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GamePlayUI _gamePlayUI;

    private bool _isNewRecord;
    private int _record;
    private int _score;

    private void Awake()
    {
        _record = PlayerPrefs.GetInt(SaveAttributes.RecordDifficulty1);

        _playerHealth.OnPlayerDefeated += SaveRecord;        
    }

    private void Start()
    {        
        _score = 0;
    }    

    private void OnDisable()
    {
        _playerHealth.OnPlayerDefeated -= SaveRecord;
    }
    
    private void SaveRecord()
    {
        if (!_isNewRecord) return;
        
        PlayerPrefs.SetInt(SaveAttributes.RecordDifficulty1, _record);
    }

    public void AddScore(int score)
    {
        _score += score;
        _gamePlayUI.SetScoreText(_score);

        if (_record >= _score) return;
        
        _record = _score;
        _isNewRecord = true;
    }
}
