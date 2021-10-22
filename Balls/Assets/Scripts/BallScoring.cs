using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoring : MonoBehaviour
{
    private double ballCost;
    private double globalMul;

    public GameObject UISystem;
    public GameObject[] finishes;
    public GameSettings settings;

    private void Start()
    {
        ballCost = settings.ballCost;        
    }

    public void InitializeValues()
    {
        globalMul = 1;
    }

    public void UpgradeGlobalMul()
    {
        globalMul *= 1.5f;
    }

    public void SetGlobalMul(int level)
    {
        globalMul = Mathf.Pow(1.5f, level);
    }

    public void UpgradeFinishMul()
    {
        foreach (var item in finishes)
        {
            item.GetComponent<CheckFinish>().IncreaseMultiplier();
        }
    }

    public void SetFinishMul(int level)
    {
        foreach (var item in finishes)
        {
            item.GetComponent<CheckFinish>().SetMultiplier(level);
        }
    }

    public double GetGlobalMul()
    {
        return globalMul;
    }

    public void ScoreBall(double multiplier)
    {
        GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul, true);
        UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul);
    }
}