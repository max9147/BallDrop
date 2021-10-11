using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoring : MonoBehaviour
{
    private float ballCost;

    private void Start()
    {
        ballCost = 1f;
    }

    public void ScoreBall(float multiplier)
    {
        GetComponent<MoneySystem>().AddMoney(ballCost * multiplier);
    }
}