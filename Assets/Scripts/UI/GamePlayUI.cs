using System;
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

        public void PauseBtn()
        {        
            _pauseMenuPanel.SetActive(true);
            _gameSpeed.StopTime();

            OnGamePause?.Invoke();
        }

        public void SetScoreText(int score)
        {
            _scoreText.text = score.ToString();
        }

        public event Action OnGamePause;
    }
}
