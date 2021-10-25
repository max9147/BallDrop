using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallScoring : MonoBehaviour
{
    private double[] levelIncomes = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private double ballCost;
    private double globalMul;
    private float[] levelMuls = new float[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

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

    public void UpgradeLevelMul(int level)
    {
        levelMuls[level]++;
    }

    public void SetLevelMuls(int[] levels)
    {
        for (int i = 0; i < levelMuls.Length; i++)
        {
            levelMuls[i] += levels[i];
        }
    }

    public void ResetLevelMuls()
    {
        for (int i = 0; i < levelMuls.Length; i++)
        {
            levelMuls[i] = 1;
        }
    }

    public void ScoreBall(double multiplier, string levelName)
    {
        switch (levelName)
        {
            case "LevelPochinko":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[0], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[0]);
                levelIncomes[0] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[0];
                levelIncomeTexts[0].text = "Level income: $" + ConvertMoneyToString(levelIncomes[0]);
                break;
            case "LevelFunnel":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[1], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[1]);
                levelIncomes[1] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[1];
                levelIncomeTexts[1].text = "Level income: $" + ConvertMoneyToString(levelIncomes[1]);
                break;
            case "LevelGaps":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[2], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[2]);
                levelIncomes[2] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[2];
                levelIncomeTexts[2].text = "Level income: $" + ConvertMoneyToString(levelIncomes[2]);
                break;
            case "LevelSqueeze":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[3], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[3]);
                levelIncomes[3] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[3];
                levelIncomeTexts[3].text = "Level income: $" + ConvertMoneyToString(levelIncomes[3]);
                break;
            case "LevelMills":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[4], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[4]);
                levelIncomes[4] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[4];
                levelIncomeTexts[4].text = "Level income: $" + ConvertMoneyToString(levelIncomes[4]);
                break;
            case "LevelVerticals":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[5], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[5]);
                levelIncomes[5] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[5];
                levelIncomeTexts[5].text = "Level income: $" + ConvertMoneyToString(levelIncomes[5]);
                break;
            case "LevelMovement":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[6], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[6]);
                levelIncomes[6] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[6];
                levelIncomeTexts[6].text = "Level income: $" + ConvertMoneyToString(levelIncomes[6]);
                break;
            case "LevelTraps":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[7], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[7]);
                levelIncomes[7] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[7];
                levelIncomeTexts[7].text = "Level income: $" + ConvertMoneyToString(levelIncomes[7]);
                break;
            case "LevelMixer":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[8], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[8]);
                levelIncomes[8] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[8];
                levelIncomeTexts[8].text = "Level income: $" + ConvertMoneyToString(levelIncomes[8]);
                break;
            case "LevelElevator":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[9], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[9]);
                levelIncomes[9] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[9];
                levelIncomeTexts[9].text = "Level income: $" + ConvertMoneyToString(levelIncomes[9]);
                break;
            case "LevelBoulders":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[10], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[10]);
                levelIncomes[10] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[10];
                levelIncomeTexts[10].text = "Level income: $" + ConvertMoneyToString(levelIncomes[10]);
                break;
            case "LevelShrink":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[11], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[11]);
                levelIncomes[11] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[11];
                levelIncomeTexts[11].text = "Level income: $" + ConvertMoneyToString(levelIncomes[11]);
                break;
            case "LevelPlatforms":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[12], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[12]);
                levelIncomes[12] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[12];
                levelIncomeTexts[12].text = "Level income: $" + ConvertMoneyToString(levelIncomes[12]);
                break;
            case "LevelDiamonds":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[13], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[13]);
                levelIncomes[13] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[13];
                levelIncomeTexts[13].text = "Level income: $" + ConvertMoneyToString(levelIncomes[13]);
                break;
            case "LevelSpinner":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[14], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[14]);
                levelIncomes[14] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[14];
                levelIncomeTexts[14].text = "Level income: $" + ConvertMoneyToString(levelIncomes[14]);
                break;
            case "LevelChoise":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[15], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[15]);
                levelIncomes[15] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[15];
                levelIncomeTexts[15].text = "Level income: $" + ConvertMoneyToString(levelIncomes[15]);
                break;
            case "LevelZigzag":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[16], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[16]);
                levelIncomes[16] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[16];
                levelIncomeTexts[16].text = "Level income: $" + ConvertMoneyToString(levelIncomes[16]);
                break;
            case "LevelFinal":
                GetComponent<MoneySystem>().AddMoney(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[17], true);
                UISystem.GetComponent<PrestigeSystem>().AddTotalEarnings(ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[17]);
                levelIncomes[17] += ballCost * multiplier * UISystem.GetComponent<PrestigeSystem>().GetBallValueBoost() / 100 * globalMul * levelMuls[17];
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