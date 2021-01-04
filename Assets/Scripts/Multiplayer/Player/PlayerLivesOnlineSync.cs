using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerLivesOnlineSync : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private PhotonView _photonView;

        private readonly ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
        private int _remainingLives;

        private void Awake()
        {
            _player.OnDefeat += DecreaseOneLife;
        }

        private void Start()
        {
            if(!_photonView.IsMine) return;

            _remainingLives = 3;
            SetLives(_remainingLives);
        }

        private void OnDestroy()
        {
            _player.OnDefeat += DecreaseOneLife;
        }

        private void SetLives(int value)
        {
            if(!_photonView.IsMine) return;
            
            _myCustomProperties[ShowScoreOnline.LivesSaveKey] = value;
            PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
        }

        private void DecreaseOneLife()
        {
            if(!_photonView.IsMine) return;
            
            _remainingLives -= 1;
            SetLives(_remainingLives);
        }

        public bool IsEnoughLives()
        {
            return _remainingLives > 0;
        }
    }
}
