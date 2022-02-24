using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpgrades : MonoBehaviour
{
    private int[] upgrade1Levels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade2Levels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade3Levels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade1MaxLevels = new int[] { 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19, 19 };
    private int[] upgrade2MaxLevels = new int[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
    private int[] upgrade3MaxLevels = new int[] { 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18 };
    private double[] upgrade1Prices = new double[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
    private double[] upgrade2Prices = new double[] { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
    private double[] upgrade3Prices = new double[] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
    private float[] upgrade1PriceIncrease = new float[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };
    private float[] upgrade2PriceIncrease = new float[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
    private float[] upgrade3PriceIncrease = new float[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };

    public Button[] upgrade1Buttons;
    public Button[] upgrade2Buttons;
    public Button[] upgrade3Buttons;
    public Button[] upgradeMax1Buttons;
    public Button[] upgradeMax2Buttons;
    public Button[] upgradeMax3Buttons;
    public Slider[] upgrade1Sliders;
    public Slider[] upgrade2Sliders;
    public Slider[] upgrade3Sliders;
    public TextMeshProUGUI[] upgrade1Costs;
    public TextMeshProUGUI[] upgrade2Costs;
    public TextMeshProUGUI[] upgrade3Costs;
    public TextMeshProUGUI[] upgrade1Descriptions;
    public TextMeshProUGUI[] upgrade2Descriptions;
    public TextMeshProUGUI[] upgrade3Descriptions;
    public GameObject ballSystem;
    public GameObject moneySystem;
    public GameObject soundSystem;
    public GameObject[] finishes;

    public void BuyMaxUpgrade1(int level)
    {
        double totalPrice = 0;
        int avaliableLevels = 0;
        for (int i = 0; i < upgrade1MaxLevels[level] - upgrade1Levels[level]; i++)
        {
            totalPrice += upgrade1Prices[level] * Mathf.Pow(upgrade1PriceIncrease[level], i);
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= totalPrice)
            {
                avaliableLevels++;
            }
        }
        for (int j = 0; j < avaliableLevels; j++)
        {
            BuyUpgrade1(level);
        }
    }

    public void BuyUpgrade1(int level)
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        upgrade1Levels[level]++;
        moneySystem.GetComponent<MoneySystem>().SpendMoney(upgrade1Prices[level]);
        upgrade1Prices[level] *= upgrade1PriceIncrease[level];
        for (int i = 0; i < upgrade1Levels.Length; i++)
        {
            Upgrade1Refresh(i);
        }
        moneySystem.GetComponent<BallScoring>().UpgradeLevelMul(level);
    }

    public void SetUpgrade1(int[] levels)
    {
        upgrade1Levels = levels;
        for (int i = 0; i < levels.Length; i++)
        {
            upgrade1Prices[i] = upgrade1Prices[i] * Mathf.Pow(upgrade1PriceIncrease[i], upgrade1Levels[i]);
            Upgrade1Refresh(i);
        }
        moneySystem.GetComponent<BallScoring>().SetLevelMuls(levels);
    }

    public int[] GetUpgrade1()
    {
        return upgrade1Levels;
    }

    public void BuyMaxUpgrade2(int level)
    {
        double totalPrice = 0;
        int avaliableLevels = 0;
        for (int i = 0; i < upgrade2MaxLevels[level] - upgrade2Levels[level]; i++)
        {
            totalPrice += upgrade2Prices[level] * Mathf.Pow(upgrade2PriceIncrease[level], i);
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= totalPrice)
            {
                avaliableLevels++;
            }
        }
        for (int j = 0; j < avaliableLevels; j++)
        {
            BuyUpgrade2(level);
        }
    }

    public void BuyUpgrade2(int level)
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        upgrade2Levels[level]++;
        moneySystem.GetComponent<MoneySystem>().SpendMoney(upgrade2Prices[level]);
        upgrade2Prices[level] *= upgrade2PriceIncrease[level];
        for (int i = 0; i < upgrade2Levels.Length; i++)
        {
            Upgrade2Refresh(i);
        }
        ballSystem.GetComponent<BallSystem>().UpgradeSpeedUp(level);
    }

    public void SetUpgrade2(int[] levels)
    {
        upgrade2Levels = levels;
        for (int i = 0; i < levels.Length; i++)
        {
            upgrade2Prices[i] = upgrade2Prices[i] * Mathf.Pow(upgrade2PriceIncrease[i], upgrade2Levels[i]);
            Upgrade2Refresh(i);
        }
        ballSystem.GetComponent<BallSystem>().SetSpeedUp(levels);
    }

    public int[] GetUpgrade2()
    {
        return upgrade2Levels;
    }

    public void BuyMaxUpgrade3(int level)
    {
        double totalPrice = 0;
        int avaliableLevels = 0;
        for (int i = 0; i < upgrade3MaxLevels[level] - upgrade3Levels[level]; i++)
        {
            totalPrice += upgrade3Prices[level] * Mathf.Pow(upgrade3PriceIncrease[level], i);
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= totalPrice)
            {
                avaliableLevels++;
            }
        }
        for (int j = 0; j < avaliableLevels; j++)
        {
            BuyUpgrade3(level);
        }
    }

    public void BuyUpgrade3(int level)
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
        upgrade3Levels[level]++;
        moneySystem.GetComponent<MoneySystem>().SpendMoney(upgrade3Prices[level]);
        upgrade3Prices[level] *= upgrade3PriceIncrease[level];
        for (int i = 0; i < upgrade3Levels.Length; i++)
        {
            Upgrade3Refresh(i);
        }
        foreach (var item in finishes)
        {
            item.GetComponent<CheckFinish>().SetDefaultMultiplier(upgrade3Levels);
        }
    }

    public void SetUpgrade3(int[] levels)
    {
        upgrade3Levels = levels;
        for (int i = 0; i < levels.Length; i++)
        {
            upgrade3Prices[i] = upgrade3Prices[i] * Mathf.Pow(upgrade3PriceIncrease[i], upgrade3Levels[i]);
            Upgrade3Refresh(i);
        }
        foreach (var item in finishes)
        {
            item.GetComponent<CheckFinish>().SetDefaultMultiplier(upgrade3Levels);
        }
    }

    public int[] GetUpgrade3()
    {
        return upgrade3Levels;
    }

    public void ResetUpgrades()
    {
        for (int i = 0; i < upgrade1Levels.Length; i++)
        {
            upgrade1Levels[i] = 0;
            upgrade2Levels[i] = 0;
            upgrade3Levels[i] = 0;
            upgrade1Sliders[i].value = 0;
            upgrade2Sliders[i].value = 0;
            upgrade3Sliders[i].value = 0;
            upgrade1Prices[i] = 5;
            upgrade2Prices[i] = 8;
            upgrade3Prices[i] = 15;
            upgrade1Costs[i].text = upgrade1Prices[i].ToString();
            upgrade2Costs[i].text = upgrade2Prices[i].ToString();
            upgrade3Costs[i].text = upgrade3Prices[i].ToString();
            RefreshUpgrades();         
            upgrade1Descriptions[i].text = "Base ball cost: (" + (upgrade1Levels[i] + 1) + " -> " + (upgrade1Levels[i] + 2) + ")";
            upgrade2Descriptions[i].text = "Time between ball drops: (" + (10 - upgrade2Levels[i] * 0.5f) + "s -> " + (10 - (upgrade2Levels[i] + 1) * 0.5f) + "s)";
            upgrade3Descriptions[i].text = "Increase finish value: (" + (1 + upgrade3Levels[i] * 0.5f) + "x -> " + (1 + (upgrade3Levels[i] + 1) * 0.5f) + "x)";
        }
        moneySystem.GetComponent<BallScoring>().ResetLevelMuls();
        ballSystem.GetComponent<BallSystem>().ResetSpeedUp();
        foreach (var item in finishes)
        {
            item.GetComponent<CheckFinish>().ResetDefaultMultiplier();
        }
    }

    public void RefreshUpgrades()
    {
        for (int i = 0; i < upgrade1Levels.Length; i++)
        {
            Upgrade1Refresh(i);
            Upgrade2Refresh(i);
            Upgrade3Refresh(i);
        }
    }

    private void Upgrade1Refresh(int level)
    {
        upgrade1Sliders[level].value = (float)upgrade1Levels[level] / (float)upgrade1MaxLevels[level];
        if (upgrade1Levels[level] >= upgrade1MaxLevels[level])
        {
            upgrade1Buttons[level].interactable = false;
            upgradeMax1Buttons[level].interactable = false;
            upgrade1Costs[level].text = "Maxed";
            upgrade1Descriptions[level].text = "Base ball cost";
        }
        else
        {
            upgrade1Descriptions[level].text = "Base ball cost: (" + (upgrade1Levels[level] + 1) + " -> " + (upgrade1Levels[level] + 2) + ")";
            if (upgrade1Prices[level] < 1000d)
            {
                upgrade1Costs[level].text = upgrade1Prices[level].ToString("F0");
            }
            else if (upgrade1Prices[level] < 1000000d)
            {
                upgrade1Costs[level].text = (upgrade1Prices[level] / 1000d).ToString("F2") + "K";
            }
            else if (upgrade1Prices[level] < 1000000000d)
            {
                upgrade1Costs[level].text = (upgrade1Prices[level] / 1000000d).ToString("F2") + "M";
            }
            else if (upgrade1Prices[level] < 1000000000000d)
            {
                upgrade1Costs[level].text = (upgrade1Prices[level] / 1000000000d).ToString("F2") + "B";
            }
            else
            {
                upgrade1Costs[level].text = (upgrade1Prices[level] / 1000000000000d).ToString("F2") + "T";
            }
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= upgrade1Prices[level])
            {
                upgrade1Buttons[level].interactable = true;
                upgradeMax1Buttons[level].interactable = true;
            }
            else
            {
                upgrade1Buttons[level].interactable = false;
                upgradeMax1Buttons[level].interactable = false;
            }
        }
    }

    private void Upgrade2Refresh(int level)
    {
        upgrade2Sliders[level].value = (float)upgrade2Levels[level] / (float)upgrade2MaxLevels[level];
        if (upgrade2Levels[level] >= upgrade2MaxLevels[level])
        {
            upgrade2Buttons[level].interactable = false;
            upgradeMax2Buttons[level].interactable = false;
            upgrade2Costs[level].text = "Maxed";
            upgrade2Descriptions[level].text = "Time between ball drops";
        }
        else
        {
            upgrade2Descriptions[level].text = "Time between ball drops: (" + (10 - upgrade2Levels[level] * 0.5f) + "s -> " + (10 - (upgrade2Levels[level] + 1) * 0.5f) + "s)";
            if (upgrade2Prices[level] < 1000d)
            {
                upgrade2Costs[level].text = upgrade2Prices[level].ToString("F0");
            }
            else if (upgrade2Prices[level] < 1000000d)
            {
                upgrade2Costs[level].text = (upgrade2Prices[level] / 1000d).ToString("F2") + "K";
            }
            else if (upgrade2Prices[level] < 1000000000d)
            {
                upgrade2Costs[level].text = (upgrade2Prices[level] / 1000000d).ToString("F2") + "M";
            }
            else if (upgrade2Prices[level] < 1000000000000d)
            {
                upgrade2Costs[level].text = (upgrade2Prices[level] / 1000000000d).ToString("F2") + "B";
            }
            else
            {
                upgrade2Costs[level].text = (upgrade2Prices[level] / 1000000000000d).ToString("F2") + "T";
            }
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= upgrade2Prices[level])
            {
                upgrade2Buttons[level].interactable = true;
                upgradeMax2Buttons[level].interactable = true;
            }
            else
            {
                upgrade2Buttons[level].interactable = false;
                upgradeMax2Buttons[level].interactable = false;
            }
        }
    }

    private void Upgrade3Refresh(int level)
    {
        upgrade3Sliders[level].value = (float)upgrade3Levels[level] / (float)upgrade3MaxLevels[level];
        if (upgrade3Levels[level] >= upgrade3MaxLevels[level])
        {
            upgrade3Buttons[level].interactable = false;
            upgradeMax3Buttons[level].interactable = false;
            upgrade3Costs[level].text = "Maxed";
            upgrade3Descriptions[level].text = "Increase finish value";
        }
        else
        {
            upgrade3Descriptions[level].text = "Increase finish value: (" + (1 + upgrade3Levels[level] * 0.5f) + "x -> " + (1 + (upgrade3Levels[level] + 1) * 0.5f) + "x)";
            if (upgrade3Prices[level] < 1000d)
            {
                upgrade3Costs[level].text = upgrade3Prices[level].ToString("F0");
            }
            else if (upgrade3Prices[level] < 1000000d)
            {
                upgrade3Costs[level].text = (upgrade3Prices[level] / 1000d).ToString("F2") + "K";
            }
            else if (upgrade3Prices[level] < 1000000000d)
            {
                upgrade3Costs[level].text = (upgrade3Prices[level] / 1000000d).ToString("F2") + "M";
            }
            else if (upgrade3Prices[level] < 1000000000000d)
            {
                upgrade3Costs[level].text = (upgrade3Prices[level] / 1000000000d).ToString("F2") + "B";
            }
            else
            {
                upgrade3Costs[level].text = (upgrade3Prices[level] / 1000000000000d).ToString("F2") + "T";
            }
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= upgrade3Prices[level])
            {
                upgrade3Buttons[level].interactable = true;
                upgradeMax3Buttons[level].interactable = true;
            }
            else
            {
                upgrade3Buttons[level].interactable = false;
                upgradeMax3Buttons[level].interactable = false;
            }
        }
    }
}