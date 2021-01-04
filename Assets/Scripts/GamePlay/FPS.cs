using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField] private Text _fpsText;
    private float _deltaTime;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        ShowFPS();
    }

    private void ShowFPS()
    {
        _deltaTime += (Time.deltaTime - _deltaTime) * 0.1f;
        var fps = 1.0f / _deltaTime;
        _fpsText.text = $"{Mathf.Ceil(fps)}";
    }
}
