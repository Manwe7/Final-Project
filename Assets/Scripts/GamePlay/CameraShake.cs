using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
	[SerializeField] private float ShakeDuration = 0.3f;  // Time the Camera Shake effect will last
    [SerializeField] private float ShakeAmplitude = 1.2f; // Cinemachine Noise Profile Parameter
    [SerializeField] private float ShakeFrequency = 2.0f; // Cinemachine Noise Profile Parameter

    [SerializeField] private CinemachineVirtualCamera VirtualCamera = null;

    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise = null;
    private float _shakeElapsedTime;
    private bool _shakeOnce = false;
    
	private void Start()
    {
        // Get Virtual Camera Noise Profile
        if (VirtualCamera != null)
        {
            _virtualCameraNoise = VirtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

	private void Update()
    {
        if (_shakeOnce == true)
        {
            _shakeElapsedTime = ShakeDuration;
			_shakeOnce = false;
        }

        // If the Cinemachine componet is not set, avoid update
        if (VirtualCamera != null && _virtualCameraNoise != null)
        {
            // If Camera Shake effect is still playing
            if (_shakeElapsedTime > 0)
            {
                // Set Cinemachine Camera Noise parameters
                _virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
                _virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
                // Update Shake Timer
                _shakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
				_shakeOnce = false;
                _virtualCameraNoise.m_AmplitudeGain = 0f;
                _shakeElapsedTime = 0f;
            }
        }
    }

    public void ShakeCameraOnce()
    {
        _shakeOnce = true;
    }
}
