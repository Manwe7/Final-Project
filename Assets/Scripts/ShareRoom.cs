using UnityEngine;
using UnityEngine.UI;

public class ShareRoom : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button _shareRoomButton;
    [SerializeField] private InputField _roomNameInput;

    private void Awake()
    {
        _shareRoomButton.onClick.AddListener(ShareRoomName);
    }
    
    private void ShareRoomName()
    {
        if(IsRoomNameEmpty()) return;
        
        new NativeShare()
			.SetSubject("Join the room").SetText("https://perfect-crawler-287812.web.app/" + "?room=" + _roomNameInput.text)
			.Share();
    }
    
    private bool IsRoomNameEmpty() => _roomNameInput.text == "";
}
