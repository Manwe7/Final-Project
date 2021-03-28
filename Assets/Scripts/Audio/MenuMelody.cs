using UnityEngine;
using UnityEngine.SceneManagement;

namespace Audio
{
    public class MenuMelody : MonoBehaviour
    {
        [TagSelector]
        [SerializeField] private string _bgTag;

        private GameObject[] _bgMelody;

        private void Awake()
        {
            _bgMelody = GameObject.FindGameObjectsWithTag(_bgTag);

            if (_bgMelody.Length > 1)
            {
                Destroy(gameObject);
            }

            SceneManager.activeSceneChanged += CheckScene;

            DontDestroyOnLoad(gameObject);
        }
    
        private void CheckScene(Scene currentScene, Scene newScene)
        {
            if(IsMenuScenes()) return;
        
            foreach (var t in _bgMelody)
            {
                Destroy(t);
            }
        }

        private bool IsMenuScenes()
        {
            return SceneManager.GetActiveScene().name == SceneNames.Menu ||
                   SceneManager.GetActiveScene().name == SceneNames.Lobby ||
                   SceneManager.GetActiveScene().name == SceneNames.Difficulty;
        }
    }
}
