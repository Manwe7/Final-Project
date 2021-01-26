using UnityEngine;
using UnityEngine.UI;

namespace Multiplayer.Player
{
    public class PlayerOnlineProperties : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
    
        [Header("Sliders")]
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private Slider _fuelSlider;
    
        [Header("Joysticks")]
        [SerializeField] private Joystick _movementJoystick;
        [SerializeField] private Joystick _weaponJoystick;
        [SerializeField] private RectTransform _joystickHandle;
    
        [Header("End panel")]
        [SerializeField] private GameObject _endPanel;
        [SerializeField] private Text _endPanelText;

        public Camera Camera => _camera;

        public Slider HealthSlider => _healthSlider;
        public Slider FuelSlider => _fuelSlider;

        public Joystick MovementJoystick => _movementJoystick;
        public Joystick WeaponJoystick => _weaponJoystick;

        public RectTransform JoystickHandle => _joystickHandle;

        public GameObject EndPanel => _endPanel;
        public Text EndPanelText => _endPanelText;
    }
}
