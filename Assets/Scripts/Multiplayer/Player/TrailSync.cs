using UnityEngine;

namespace Multiplayer.Player
{
    public class TrailSync : MonoBehaviour
    {
        [SerializeField] private GameObject _trial;
        
        private Vector3 _lastPos;
        private const float Threshold = 0f;

        private void Start()
        {
            _lastPos = transform.position;
        }

        private void Update()
        {
            Vector2 diff = transform.position - _lastPos;

            _lastPos = transform.position;
            
            _trial.SetActive(Mathf.Abs(diff.y) > Threshold);
        }
    }
}
