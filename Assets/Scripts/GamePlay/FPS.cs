using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [SerializeField] private Text _fpsText = null;
    private float deltaTime;

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
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        _fpsText.text = Mathf.Ceil(fps).ToString();
    }
}
