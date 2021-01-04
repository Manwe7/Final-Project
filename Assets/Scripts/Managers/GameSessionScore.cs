using Interfaces;
using UnityEngine;

public class GameSessionScore : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerOfflineScipts.PlayerHealth _playerHealth;
    [SerializeField] private GamePlayUI _gamePlayUI;

    private bool _isNewRecord;
    private int _record;
    private int _score;

    private IRepo<SaveAttributes> _repoInt;
    private SaveAttributes _saveAttributes;

    private void Awake()
    {     
        _repoInt = new SaveClassRepo();
        
        _saveAttributes = _repoInt.Get();
        _record = _saveAttributes.Record;
           
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
        
        _saveAttributes.Record = _score;
        _repoInt.Save(_saveAttributes);
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
