using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playSoloButton;
        [SerializeField] private Button _openLobbyButton;

        [Header("Scripts")]
        [SerializeField] private SoundPlayer _soundPlayer;

        private void Awake()
        {
            _playSoloButton.onClick.AddListener(Play);
            _openLobbyButton.onClick.AddListener(Lobby);
        }

        private void Play()
        {
            _soundPlayer.Play(SoundNames.Button);
            StartCoroutine(LoadScene(SceneNames.Difficulty));
        }

        private void Lobby()
        {
            _soundPlayer.Play(SoundNames.Button);
            StartCoroutine(LoadScene(SceneNames.Lobby));
        }

        private IEnumerator LoadScene(string scene)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            SceneManager.LoadScene(scene);
        }
    }
}
