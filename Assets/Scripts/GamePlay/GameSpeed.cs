using UnityEngine;

namespace GamePlay
{
    public class GameSpeed : MonoBehaviour
    {
        private void Awake()
        {
            ResumeTime();
        }

        public void ResumeTime()
        {
            SetTimeScale(1f);
        }

        public void SetToHalfSpeed()
        {
            SetTimeScale(0.5f);
        }

        public void StopTime()
        {
            SetTimeScale(0f);
        }

        private static void SetTimeScale(float value)
        {
            Time.timeScale = value;
        }
    }
}
