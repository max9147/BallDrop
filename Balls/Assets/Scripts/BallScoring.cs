using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoring : MonoBehaviour
{
    private double ballCost;
    private double globalMul;

    public GameObject UISystem;
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
        globalMul *= 2;
    }

    public void SetGlobalMul(int level)
    {
        globalMul = Mathf.Pow(2, level);
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