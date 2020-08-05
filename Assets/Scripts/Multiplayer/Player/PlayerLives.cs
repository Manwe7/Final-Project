using UnityEngine;
using Photon.Pun;

namespace PlayerOfflineScipts
{
    public class PlayerLives : MonoBehaviour
    {
        [SerializeField] private PlayerOnlineScripts.Player _player;

        private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

        private void Start()
        {
            SetLives(3);
        }

        private void OnEnable()
        {
            _player.OnLivesChange += SetLives;
        }

        private void OnDisable()
        {
            _player.OnLivesChange -= SetLives;
        }

        private void SetLives(int value)
        {
            //_remainingLives = value;
            _myCustomProperties[ShowScoreOnline.healthSaveKey] = value; //_remainingLives;
            PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
        }

    }
}
