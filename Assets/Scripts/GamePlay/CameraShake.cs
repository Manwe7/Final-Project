using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
	[SerializeField] private float ShakeDuration = 0.3f;  // Time the Camera Shake effect will last
    [SerializeField] private float ShakeAmplitude = 1.2f; // Cinemachine Noise Profile Parameter
    [SerializeField] private float ShakeFrequency = 2.0f; // Cinemachine Noise Profile Parameter

    [SerializeField] private CinemachineVirtualCamera VirtualCamera = null;

    private CinemachineBasicMultiChannelPerlin virtualCameraNoise = null;
    private float ShakeElapsedTime = 0f;
    
    public static bool ShakeOnce = false;// remove static
    
	private void Start()
    {
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
        {
            virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

	private void Update()
    {
        if (ShakeOnce == true)
        {
            ShakeElapsedTime = ShakeDuration;
			ShakeOnce = false;
        }

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (ShakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
				ShakeOnce = false;
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }
}
