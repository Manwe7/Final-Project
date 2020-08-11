using UnityEngine;
using Photon.Pun;

namespace PlayerOnlineScripts
{
    public class PlayerLivesOnlineSync : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

        private int _remainingLives;

        private void Start()
        {
            _remainingLives = 3;
            SetLives(_remainingLives);
        }

        private void SetLives(int value)
        {
            _myCustomProperties[ShowScoreOnline.LivesSaveKey] = value;
            PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
        }

        public void DecreaseOneLife()
        {
            _remainingLives -= 1;
            SetLives(_remainingLives);
        }

        public bool IsEnoughLives()
        {
            return _remainingLives > 0;
        }
    }
}
