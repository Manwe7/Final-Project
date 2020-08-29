using UnityEngine;
using UnityEngine.UI;

public class ShareRoom : MonoBehaviour
{
    [SerializeField] private InputField _roomNameInput;

    private bool IsRoomNameEmpty()
    {
        return _roomNameInput.text == "";
    }

    public void ShareRoomName()
    {
        if(IsRoomNameEmpty()) return;
        
        new NativeShare()
			.SetSubject("Join the room").SetText("https://perfect-crawler-287812.web.app/" + "?room=" + _roomNameInput.text)
			.Share();
    }
}
