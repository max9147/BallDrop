using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScoring : MonoBehaviour
{
    private float ballCost;

    public GameSettings settings;

    private void Start()
    {
        ballCost = settings.ballCost;
    }

    public void ScoreBall(float multiplier)
    {
        GetComponent<MoneySystem>().AddMoney(ballCost * multiplier, true);
    }
}