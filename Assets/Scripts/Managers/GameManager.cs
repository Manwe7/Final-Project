using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{    
    //why do you set them as NULL?
    [SerializeField] Text ScoreText = null, defeatRecordText = null;
    
    //naming.. does not sound as English
    //_isNewRecord sounds better
    private bool _recordIsNew = false;    
    private float _oldRecord;
    
    public float CurrentScore;

    #region Singleton 
    public static GameManager gameManagerInstance;
    //singleton is not complete. refer to my ObjecPoolManager example
    private void Awake()
    {
        Application.targetFrameRate = 60;
        gameManagerInstance = this;
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
            _recordIsNew = true;
        }

        defeatRecordText.text = _oldRecord.ToString();

        ScoreText.text = CurrentScore.ToString();          
    }    
    
    public void SaveRecord()
    {
        //simply if(_recordIsNew)
        if (_recordIsNew == true)
        {
            PlayerPrefs.SetFloat("ScoreRecord", CurrentScore);
        }
    }
}
