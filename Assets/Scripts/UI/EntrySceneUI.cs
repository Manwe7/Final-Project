using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EntrySceneUI : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
    
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    
        public void LoadMenuScene()
        {
            SceneManager.LoadScene(SceneNames.Menu);
        }

        public IEnumerator PlayFadeInAnimation()
        {
            yield return new WaitForSeconds(1f);
        
            _animator.SetTrigger(FadeIn);
        }
    }
}
