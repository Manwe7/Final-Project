using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private GameSpeed _gameSpeed;

    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _adsPanel;

    public event Action OnAdWatched;
        
    private void Awake()
    {
        //Set ADS
        Advertisement.AddListener(this);
        Advertisement.Initialize("3772605", true);
    }

    public void WatchAnAd(string p)
    {
        if (Advertisement.IsReady("3772605"))
        {
            //Set ADS
            Advertisement.AddListener(this);
            Advertisement.Initialize("3772605", true);
        }

        Advertisement.Show(p);
    }

    #region Unity ads

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            _adsPanel.SetActive(false);

            _player.SetActive(true);
            _player.transform.position = new Vector3(0, 0, 0);
            
            OnAdWatched?.Invoke();
            _gameSpeed.SetToNormal();            
        }
        else if(showResult == ShowResult.Failed)
        {
            //Something went wrong
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        
    }    

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }
    #endregion
}
