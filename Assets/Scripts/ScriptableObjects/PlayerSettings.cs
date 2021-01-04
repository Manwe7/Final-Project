using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "PlayerSettings")]
    public class PlayerSettings : ScriptableObject
    {
        public float _moveSpeed;

        public float _flySpeed;
    }
}
