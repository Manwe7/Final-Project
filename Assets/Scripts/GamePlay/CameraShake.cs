using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
	[SerializeField] private float _shakeDuration;  // Time the Camera Shake effect will last
    [SerializeField] private float _shakeAmplitude; // Cinemachine Noise Profile Parameter
    [SerializeField] private float _shakeFrequency; // Cinemachine Noise Profile Parameter

    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private CinemachineBasicMultiChannelPerlin _virtualCameraNoise;
    private float _shakeElapsedTime;
    private bool _shakeOnce;
    
	private void Start()
    {
        // Get Virtual Camera Noise Profile
        if (_virtualCamera != null)
        {
            _virtualCameraNoise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

	private void Update()
    {
        if (_shakeOnce)
        {
            _shakeElapsedTime = _shakeDuration;
			_shakeOnce = false;
        }

        // If the Cinemachine component is not set, avoid update
        if (_virtualCamera == null || _virtualCameraNoise == null) return;
        
        
        // If Camera Shake effect is still playing
        if (_shakeElapsedTime > 0)
        {
            // Set Cinemachine Camera Noise parameters
            _virtualCameraNoise.m_AmplitudeGain = _shakeAmplitude;
            _virtualCameraNoise.m_FrequencyGain = _shakeFrequency;
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

    public void ShakeCameraOnce(float shakePower)
    {
        _shakeAmplitude = shakePower;
        _shakeOnce = true;
    }
}
