using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdDouble : MonoBehaviour
{
    private double adTime = 0;
    private bool adActive = false;

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
            adCircle.GetComponent<Image>().fillAmount = (float)((50d - adTime) / 50d);
        }
    }

    public void WatchAd()
    {
        moneySystem.GetComponent<BallScoring>().setAdMul(2);
        adActive = true;
        adButton.SetActive(false);
        adCounter.SetActive(true);
        adTime = 50f;
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