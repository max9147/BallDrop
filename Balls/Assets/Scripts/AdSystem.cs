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

    private float noAdTime = 0;

    private bool passiveAdReady = false;

    private string gameId = "4429195";

    private string rewardedVideo = "Rewarded_Android";
    private string adScreen = "Interstitial_Android";

    public GameObject UISystem;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    private void Update()
    {
        noAdTime += Time.deltaTime;
        if (noAdTime > 300)
        {
            passiveAdReady = true;
        }
    }

    public void ShowRewardedVideo(int id)
    {
        bonusID = id;
        Advertisement.Load(rewardedVideo);
        Advertisement.Show(rewardedVideo);
    }

    public void ShowAdScreen()
    {
        bonusID = 3;
        Analytics.CustomEvent("AdPassive");
        Advertisement.Load(adScreen);
        Advertisement.Show(adScreen);
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
        passiveAdReady = false;
        noAdTime = 0;
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

    public bool GetPassiveAdStatus()
    {
        return passiveAdReady;
    }
}