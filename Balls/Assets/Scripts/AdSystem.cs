using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdSystem : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool testMode = true;
    [SerializeField] private Button[] adsButtons;

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
        Advertisement.Show(rewardedVideo);
    }

    public void OnUnityAdsReady(string placementId)
    {        

    }

    public void OnUnityAdsDidError(string message)
    {
        Time.timeScale = 1;
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Time.timeScale = 0;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        Time.timeScale = 1;
        if (showResult == ShowResult.Finished)
        {
            switch (bonusID)
            {
                case 0:
                    UISystem.GetComponent<AdDouble>().AdCompleted();
                    break;
                case 1:
                    UISystem.GetComponent<PrestigeSystem>().AdCompleted();
                    break;
                case 2:
                    UISystem.GetComponent<OfflineIncome>().AdCompleted();
                    break;
                default:
                    break;
            }
        }
        else if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Skipped");
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.Log("Failed");
        }
    }
}