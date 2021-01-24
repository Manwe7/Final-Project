using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class GamePlayButtons : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject _pauseMenuPanel = null;

        [Header("Scripts")]
        [SerializeField] private GameSpeed _gameSpeed = null;
        
        public void Resume()
        {
            _pauseMenuPanel.SetActive(false);
            _gameSpeed.ResumeTime();
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            _gameSpeed.ResumeTime();
        }

        public void Exit()
        {
            SceneManager.LoadScene(SceneNames.Menu);
        }
    }
}