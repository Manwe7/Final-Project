using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject _pauseMenuPanel = null;

    [Header("Scripts")]
    [SerializeField] private GameSpeed _gameSpeed;

    [SerializeField] private GamePlayUI _gamePlayUI;

    [Header("Record on pause panel")]
    [SerializeField] private Text _pauseRecordText;

    private int _record;

    private IGetRepo<int> _repo;

    private void Awake()
    {
        _repo = new RecordRepo();
        
        _gamePlayUI.OnGamePause += ShowRecord;
    }

    private void OnDestroy()
    {
        _gamePlayUI.OnGamePause -= ShowRecord;
    }

    private void ShowRecord()
    {
        _record = _repo.Get();
        _pauseRecordText.text = _record.ToString();
    }

    public void Resume()
    {
        _pauseMenuPanel.SetActive(false);
        _gameSpeed.ToNormal();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _gameSpeed.ToNormal();
    }

    public void Exit()
    {
        SceneManager.LoadScene(SceneNames.Menu);
    }
}
