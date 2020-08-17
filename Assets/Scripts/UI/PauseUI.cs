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

    private IGetRepo<SaveAttributes> _repoInt;

    SaveAttributes _saveAttributes;

    private void Awake()
    {                
        _repoInt = new SaveClassRepo();
        
        _gamePlayUI.OnGamePause += ShowRecord;
    }

    private void OnDestroy()
    {
        _gamePlayUI.OnGamePause -= ShowRecord;
    }

    private void ShowRecord()
    {
        _saveAttributes = _repoInt.Get();
        _record = _saveAttributes.Record;

        _pauseRecordText.text = _record.ToString();
    }

    public void Resume()
    {
        _pauseMenuPanel.SetActive(false);
        _gameSpeed.SetToNormal();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        _gameSpeed.SetToNormal();
    }

    public void Exit()
    {
        SceneManager.LoadScene(SceneNames.Menu);
    }
}
