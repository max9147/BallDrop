using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdDouble : MonoBehaviour
{
    private double adTime = 0;
    private bool adActive = false;

    public GameObject adSystem;
    public GameObject moneySystem;
    public GameObject adButton;
    public GameObject adCounter;
    public GameObject adCircle;

    private void FixedUpdate()
    {
        if (adTime <= 0 && adActive)
        {
            moneySystem.GetComponent<BallScoring>().setAdMul(1);
            adActive = false;
            adButton.SetActive(true);
            adCounter.SetActive(false);
            adTime = 0;
        }
        else if (adActive)
        {
            adTime -= Time.deltaTime;
            adCircle.GetComponent<Image>().fillAmount = (float)((7200d - adTime) / 7200d);
        }
    }

    public void WatchAd()
    {
        adSystem.GetComponent<AdSystem>().ShowRewardedVideo(0);
    }

    public void AdCompleted()
    {
        moneySystem.GetComponent<BallScoring>().setAdMul(2);
        adActive = true;
        adButton.SetActive(false);
        adCounter.SetActive(true);
        adTime = 7200d;
    }

    public double GetAdTime()
    {
        return adTime;
    }

    public void SetAdTime(double savedTime)
    {
        moneySystem.GetComponent<BallScoring>().setAdMul(2);
        adTime = savedTime;
        adActive = true;
        adButton.SetActive(false);
        adCounter.SetActive(true);
    }
}