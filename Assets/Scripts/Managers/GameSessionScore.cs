using UnityEngine;

public class GameSessionScore : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerHealth _playerHealth;

    [SerializeField] private GamePlayUI _gamePlayUI;

    private bool _isNewRecord;

    private int _record;

    private int _score;

    private ISaveRepo<int> _repo;

    private void Awake()
    {
        _repo = new RecordRepo();

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
        if (_isNewRecord)
        {
            _repo.Save(_score);
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
