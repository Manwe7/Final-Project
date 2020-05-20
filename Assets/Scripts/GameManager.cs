using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{    
    [SerializeField] Text ScoreText = null, defeatRecordText = null;
    
    private bool _recordIsNew = false;    
    private float _oldRecord;
    
    public GameObject _player;
    public float CurrentScore;

    #region Singleton 
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

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

        defeatRecordText.text = _oldRecord.ToString();

        ScoreText.text = CurrentScore.ToString();          
    }    
    
    public void SaveRecord()
    {
        if (_recordIsNew == true)
        {
            PlayerPrefs.SetFloat("ScoreRecord", CurrentScore);
        }
    }
}
