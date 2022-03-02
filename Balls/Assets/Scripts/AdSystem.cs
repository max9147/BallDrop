using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class AdSystem : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool testMode = false;

    private int bonusID;

    private string gameId = "4429195";

    private string rewardedVideo = "Rewarded_Android";

    public GameObject UISystem;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowRewardedVideo(int id)
    {
        bonusID = id;
        Advertisement.Load(rewardedVideo);
        Advertisement.Show(rewardedVideo);
    }

    public void OnUnityAdsReady(string placementId)
    {        

    }

    public void OnUnityAdsDidError(string message)
    {

    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            switch (bonusID)
            {
                case 0:
                    UISystem.GetComponent<AdDouble>().AdCompleted();
                    Analytics.CustomEvent("AdDouble");
                    break;
                case 1:
                    UISystem.GetComponent<PrestigeSystem>().AdCompleted();
                    Analytics.CustomEvent("AdPrestige");
                    break;
                case 2:
                    UISystem.GetComponent<OfflineIncome>().AdCompleted();
                    Analytics.CustomEvent("AdOffline");
                    break;
                default:
                    break;
            }
        }
    }
}