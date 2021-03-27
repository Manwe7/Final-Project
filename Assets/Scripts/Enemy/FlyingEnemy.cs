using BaseClasses;
using UnityEngine;

namespace Enemy
{
    public class FlyingEnemy : BaseEnemy
    {
        protected override int ScoreWeight => 15;

        private void OnEnable()
        {
            _health = Random.Range(50, 60);
            _distance = Random.Range(3f, 12f);
        }

        private void Update()
        {
            //Empty Override to prevent movement logic
        }

        private void FixedUpdate()
        {
            //Empty Override to prevent movement logic
        }
    }
}