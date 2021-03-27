using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playSoloButton;
        [SerializeField] private Button _openLobbyButton;

        private void Awake()
        {
            _playSoloButton.onClick.AddListener(Play);
            _openLobbyButton.onClick.AddListener(Lobby);
        }

        private void Play()
        {
            SceneManager.LoadScene(SceneNames.Game);
        }

        private void Lobby()
        {
            SceneManager.LoadScene(SceneNames.Lobby);
        }
    }
}
