using BaseClasses;
using BasePlayer;
using Interfaces;
using Managers;
using UI;
using UnityEngine;

public class GameSessionScore : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private GamePlayUI _gamePlayUI;
    [SerializeField] private DifficultyManager _difficultyManager;
    [SerializeField] private DefeatUI _defeatUI;

    private bool _isNewRecord;
    private int _record;
    private int _score;
    
    private IRepo<SaveAttributes> _repo;
    private SaveAttributes _saveAttributes;

    private void Awake()
    {
        _repo = new SaveClassRepo();
        _saveAttributes = _repo.Get();

        _record = _saveAttributes.Records[_difficultyManager.DifficultyLevel];
    }

    private void Start()
    {        
        _score = 0;
    }

    private void OnEnable()
    {
        _playerHealth.OnPlayerDefeated += SaveRecord;
    }

    private void OnDisable()
    {
        _playerHealth.OnPlayerDefeated -= SaveRecord;
    }
    
    private void SaveRecord()
    {
        _defeatUI.ShowRecord(_record);
        
        if (!_isNewRecord) return;

        _saveAttributes.Records[_difficultyManager.DifficultyLevel] = _record;
        _repo.Save(_saveAttributes);
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
