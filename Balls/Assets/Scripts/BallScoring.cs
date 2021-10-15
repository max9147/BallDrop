using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoring : MonoBehaviour
{
    private double ballCost;

    private void Start()
    {
        ballCost = 1d;
    }

    public void ScoreBall(float multiplier)
    {
        GetComponent<MoneySystem>().AddMoney(ballCost * multiplier, true);
    }
}