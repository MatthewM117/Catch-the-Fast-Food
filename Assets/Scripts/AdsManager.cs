using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    Action onRewardedAdSuccess;

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("4610614");
        Advertisement.AddListener(this);
    }

    public void PlayAd()
    {
        if (Advertisement.IsReady("Interstitial_iOS"))
        {
            Advertisement.Show("Interstitial_iOS");
        }
    }

    public void PlayRewardedAd(Action onSuccess)
    {
        onRewardedAdSuccess = onSuccess;
        if (Advertisement.IsReady("Rewarded_iOS"))
        {
            Advertisement.Show("Rewarded_iOS");
        }
        else
        {
            Debug.Log("Rewarded ad not ready");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("ADS ARE READY");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("video started");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == "Rewarded_iOS" && showResult == ShowResult.Finished)
        {
            onRewardedAdSuccess.Invoke();
        }
    }
}
