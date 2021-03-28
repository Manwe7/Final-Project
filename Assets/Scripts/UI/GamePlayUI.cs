using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GamePlayUI : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] protected GameObject _pauseMenuPanel = null;

        [Header("Score in Game")]
        [SerializeField] private Text _scoreText;

        [Header("Scripts")]
        [SerializeField] private GameSpeed _gameSpeed;
        [SerializeField] private SoundPlayer _soundPlayer;

        public void PauseBtn()
        {        
            _pauseMenuPanel.SetActive(true);
            _gameSpeed.StopTime();
            
            _soundPlayer.Play(SoundNames.Button);
        }

        public void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }
    }
}
