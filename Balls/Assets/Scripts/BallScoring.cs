using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoring : MonoBehaviour
{
    private double ballCost;

    public GameObject UISystem;
    public GameSettings settings;

    private void Start()
    {
        ballCost = settings.ballCost;
    }

    public void ScoreBall(double multiplier)
    {
        GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100, true);
        UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100);
    }
}