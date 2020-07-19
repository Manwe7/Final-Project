using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class DefeatUI : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] protected Player _player;

    [SerializeField] protected GameObject  _defeatMenuPanel = null;

    [Header("Record on defeat panel")]
    [SerializeField] protected Text _defeatRecordText;

    private int _record;

    private void OnEnable()
    {
        _player.OnPlayerDefeated += Defeat;
    }

    private void OnDisable()
    {
        _player.OnPlayerDefeated -= Defeat;
    }

    private void Defeat()
    {        
        Time.timeScale = 0.5f;
        StartCoroutine(OpenDefeatMenu());
    }

    private IEnumerator OpenDefeatMenu()
    {
        yield return new WaitForSeconds(1.5f);
        
        _record = PlayerPrefs.GetInt("Record", 0);
        _defeatRecordText.text = _record.ToString();
        
        _defeatMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }    


    public void Exit()
    {
        SceneManager.LoadScene(StaticStringNames.Menu);
    }    
}
