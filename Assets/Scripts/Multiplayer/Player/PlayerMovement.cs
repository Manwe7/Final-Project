using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PhotonView _photonView;        

        [SerializeField] private Rigidbody2D _rigidbody2D = null;

        [SerializeField] private float _moveSpeed;

        private FixedJoystick _joystick = null;

        private float _horizontalMove;

        private void Awake()
        {
            _joystick = GameObject.Find("Canvas/MovementJoystick").GetComponent<FixedJoystick>();
        }

        private void Update()
        {
            if(!_photonView.IsMine) { return; }

            SetDirections();
        }

        private void FixedUpdate()
        {
            if(!_photonView.IsMine) { return; }

            HorizontalMovement();
        }

        private void SetDirections()
        {            
            _horizontalMove = _joystick.Horizontal;
        }

        private void HorizontalMovement()
        {
            _rigidbody2D.velocity = new Vector2(_horizontalMove * _moveSpeed, _rigidbody2D.velocity.y);
        }
    }
}
