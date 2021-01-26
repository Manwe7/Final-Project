using BaseClasses;
using UnityEngine;

namespace Enemy
{
    public class MegaEnemy : BaseEnemy
    {
        protected override int ScoreWeight => 15;

        private void OnEnable()
        {
            _health = Random.Range(50, 60);
            _distance = Random.Range(3f, 12f);
        }
 
    }
}
