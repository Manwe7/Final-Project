using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.DifficultyLevel
{
    public class DifficultyChooser : MonoBehaviour
    {
        [SerializeField] private Button[] _difficultyButtons;
        [SerializeField] private Image[] _buttonImages;

        [Space] 
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _menuButton;

        [Header("Scripts")] 
        [SerializeField] private SoundPlayer _soundPlayer;
    
        private void Awake()
        {
            AssignButtons();
        
            ChooseDifficulty(0);
        }

        private void AssignButtons()
        {
            _startButton.onClick.AddListener(StartGame);
            _menuButton.onClick.AddListener(OpenMenuScene);
            
            for (int i = 0; i < _difficultyButtons.Length; i++)
            {
                int num = i;
                _difficultyButtons[i].onClick.AddListener(() =>
                {
                    ChooseDifficulty(num);
                });
            }
        }

        private void StartGame()
        {
            _soundPlayer.Play(SoundNames.Button);
            StartCoroutine(LoadScene(SceneNames.Game));
        }

        private void OpenMenuScene()
        {
            _soundPlayer.Play(SoundNames.Button);
            StartCoroutine(LoadScene(SceneNames.Menu));
        }

        private void ChooseDifficulty(int difficultyNum)
        {
            PlayerPrefs.SetInt(SaveAttributes.DifficultyLevel, difficultyNum);

            ActivateButton(difficultyNum);
            
            _soundPlayer.Play(SoundNames.Button);
        }

        private void ActivateButton(int num)
        {
            foreach (var image in _buttonImages)
            {
                image.color = new Color32(0, 200, 255, 120);
            }

            _buttonImages[num].color = new Color32(0, 200, 255, 255);
        }
        
        private IEnumerator LoadScene(string scene)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            SceneManager.LoadScene(scene);
        }
    }
}
