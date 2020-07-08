using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("Score in Game")]
    [SerializeField] Text ScoreText = null;
    
    [Header("Record on defeat panel")]
    [SerializeField] Text defeatRecordText = null;
    
    private bool _isNewRecord = false;    
    private float _oldRecord;
    
    public float CurrentScore;
    
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    #region (Un)Subscribe to player defeat event
    private void OnEnable()
    {
        Player.defeated += SaveRecord;
    }

    private void OnDisable()
    {
        Player.defeated -= SaveRecord;
    }
    #endregion

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
