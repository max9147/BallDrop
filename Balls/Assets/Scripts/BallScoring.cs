using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoring : MonoBehaviour
{
    private int ballCost;

    private void Start()
    {
        ballCost = 1;
    }

    public void ScoreBall(float multiplier)
    {
        GetComponent<MoneySystem>().AddMoney(ballCost * multiplier);
    }
}