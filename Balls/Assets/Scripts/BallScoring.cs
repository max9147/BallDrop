using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallScoring : MonoBehaviour
{
    private double[] levelIncomes = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private double ballCost;
    private double globalMul;

    public GameObject UISystem;
    public GameObject[] finishes;
    public GameSettings settings;
    public TextMeshProUGUI[] levelIncomeTexts;

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

    public double[] GetLevelIncomes()
    {
        return levelIncomes;
    }

    public void SetLevelIncomes(double[] savedIncomes)
    {
        levelIncomes = savedIncomes;
        for (int i = 0; i < levelIncomes.Length; i++)
        {
            levelIncomeTexts[i].text = "Level income: $" + ConvertMoneyToString(levelIncomes[i]);
        }
    }

    public void ResetLevelIncomes()
    {
        for (int i = 0; i < levelIncomes.Length; i++)
        {
            levelIncomes[i] = 0;
            levelIncomeTexts[i].text = "Level income: $" + ConvertMoneyToString(levelIncomes[i]);
        }
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

    public void ScoreBall(double multiplier, string levelName)
    {
        GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul, true);
        UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul);
        switch (levelName)
        {
            case "LevelPochinko":
                levelIncomes[0] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[0].text = "Level income: $" + ConvertMoneyToString(levelIncomes[0]);
                break;
            case "LevelFunnel":
                levelIncomes[1] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[1].text = "Level income: $" + ConvertMoneyToString(levelIncomes[1]);
                break;
            case "LevelGaps":
                levelIncomes[2] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[2].text = "Level income: $" + ConvertMoneyToString(levelIncomes[2]);
                break;
            case "LevelSqueeze":
                levelIncomes[3] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[3].text = "Level income: $" + ConvertMoneyToString(levelIncomes[3]);
                break;
            case "LevelMills":
                levelIncomes[4] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[4].text = "Level income: $" + ConvertMoneyToString(levelIncomes[4]);
                break;
            case "LevelVerticals":
                levelIncomes[5] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[5].text = "Level income: $" + ConvertMoneyToString(levelIncomes[5]);
                break;
            case "LevelMovement":
                levelIncomes[6] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[6].text = "Level income: $" + ConvertMoneyToString(levelIncomes[6]);
                break;
            case "LevelTraps":
                levelIncomes[7] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[7].text = "Level income: $" + ConvertMoneyToString(levelIncomes[7]);
                break;
            case "LevelMixer":
                levelIncomes[8] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[8].text = "Level income: $" + ConvertMoneyToString(levelIncomes[8]);
                break;
            case "LevelElevator":
                levelIncomes[9] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[9].text = "Level income: $" + ConvertMoneyToString(levelIncomes[9]);
                break;
            case "LevelBoulders":
                levelIncomes[10] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[10].text = "Level income: $" + ConvertMoneyToString(levelIncomes[10]);
                break;
            case "LevelShrink":
                levelIncomes[11] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[11].text = "Level income: $" + ConvertMoneyToString(levelIncomes[11]);
                break;
            case "LevelPlatforms":
                levelIncomes[12] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[12].text = "Level income: $" + ConvertMoneyToString(levelIncomes[12]);
                break;
            case "LevelDiamonds":
                levelIncomes[13] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[13].text = "Level income: $" + ConvertMoneyToString(levelIncomes[13]);
                break;
            case "LevelSpinner":
                levelIncomes[14] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[14].text = "Level income: $" + ConvertMoneyToString(levelIncomes[14]);
                break;
            case "LevelChoise":
                levelIncomes[15] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[15].text = "Level income: $" + ConvertMoneyToString(levelIncomes[15]);
                break;
            case "LevelZigzag":
                levelIncomes[16] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[16].text = "Level income: $" + ConvertMoneyToString(levelIncomes[16]);
                break;
            case "LevelFinal":
                levelIncomes[17] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul;
                levelIncomeTexts[17].text = "Level income: $" + ConvertMoneyToString(levelIncomes[17]);
                break;
            default:
                break;
        }
    }

    private string ConvertMoneyToString(double money)
    {
        string moneyString;
        if (money < 100d)
        {
            moneyString = money.ToString("F2");
        }
        else if (money < 1000d)
        {
            moneyString = money.ToString("F0");
        }
        else if (money < 1000000d)
        {
            moneyString = (money / 1000d).ToString("F2") + "K";
        }
        else if (money < 1000000000d)
        {
            moneyString = (money / 1000000d).ToString("F2") + "M";
        }
        else if (money < 1000000000000d)
        {
            moneyString = (money / 1000000000d).ToString("F2") + "B";
        }
        else
        {
            moneyString = (money / 1000000000000d).ToString("F2") + "T";
        }
        return moneyString;
    }
}