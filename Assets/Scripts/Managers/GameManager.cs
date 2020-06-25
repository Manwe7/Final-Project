using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{    
    [SerializeField] Text ScoreText = null, defeatRecordText = null;
    
    private bool _isNewRecord = false;    
    private float _oldRecord;
    
    public float CurrentScore;

    #region Singleton 
    public static GameManager gameManagerInstance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void OnEnable()
    {
        Player.defeated += SaveRecord;
    }

    private void OnDisable()
    {
        Player.defeated -= SaveRecord;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        _oldRecord = PlayerPrefs.GetFloat("ScoreRecord", 0);

        CurrentScore = 0;
    }

    private void Update()
    {
        //If record is broken, update it 
        if (_oldRecord < CurrentScore)
        {
            _oldRecord = CurrentScore;
            _isNewRecord = true;
        }

        defeatRecordText.text = _oldRecord.ToString();

        ScoreText.text = CurrentScore.ToString();          
    }    
    
    public void SaveRecord()
    {        
        if (_isNewRecord)
        {
            PlayerPrefs.SetFloat("ScoreRecord", CurrentScore);
        }
    }
}
