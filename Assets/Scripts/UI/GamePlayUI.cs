using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] protected GameObject _pauseMenuPanel = null;

    [Header("Score in Game")]
    [SerializeField] private Text _scoreText;

    public void PauseBtn()
    {        
        _pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;        
    }

    public void SetScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }
}
