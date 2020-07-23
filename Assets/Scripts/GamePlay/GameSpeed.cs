using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    public void ToNormal()
    {
        SetTimeScale(1f);
    }

    public void ToHalfSpeed()
    {
        SetTimeScale(0.5f);
    }

    public void Stop()
    {
        SetTimeScale(0f);
    }

    private void SetTimeScale(float value)
    {
        Time.timeScale = value;
    }
}
