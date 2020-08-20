using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    public void SetToNormal()
    {
        SetTimeScale(1f);
    }

    public void SetToHalfSpeed()
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
