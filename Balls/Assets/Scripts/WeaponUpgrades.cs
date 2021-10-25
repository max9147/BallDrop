using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrades : MonoBehaviour
{
    private int[] upgrade1Levels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade2Levels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade3Levels = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade1MaxLevels = new int[] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 };
    private int[] upgrade2MaxLevels = new int[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
    private int[] upgrade3MaxLevels = new int[] { 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18 };
    private double[] upgrade1Prices = new double[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
    private double[] upgrade2Prices = new double[] { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8 };
    private double[] upgrade3Prices = new double[] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };
    private float[] upgrade1PriceIncrease = new float[] { 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17, 17 };
    private float[] upgrade2PriceIncrease = new float[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
    private float[] upgrade3PriceIncrease = new float[] { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 };

    public Button[] upgrade1Buttons;
    public Button[] upgrade2Buttons;
    public Button[] upgrade3Buttons;
    public Slider[] upgrade1Sliders;
    public Slider[] upgrade2Sliders;
    public Slider[] upgrade3Sliders;
    public TextMeshProUGUI[] upgrade1Costs;
    public TextMeshProUGUI[] upgrade2Costs;
    public TextMeshProUGUI[] upgrade3Costs;
    public GameObject moneySystem;

    public void BuyUpgrade1(int weapon)
    {
        upgrade1Levels[weapon]++;
        moneySystem.GetComponent<MoneySystem>().SpendMoney(upgrade1Prices[weapon]);
        upgrade1Prices[weapon] *= upgrade1PriceIncrease[weapon];
        for (int i = 0; i < upgrade1Levels.Length; i++)
        {
            Upgrade1Refresh(i);
        }
    }

    public void SetUpgrade1(int[] weapons)
    {
        upgrade1Levels = weapons;
        for (int i = 0; i < weapons.Length; i++)
        {
            upgrade1Prices[i] = upgrade1Prices[i] * Mathf.Pow(upgrade1PriceIncrease[i], upgrade1Levels[i]);
            Upgrade1Refresh(i);
        }
    }

    public int[] GetUpgrade1()
    {
        return upgrade1Levels;
    }

    public void BuyUpgrade2(int weapon)
    {
        upgrade2Levels[weapon]++;
        moneySystem.GetComponent<MoneySystem>().SpendMoney(upgrade2Prices[weapon]);
        upgrade2Prices[weapon] *= upgrade2PriceIncrease[weapon];
        for (int i = 0; i < upgrade2Levels.Length; i++)
        {
            Upgrade2Refresh(i);
        }
    }

    public void SetUpgrade2(int[] weapons)
    {
        upgrade2Levels = weapons;
        for (int i = 0; i < weapons.Length; i++)
        {
            upgrade2Prices[i] = upgrade2Prices[i] * Mathf.Pow(upgrade2PriceIncrease[i], upgrade2Levels[i]);
            Upgrade2Refresh(i);
        }
    }

    public int[] GetUpgrade2()
    {
        return upgrade2Levels;
    }

    public void BuyUpgrade3(int weapon)
    {
        upgrade3Levels[weapon]++;
        moneySystem.GetComponent<MoneySystem>().SpendMoney(upgrade3Prices[weapon]);
        upgrade3Prices[weapon] *= upgrade3PriceIncrease[weapon];
        for (int i = 0; i < upgrade3Levels.Length; i++)
        {
            Upgrade3Refresh(i);
        }
    }

    public void SetUpgrade3(int[] weapons)
    {
        upgrade3Levels = weapons;
        for (int i = 0; i < weapons.Length; i++)
        {
            upgrade3Prices[i] = upgrade3Prices[i] * Mathf.Pow(upgrade3PriceIncrease[i], upgrade3Levels[i]);
            Upgrade3Refresh(i);
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
            upgrade1Prices[i] = 10;
            upgrade2Prices[i] = 8;
            upgrade3Prices[i] = 15;
            upgrade1Costs[i].text = upgrade1Prices[i].ToString();
            upgrade2Costs[i].text = upgrade2Prices[i].ToString();
            upgrade3Costs[i].text = upgrade3Prices[i].ToString();
            upgrade1Buttons[i].interactable = true;
            upgrade2Buttons[i].interactable = true;
            upgrade3Buttons[i].interactable = false;
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

    private void Upgrade1Refresh(int weapon)
    {
        upgrade1Sliders[weapon].value = (float)upgrade1Levels[weapon] / (float)upgrade1MaxLevels[weapon];
        if (upgrade1Levels[weapon] >= upgrade1MaxLevels[weapon])
        {
            upgrade1Buttons[weapon].interactable = false;
            upgrade1Costs[weapon].text = "Maxed";
        }
        else
        {
            if (upgrade1Prices[weapon] < 1000d)
            {
                upgrade1Costs[weapon].text = upgrade1Prices[weapon].ToString("F0");
            }
            else if (upgrade1Prices[weapon] < 1000000d)
            {
                upgrade1Costs[weapon].text = (upgrade1Prices[weapon] / 1000d).ToString("F2") + "K";
            }
            else if (upgrade1Prices[weapon] < 1000000000d)
            {
                upgrade1Costs[weapon].text = (upgrade1Prices[weapon] / 1000000d).ToString("F2") + "M";
            }
            else if (upgrade1Prices[weapon] < 1000000000000d)
            {
                upgrade1Costs[weapon].text = (upgrade1Prices[weapon] / 1000000000d).ToString("F2") + "B";
            }
            else
            {
                upgrade1Costs[weapon].text = (upgrade1Prices[weapon] / 1000000000000d).ToString("F2") + "T";
            }
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= upgrade1Prices[weapon])
            {
                upgrade1Buttons[weapon].interactable = true;
            }
            else
            {
                upgrade1Buttons[weapon].interactable = false;
            }
        }
    }

    private void Upgrade2Refresh(int weapon)
    {
        upgrade2Sliders[weapon].value = (float)upgrade2Levels[weapon] / (float)upgrade2MaxLevels[weapon];
        if (upgrade2Levels[weapon] >= upgrade2MaxLevels[weapon])
        {
            upgrade2Buttons[weapon].interactable = false;
            upgrade2Costs[weapon].text = "Maxed";
        }
        else
        {
            if (upgrade2Prices[weapon] < 1000d)
            {
                upgrade2Costs[weapon].text = upgrade2Prices[weapon].ToString("F0");
            }
            else if (upgrade2Prices[weapon] < 1000000d)
            {
                upgrade2Costs[weapon].text = (upgrade2Prices[weapon] / 1000d).ToString("F2") + "K";
            }
            else if (upgrade2Prices[weapon] < 1000000000d)
            {
                upgrade2Costs[weapon].text = (upgrade2Prices[weapon] / 1000000d).ToString("F2") + "M";
            }
            else if (upgrade2Prices[weapon] < 1000000000000d)
            {
                upgrade2Costs[weapon].text = (upgrade2Prices[weapon] / 1000000000d).ToString("F2") + "B";
            }
            else
            {
                upgrade2Costs[weapon].text = (upgrade2Prices[weapon] / 1000000000000d).ToString("F2") + "T";
            }
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= upgrade2Prices[weapon])
            {
                upgrade2Buttons[weapon].interactable = true;
            }
            else
            {
                upgrade2Buttons[weapon].interactable = false;
            }
        }
    }

    private void Upgrade3Refresh(int weapon)
    {
        upgrade3Sliders[weapon].value = (float)upgrade3Levels[weapon] / (float)upgrade3MaxLevels[weapon];
        if (upgrade3Levels[weapon] >= upgrade3MaxLevels[weapon])
        {
            upgrade3Buttons[weapon].interactable = false;
            upgrade3Costs[weapon].text = "Maxed";
        }
        else
        {
            if (upgrade3Prices[weapon] < 1000d)
            {
                upgrade3Costs[weapon].text = upgrade3Prices[weapon].ToString("F0");
            }
            else if (upgrade3Prices[weapon] < 1000000d)
            {
                upgrade3Costs[weapon].text = (upgrade3Prices[weapon] / 1000d).ToString("F2") + "K";
            }
            else if (upgrade3Prices[weapon] < 1000000000d)
            {
                upgrade3Costs[weapon].text = (upgrade3Prices[weapon] / 1000000d).ToString("F2") + "M";
            }
            else if (upgrade3Prices[weapon] < 1000000000000d)
            {
                upgrade3Costs[weapon].text = (upgrade3Prices[weapon] / 1000000000d).ToString("F2") + "B";
            }
            else
            {
                upgrade3Costs[weapon].text = (upgrade3Prices[weapon] / 1000000000000d).ToString("F2") + "T";
            }
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= upgrade3Prices[weapon])
            {
                upgrade3Buttons[weapon].interactable = true;
            }
            else
            {
                upgrade3Buttons[weapon].interactable = false;
            }
        }
    }
}