using System.Collections;
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
        [SerializeField] private SoundPlayer _soundPlayer;
        
        public void Resume()
        {
            _soundPlayer.Play(SoundNames.Button);
            _pauseMenuPanel.SetActive(false);
            _gameSpeed.ResumeTime();
        }

        public void Restart()
        {
            _soundPlayer.Play(SoundNames.Button);
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
            _gameSpeed.ResumeTime();
        }

        public void Exit()
        {
            _soundPlayer.Play(SoundNames.Button);
            StartCoroutine(LoadScene(SceneNames.Difficulty));
        }
        
        private IEnumerator LoadScene(string scene)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            SceneManager.LoadScene(scene);
        }
    }
}