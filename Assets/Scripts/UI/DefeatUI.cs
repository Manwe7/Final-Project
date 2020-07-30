using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class DefeatUI : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private PlayerHealth _playerHealth;

    [SerializeField] private GameSpeed _gameSpeed;

    [Header("Defeat panel")]
    [SerializeField] private GameObject _defeatMenuPanel = null;

    [Header("Record on defeat panel")]
    [SerializeField] private Text _defeatRecordText;

    private int _record;

    private IGetRepo<int> _repo;

    private void Awake()
    {
        _repo = new RecordRepo();

        _gameSpeed.ToNormal();
        
        _playerHealth.OnPlayerDefeated += Defeat;
    }    

    private void OnDisable()
    {
        _playerHealth.OnPlayerDefeated -= Defeat;
    }

    private void Defeat()
    {        
        _gameSpeed.ToHalfSpeed();        
        StartCoroutine(OpenDefeatMenu());
    }

    private IEnumerator OpenDefeatMenu()
    {
        yield return new WaitForSeconds(1.5f);
                
        _record = _repo.Get();
        _defeatRecordText.text = _record.ToString();
        
        _defeatMenuPanel.SetActive(true);        
        _gameSpeed.Stop();
        
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
