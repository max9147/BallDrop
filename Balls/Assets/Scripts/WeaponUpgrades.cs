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
    private int[] upgrade1MaxLevels = new int[] { 10, 16, 8, 10, 8, 6, 10, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade2MaxLevels = new int[] { 10, 5, 10, 10, 7, 10, 10, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private int[] upgrade3MaxLevels = new int[] { 3, 5, 12, 5, 6, 17, 10, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private double[] upgrade1Prices = new double[] { 5, 10, 8, 8, 10, 30, 8, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private double[] upgrade2Prices = new double[] { 10, 20, 10, 10, 20, 10, 8, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private double[] upgrade3Prices = new double[] { 1000, 100, 4, 15, 30, 2, 8, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private double[] weaponDamages = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private float[] upgrade1PriceIncrease = new float[] { 14, 5, 25, 13, 24, 58, 13, 185, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private float[] upgrade2PriceIncrease = new float[] { 13, 140, 13, 13, 35, 13, 13, 380, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private float[] upgrade3PriceIncrease = new float[] { 1100, 100, 9, 150, 58, 5, 13, 140, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public Button[] upgrade1Buttons;
    public Button[] upgrade2Buttons;
    public Button[] upgrade3Buttons;
    public Slider[] upgrade1Sliders;
    public Slider[] upgrade2Sliders;
    public Slider[] upgrade3Sliders;
    public TextMeshProUGUI[] upgrade1Costs;
    public TextMeshProUGUI[] upgrade2Costs;
    public TextMeshProUGUI[] upgrade3Costs;
    public TextMeshProUGUI[] weaponDamageTexts;
    public TextMeshProUGUI[] weaponDPSTexts;
    public GameObject moneySystem;
    public GameSettings settings;

    public void IncreaseDamage(int id, double amount)
    {
        weaponDamages[id] += amount;
        weaponDamageTexts[id].text = "Total damage dealt: " + weaponDamages[id].ToString("F0");
    }

    public double[] GetDamage()
    {
        return weaponDamages;
    }

    public void SetDamage(double[] savedDamage)
    {
        for (int i = 0; i < weaponDamages.Length; i++)
        {
            weaponDamages[i] = savedDamage[i];
            weaponDamageTexts[i].text = "Total damage dealt: " + weaponDamages[i].ToString("F0");
        }
    }

    public void BuyUpgrade1(int weapon)
    {
        upgrade1Levels[weapon]++;
        moneySystem.GetComponent<MoneySystem>().SpendMoney(upgrade1Prices[weapon]);
        upgrade1Prices[weapon] *= upgrade1PriceIncrease[weapon];
        for (int i = 0; i < upgrade1Levels.Length; i++)
        {
            Upgrade1Refresh(i);
        }
        switch (weapon)
        {
            case 0:
                GameObject[] weaponsLaser = GameObject.FindGameObjectsWithTag("WeaponLaser");
                foreach (var item in weaponsLaser)
                {
                    if (item.GetComponent<Laser>())
                    {
                        item.GetComponent<Laser>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[0].text = "Estimated DPS: " + ((settings.laserDPS + 0.5f * upgrade1Levels[0]) * (1 + upgrade3Levels[0])).ToString("F2");
                break;
            case 1:
                GameObject[] weaponsGas = GameObject.FindGameObjectsWithTag("WeaponGas");
                foreach (var item in weaponsGas)
                {
                    if (item.GetComponent<Gas>())
                    {
                        item.GetComponent<Gas>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
                break;
            case 2:
                GameObject[] weaponsGun = GameObject.FindGameObjectsWithTag("WeaponGun");
                foreach (var item in weaponsGun)
                {
                    if (item.GetComponent<Gun>())
                    {
                        item.GetComponent<Gun>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[2].text = "Estimated DPS: " + ((settings.gunDamage + upgrade1Levels[2]) / (settings.gunReload - (0.05f * upgrade3Levels[2]))).ToString("F2");
                break;
            case 3:
                GameObject[] weaponsFlamethrower = GameObject.FindGameObjectsWithTag("WeaponFlamethrower");
                foreach (var item in weaponsFlamethrower)
                {
                    if (item.GetComponent<Flamethrower>())
                    {
                        item.GetComponentInChildren<Flames>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[3].text = "Estimated DPS: " + (settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]).ToString("F2");
                break;
            case 4:
                GameObject[] weaponsHive = GameObject.FindGameObjectsWithTag("WeaponHive");
                foreach (var item in weaponsHive)
                {
                    if (item.GetComponent<Hive>())
                    {
                        item.GetComponent<Hive>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[4].text = "Estimated DPS: " + ((settings.hiveDPS + 0.5f * upgrade1Levels[4]) / (settings.hiveSpawnTime - 0.5f * upgrade2Levels[4]) * (settings.hiveLifeTime + 0.5f * upgrade3Levels[4])).ToString("F2");
                break;
            case 5:
                GameObject[] weaponsCannon = GameObject.FindGameObjectsWithTag("WeaponCannon");
                foreach (var item in weaponsCannon)
                {
                    if (item.GetComponent<Cannon>())
                    {
                        item.GetComponent<Cannon>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
                break;
            case 6:
                GameObject[] weaponsLightning = GameObject.FindGameObjectsWithTag("WeaponLightning");
                foreach (var item in weaponsLightning)
                {
                    if (item.GetComponent<Lightning>())
                    {
                        item.GetComponent<Lightning>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.5f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
                break;
            case 7:
                GameObject[] weaponsSpikes = GameObject.FindGameObjectsWithTag("WeaponSpikes");
                foreach (var item in weaponsSpikes)
                {
                    if (item.GetComponent<Spikes>())
                    {
                        item.GetComponent<Spikes>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * (settings.spikesCount * upgrade2Levels[7] / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                break;
            default:
                break;
        }
    }

    public void SetUpgrade1(int[] weapons)
    {
        upgrade1Levels = weapons;
        for (int i = 0; i < weapons.Length; i++)
        {
            upgrade1Prices[i] = upgrade1Prices[i] * Mathf.Pow(upgrade1PriceIncrease[i], upgrade1Levels[i]);
            Upgrade1Refresh(i);
            switch (i)
            {
                case 0:
                    GameObject[] weaponsLaser = GameObject.FindGameObjectsWithTag("WeaponLaser");
                    foreach (var item in weaponsLaser)
                    {
                        if (item.GetComponent<Laser>())
                        {
                            item.GetComponent<Laser>().SetDPS(upgrade1Levels[0]);
                        }
                    }
                    weaponDPSTexts[0].text = "Estimated DPS: " + ((settings.laserDPS + 0.5f * upgrade1Levels[0]) * (1 + upgrade3Levels[0])).ToString("F2");
                    break;
                case 1:
                    GameObject[] weaponsGas = GameObject.FindGameObjectsWithTag("WeaponGas");
                    foreach (var item in weaponsGas)
                    {
                        if (item.GetComponent<Gas>())
                        {
                            item.GetComponent<Gas>().SetDPS(upgrade1Levels[1]);
                        }
                    }
                    weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
                    break;
                case 2:
                    GameObject[] weaponsGun = GameObject.FindGameObjectsWithTag("WeaponGun");
                    foreach (var item in weaponsGun)
                    {
                        if (item.GetComponent<Gun>())
                        {
                            item.GetComponent<Gun>().SetDPS(upgrade1Levels[2]);
                        }
                    }
                    weaponDPSTexts[2].text = "Estimated DPS: " + ((settings.gunDamage + upgrade1Levels[2]) / (settings.gunReload - (0.05f * upgrade3Levels[2]))).ToString("F2");
                    break;
                case 3:
                    GameObject[] weaponsFlamethrower = GameObject.FindGameObjectsWithTag("WeaponFlamethrower");
                    foreach (var item in weaponsFlamethrower)
                    {
                        if (item.GetComponent<Flamethrower>())
                        {
                            item.GetComponentInChildren<Flames>().SetDPS(upgrade1Levels[3]);
                        }
                    }
                    weaponDPSTexts[3].text = "Estimated DPS: " + (settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]).ToString("F2");
                    break;
                case 4:
                    GameObject[] weaponsHive = GameObject.FindGameObjectsWithTag("WeaponHive");
                    foreach (var item in weaponsHive)
                    {
                        if (item.GetComponent<Hive>())
                        {
                            item.GetComponent<Hive>().SetDPS(upgrade1Levels[4]);
                        }
                    }
                    weaponDPSTexts[4].text = "Estimated DPS: " + ((settings.hiveDPS + 0.5f * upgrade1Levels[4]) / (settings.hiveSpawnTime - 0.5f * upgrade2Levels[4]) * (settings.hiveLifeTime + 0.5f * upgrade3Levels[4])).ToString("F2");
                    break;
                case 5:
                    GameObject[] weaponsCannon = GameObject.FindGameObjectsWithTag("WeaponCannon");
                    foreach (var item in weaponsCannon)
                    {
                        if (item.GetComponent<Cannon>())
                        {
                            item.GetComponent<Cannon>().SetDPS(upgrade1Levels[5]);
                        }
                    }
                    weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
                    break;
                case 6:
                    GameObject[] weaponsLightning = GameObject.FindGameObjectsWithTag("WeaponLightning");
                    foreach (var item in weaponsLightning)
                    {
                        if (item.GetComponent<Lightning>())
                        {
                            item.GetComponent<Lightning>().SetDPS(upgrade1Levels[6]);
                        }
                    }
                    weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.5f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
                    break;
                case 7:
                    GameObject[] weaponsSpikes = GameObject.FindGameObjectsWithTag("WeaponSpikes");
                    foreach (var item in weaponsSpikes)
                    {
                        if (item.GetComponent<Spikes>())
                        {
                            item.GetComponent<Spikes>().SetDPS(upgrade1Levels[7]);
                        }
                    }
                    weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * (settings.spikesCount * upgrade2Levels[7] / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                    break;
                default:
                    break;
            }
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
        switch (weapon)
        {
            case 0:
                GameObject[] weaponsLaser = GameObject.FindGameObjectsWithTag("WeaponLaser");
                foreach (var item in weaponsLaser)
                {
                    if (item.GetComponent<Laser>())
                    {
                        item.GetComponent<Laser>().UpgradeRange();
                    }
                }
                weaponDPSTexts[0].text = "Estimated DPS: " + ((settings.laserDPS + 0.5f * upgrade1Levels[0]) * (1 + upgrade3Levels[0])).ToString("F2");
                break;
            case 1:
                GameObject[] weaponsGas = GameObject.FindGameObjectsWithTag("WeaponGas");
                foreach (var item in weaponsGas)
                {
                    if (item.GetComponent<Gas>())
                    {
                        item.GetComponent<Gas>().UpgradeRange();
                    }
                }
                weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
                break;
            case 2:
                GameObject[] weaponsGun = GameObject.FindGameObjectsWithTag("WeaponGun");
                foreach (var item in weaponsGun)
                {
                    if (item.GetComponent<Gun>())
                    {
                        item.GetComponent<Gun>().UpgradeRange();
                    }
                }
                weaponDPSTexts[2].text = "Estimated DPS: " + ((settings.gunDamage + upgrade1Levels[2]) / (settings.gunReload - (0.05f * upgrade3Levels[2]))).ToString("F2");
                break;
            case 3:
                GameObject[] weaponsFlamethrower = GameObject.FindGameObjectsWithTag("WeaponFlamethrower");
                foreach (var item in weaponsFlamethrower)
                {
                    if (item.GetComponent<Flamethrower>())
                    {
                        item.GetComponent<Flamethrower>().UpgradeRange();
                        item.GetComponentInChildren<Flames>().UpgradeRange();
                    }
                }
                weaponDPSTexts[3].text = "Estimated DPS: " + (settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]).ToString("F2");
                break;
            case 4:
                GameObject[] weaponsHive = GameObject.FindGameObjectsWithTag("WeaponHive");
                foreach (var item in weaponsHive)
                {
                    if (item.GetComponent<Hive>())
                    {
                        item.GetComponent<Hive>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[4].text = "Estimated DPS: " + ((settings.hiveDPS + 0.5f * upgrade1Levels[4]) / (settings.hiveSpawnTime - 0.5f * upgrade2Levels[4]) * (settings.hiveLifeTime + 0.5f * upgrade3Levels[4])).ToString("F2");
                break;
            case 5:
                GameObject[] weaponsCannon = GameObject.FindGameObjectsWithTag("WeaponCannon");
                foreach (var item in weaponsCannon)
                {
                    if (item.GetComponent<Cannon>())
                    {
                        item.GetComponent<Cannon>().UpgradeRange();
                    }
                }
                weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
                break;
            case 6:
                GameObject[] weaponsLightning = GameObject.FindGameObjectsWithTag("WeaponLightning");
                foreach (var item in weaponsLightning)
                {
                    if (item.GetComponent<Lightning>())
                    {
                        item.GetComponent<Lightning>().UpgradeRange();
                    }
                }
                weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.5f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
                break;
            case 7:
                GameObject[] weaponsSpikes = GameObject.FindGameObjectsWithTag("WeaponSpikes");
                foreach (var item in weaponsSpikes)
                {
                    if (item.GetComponent<Spikes>())
                    {
                        item.GetComponent<Spikes>().UpgradeSpikes();
                    }
                }
                weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * (settings.spikesCount * upgrade2Levels[7] / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                break;
            default:
                break;
        }
    }

    public void SetUpgrade2(int[] weapons)
    {
        upgrade2Levels = weapons;
        for (int i = 0; i < weapons.Length; i++)
        {
            upgrade2Prices[i] = upgrade2Prices[i] * Mathf.Pow(upgrade2PriceIncrease[i], upgrade2Levels[i]);
            Upgrade2Refresh(i);
            switch (i)
            {
                case 0:
                    GameObject[] weaponsLaser = GameObject.FindGameObjectsWithTag("WeaponLaser");
                    foreach (var item in weaponsLaser)
                    {
                        if (item.GetComponent<Laser>())
                        {
                            item.GetComponent<Laser>().SetRange(upgrade2Levels[0]);
                        }
                    }
                    weaponDPSTexts[0].text = "Estimated DPS: " + ((settings.laserDPS + 0.5f * upgrade1Levels[0]) * (1 + upgrade3Levels[0])).ToString("F2");
                    break;
                case 1:
                    GameObject[] weaponsGas = GameObject.FindGameObjectsWithTag("WeaponGas");
                    foreach (var item in weaponsGas)
                    {
                        if (item.GetComponent<Gas>())
                        {
                            item.GetComponent<Gas>().SetRange(upgrade2Levels[1]);
                        }
                    }
                    weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
                    break;
                case 2:
                    GameObject[] weaponsGun = GameObject.FindGameObjectsWithTag("WeaponGun");
                    foreach (var item in weaponsGun)
                    {
                        if (item.GetComponent<Gun>())
                        {
                            item.GetComponent<Gun>().SetRange(upgrade2Levels[2]);
                        }
                    }
                    weaponDPSTexts[2].text = "Estimated DPS: " + ((settings.gunDamage + upgrade1Levels[2]) / (settings.gunReload - (0.05f * upgrade3Levels[2]))).ToString("F2");
                    break;
                case 3:
                    GameObject[] weaponsFlamethrower = GameObject.FindGameObjectsWithTag("WeaponFlamethrower");
                    foreach (var item in weaponsFlamethrower)
                    {
                        if (item.GetComponent<Flamethrower>())
                        {
                            item.GetComponent<Flamethrower>().SetRange(upgrade2Levels[3]);
                            item.GetComponentInChildren<Flames>().SetRange(upgrade2Levels[3]);
                        }
                    }
                    weaponDPSTexts[3].text = "Estimated DPS: " + (settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]).ToString("F2");
                    break;
                case 4:
                    GameObject[] weaponsHive = GameObject.FindGameObjectsWithTag("WeaponHive");
                    foreach (var item in weaponsHive)
                    {
                        if (item.GetComponent<Hive>())
                        {
                            item.GetComponent<Hive>().SetSpeed(upgrade2Levels[4]);
                        }
                    }
                    weaponDPSTexts[4].text = "Estimated DPS: " + ((settings.hiveDPS + 0.5f * upgrade1Levels[4]) / (settings.hiveSpawnTime - 0.5f * upgrade2Levels[4]) * (settings.hiveLifeTime + 0.5f * upgrade3Levels[4])).ToString("F2");
                    break;
                case 5:
                    GameObject[] weaponsCannon = GameObject.FindGameObjectsWithTag("WeaponCannon");
                    foreach (var item in weaponsCannon)
                    {
                        if (item.GetComponent<Cannon>())
                        {
                            item.GetComponent<Cannon>().SetRange(upgrade2Levels[5]);
                        }
                    }
                    weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
                    break;
                case 6:
                    GameObject[] weaponsLightning = GameObject.FindGameObjectsWithTag("WeaponLightning");
                    foreach (var item in weaponsLightning)
                    {
                        if (item.GetComponent<Lightning>())
                        {
                            item.GetComponent<Lightning>().SetRange(upgrade2Levels[6]);
                        }
                    }
                    weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.5f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
                    break;
                case 7:
                    GameObject[] weaponsSpikes = GameObject.FindGameObjectsWithTag("WeaponSpikes");
                    foreach (var item in weaponsSpikes)
                    {
                        if (item.GetComponent<Spikes>())
                        {
                            item.GetComponent<Spikes>().SetSpikes(upgrade2Levels[7]);
                        }
                    }
                    weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * (settings.spikesCount * upgrade2Levels[7] / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                    break;
                default:
                    break;
            }
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
        switch (weapon)
        {
            case 0:
                GameObject[] weaponsLaser = GameObject.FindGameObjectsWithTag("WeaponLaser");
                foreach (var item in weaponsLaser)
                {
                    if (item.GetComponent<Laser>())
                    {
                        item.GetComponent<Laser>().UpgradeTargets();
                    }
                }
                weaponDPSTexts[0].text = "Estimated DPS: " + ((settings.laserDPS + 0.5f * upgrade1Levels[0]) * (1 + upgrade3Levels[0])).ToString("F2");
                break;
            case 1:
                GameObject[] weaponsGas = GameObject.FindGameObjectsWithTag("WeaponGas");
                foreach (var item in weaponsGas)
                {
                    if (item.GetComponent<Gas>())
                    {
                        item.GetComponent<Gas>().UpgradeDamageBoost();
                    }
                }
                weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
                break;
            case 2:
                GameObject[] weaponsGun = GameObject.FindGameObjectsWithTag("WeaponGun");
                foreach (var item in weaponsGun)
                {
                    if (item.GetComponent<Gun>())
                    {
                        item.GetComponent<Gun>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[2].text = "Estimated DPS: " + ((settings.gunDamage + upgrade1Levels[2]) / (settings.gunReload - (0.05f * upgrade3Levels[2]))).ToString("F2");
                break;
            case 3:
                GameObject[] weaponsFlamethrower = GameObject.FindGameObjectsWithTag("WeaponFlamethrower");
                foreach (var item in weaponsFlamethrower)
                {
                    if (item.GetComponent<Flamethrower>())
                    {                        
                        item.GetComponentInChildren<Flames>().UpgradeArea();
                    }
                }
                weaponDPSTexts[3].text = "Estimated DPS: " + (settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]).ToString("F2");
                break;
            case 4:
                weaponDPSTexts[4].text = "Estimated DPS: " + ((settings.hiveDPS + 0.5f * upgrade1Levels[4]) / (settings.hiveSpawnTime - 0.5f * upgrade2Levels[4]) * (settings.hiveLifeTime + 0.5f * upgrade3Levels[4])).ToString("F2");
                break;
            case 5:
                GameObject[] weaponsCannon = GameObject.FindGameObjectsWithTag("WeaponCannon");
                foreach (var item in weaponsCannon)
                {
                    if (item.GetComponent<Cannon>())
                    {
                        item.GetComponent<Cannon>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
                break;
            case 6:
                GameObject[] weaponsLightning = GameObject.FindGameObjectsWithTag("WeaponLightning");
                foreach (var item in weaponsLightning)
                {
                    if (item.GetComponent<Lightning>())
                    {
                        item.GetComponent<Lightning>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.5f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
                break;
            case 7:
                GameObject[] weaponsSpikes = GameObject.FindGameObjectsWithTag("WeaponSpikes");
                foreach (var item in weaponsSpikes)
                {
                    if (item.GetComponent<Spikes>())
                    {
                        item.GetComponent<Spikes>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * (settings.spikesCount * upgrade2Levels[7] / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                break;
            default:
                break;
        }
    }

    public void SetUpgrade3(int[] weapons)
    {
        upgrade3Levels = weapons;
        for (int i = 0; i < weapons.Length; i++)
        {
            upgrade3Prices[i] = upgrade3Prices[i] * Mathf.Pow(upgrade3PriceIncrease[i], upgrade3Levels[i]);
            Upgrade3Refresh(i);
            switch (i)
            {
                case 0:
                    GameObject[] weaponsLaser = GameObject.FindGameObjectsWithTag("WeaponLaser");
                    foreach (var item in weaponsLaser)
                    {
                        if (item.GetComponent<Laser>())
                        {
                            item.GetComponent<Laser>().SetTargets(upgrade3Levels[0]);
                        }
                    }
                    weaponDPSTexts[0].text = "Estimated DPS: " + ((settings.laserDPS + 0.5f * upgrade1Levels[0]) * (1 + upgrade3Levels[0])).ToString("F2");
                    break;
                case 1:
                    GameObject[] weaponsGas = GameObject.FindGameObjectsWithTag("WeaponGas");
                    foreach (var item in weaponsGas)
                    {
                        if (item.GetComponent<Gas>())
                        {
                            item.GetComponent<Gas>().SetDamageBoost(upgrade3Levels[1]);
                        }
                    }
                    weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * (upgrade1Levels[1] + upgrade3Levels[1]));
                    break;
                case 2:
                    GameObject[] weaponsGun = GameObject.FindGameObjectsWithTag("WeaponGun");
                    foreach (var item in weaponsGun)
                    {
                        if (item.GetComponent<Gun>())
                        {
                            item.GetComponent<Gun>().SetSpeed(upgrade3Levels[2]);
                        }
                    }
                    weaponDPSTexts[2].text = "Estimated DPS: " + ((settings.gunDamage + upgrade1Levels[2]) / (settings.gunReload - (0.05f * upgrade3Levels[2]))).ToString("F2");
                    break;
                case 3:
                    GameObject[] weaponsFlamethrower = GameObject.FindGameObjectsWithTag("WeaponFlamethrower");
                    foreach (var item in weaponsFlamethrower)
                    {
                        if (item.GetComponent<Flamethrower>())
                        {
                            item.GetComponentInChildren<Flames>().SetArea(upgrade3Levels[3]);
                        }
                    }
                    weaponDPSTexts[3].text = "Estimated DPS: " + (settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]).ToString("F2");
                    break;
                case 4:
                    weaponDPSTexts[4].text = "Estimated DPS: " + ((settings.hiveDPS + 0.5f * upgrade1Levels[4]) / (settings.hiveSpawnTime - 0.5f * upgrade2Levels[4]) * (settings.hiveLifeTime + 0.5f * upgrade3Levels[4])).ToString("F2");
                    break;
                case 5:
                    GameObject[] weaponsCannon = GameObject.FindGameObjectsWithTag("WeaponCannon");
                    foreach (var item in weaponsCannon)
                    {
                        if (item.GetComponent<Cannon>())
                        {
                            item.GetComponent<Cannon>().SetSpeed(upgrade3Levels[5]);
                        }
                    }
                    weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
                    break;
                case 6:
                    GameObject[] weaponsLightning = GameObject.FindGameObjectsWithTag("WeaponLightning");
                    foreach (var item in weaponsLightning)
                    {
                        if (item.GetComponent<Lightning>())
                        {
                            item.GetComponent<Lightning>().SetSpeed(upgrade3Levels[6]);
                        }
                    }
                    weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.5f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
                    break;
                case 7:
                    GameObject[] weaponsSpikes = GameObject.FindGameObjectsWithTag("WeaponSpikes");
                    foreach (var item in weaponsSpikes)
                    {
                        if (item.GetComponent<Spikes>())
                        {
                            item.GetComponent<Spikes>().SetSpeed(upgrade3Levels[7]);
                        }
                    }
                    weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * (settings.spikesCount * upgrade2Levels[7] / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                    break;
                default:
                    break;
            }
        }        
    }

    public int[] GetUpgrade3()
    {
        return upgrade3Levels;
    }

    public void ResetUpgrades()
    {
        upgrade1Prices = new double[] { 5, 10, 8, 8, 10, 30, 8, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        upgrade2Prices = new double[] { 10, 20, 10, 10, 20, 10, 8, 50, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        upgrade3Prices = new double[] { 1000, 100, 4, 15, 30, 2, 8, 20, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < upgrade1Levels.Length; i++)
        {
            upgrade1Levels[i] = 0;
            upgrade2Levels[i] = 0;
            upgrade3Levels[i] = 0;
            upgrade1Sliders[i].value = 0;
            upgrade2Sliders[i].value = 0;
            upgrade3Sliders[i].value = 0;            
            upgrade1Costs[i].text = upgrade1Prices[i].ToString();
            upgrade2Costs[i].text = upgrade2Prices[i].ToString();
            upgrade3Costs[i].text = upgrade3Prices[i].ToString();
            upgrade1Buttons[i].interactable = true;
            upgrade2Buttons[i].interactable = true;
            upgrade3Buttons[i].interactable = false;
            weaponDamages[i] = 0;
            weaponDamageTexts[i].text = "Total damage dealt: " + weaponDamages[i].ToString("F0");
        }
        weaponDPSTexts[0].text = "Estimated DPS: " + ((settings.laserDPS + 0.5f * upgrade1Levels[0]) * (1 + upgrade3Levels[0])).ToString("F2");
        weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
        weaponDPSTexts[2].text = "Estimated DPS: " + ((settings.gunDamage + upgrade1Levels[2]) / (settings.gunReload - (0.05f * upgrade3Levels[2]))).ToString("F2");
        weaponDPSTexts[3].text = "Estimated DPS: " + (settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]).ToString("F2");
        weaponDPSTexts[4].text = "Estimated DPS: " + ((settings.hiveDPS + 0.5f * upgrade1Levels[4]) / (settings.hiveSpawnTime - 0.5f * upgrade2Levels[4]) * (settings.hiveLifeTime + 0.5f * upgrade3Levels[4])).ToString("F2");
        weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
        weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.5f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
        weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * (settings.spikesCount * upgrade2Levels[7] / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
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