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
    private int[] upgrade1MaxLevels = new int[] { 10, 16, 8, 10, 8, 6, 10, 5, 8, 8, 16, 11, 8, 16, 14, 10, 6, 5 };
    private int[] upgrade2MaxLevels = new int[] { 10, 5, 10, 10, 7, 10, 10, 4, 12, 6, 5, 11, 4, 7, 5, 10, 5, 4 };
    private int[] upgrade3MaxLevels = new int[] { 3, 5, 12, 5, 6, 17, 10, 5, 5, 5, 5, 6, 6, 4, 5, 10, 5, 5 };
    private double[] upgrade1Prices = new double[] { 5, 10, 8, 8, 10, 30, 8, 5, 10, 5, 10, 10, 8, 7, 15, 10, 10, 5 };
    private double[] upgrade2Prices = new double[] { 10, 20, 10, 10, 20, 10, 8, 50, 4, 20, 20, 10, 100, 15, 100, 10, 40, 50 };
    private double[] upgrade3Prices = new double[] { 1000, 100, 4, 15, 30, 2, 8, 20, 10, 30, 100, 30, 40, 70, 100, 10, 40, 20 };
    private double[] weaponDamages = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    private float[] upgrade1PriceIncrease = new float[] { 14, 5, 25, 13, 24, 58, 13, 185, 24, 26, 5, 10, 25, 5, 6, 13, 70, 185 };
    private float[] upgrade2PriceIncrease = new float[] { 13, 140, 13, 13, 35, 13, 13, 380, 9, 62, 140, 10, 320, 36, 100, 13, 125, 380 };
    private float[] upgrade3PriceIncrease = new float[] { 1100, 100, 9, 150, 58, 5, 13, 140, 160, 130, 100, 58, 55, 350, 100, 13, 125, 140 };

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
    public TextMeshProUGUI[] weaponDamageTexts;
    public TextMeshProUGUI[] weaponDPSTexts;
    public GameObject moneySystem;
    public GameObject soundSystem;
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

    public void BuyMaxUpgrade1(int weapon)
    {
        double totalPrice = 0;
        int avaliableLevels = 0;
        for (int i = 0; i < upgrade1MaxLevels[weapon] - upgrade1Levels[weapon]; i++)
        {
            totalPrice += upgrade1Prices[weapon] * Mathf.Pow(upgrade1PriceIncrease[weapon], i);
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= totalPrice)
            {
                avaliableLevels++;
            }
        }
        for (int j = 0; j < avaliableLevels; j++)
        {
            BuyUpgrade1(weapon);
        }
    }

    public void BuyUpgrade1(int weapon)
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
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
                weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * 3 * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
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
                weaponDPSTexts[3].text = "Estimated DPS: " + ((settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]) * (1 + 0.2f * (upgrade2Levels[3] + upgrade3Levels[3]))).ToString("F2");
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
                weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) * 4 / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
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
                weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.7f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
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
                weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * ((settings.spikesCount + upgrade2Levels[7]) / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                break;
            case 8:
                weaponDPSTexts[8].text = "Estimated DPS: " + ((settings.poisonDPS + upgrade1Levels[8] + 0.5f * upgrade3Levels[8]) * 4).ToString("F2");
                break;
            case 9:
                GameObject[] weaponsDarkMagic = GameObject.FindGameObjectsWithTag("WeaponDarkMagic");
                foreach (var item in weaponsDarkMagic)
                {
                    if (item.GetComponent<DarkMagic>())
                    {
                        item.GetComponent<DarkMagic>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[9].text = "Estimated DPS: " + ((settings.darkMagicDPS + 0.5f * upgrade1Levels[9]) * (4 + 0.1f * upgrade3Levels[9])).ToString("F2");
                break;
            case 10:
                GameObject[] weaponsSaw = GameObject.FindGameObjectsWithTag("WeaponSaw");
                foreach (var item in weaponsSaw)
                {
                    if (item.GetComponent<Saw>())
                    {
                        item.GetComponent<Saw>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[10].text = "Estimated DPS: " + ((settings.sawDPS + 0.5f * (upgrade1Levels[10] + upgrade3Levels[10])) * 3).ToString("F2");
                break;
            case 11:
                GameObject[] weaponsSniper = GameObject.FindGameObjectsWithTag("WeaponSniper");
                foreach (var item in weaponsSniper)
                {
                    if (item.GetComponent<Sniper>())
                    {
                        item.GetComponent<Sniper>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[11].text = "Estimated DPS: " + ((settings.sniperDamage + upgrade1Levels[11]) * (1 + 0.05f * upgrade3Levels[11]) * 3 / (settings.sniperReload - 0.2f * upgrade2Levels[11])).ToString("F2");
                break;
            case 12:
                GameObject[] weaponsShocker = GameObject.FindGameObjectsWithTag("WeaponShocker");
                foreach (var item in weaponsShocker)
                {
                    if (item.GetComponent<Shocker>())
                    {
                        item.GetComponent<Shocker>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[12].text = "Estimated DPS: " + ((settings.shockerDamage + 0.5f * upgrade1Levels[12]) * 3 / (settings.shockerReload - 0.1f * upgrade2Levels[12])).ToString("F2");
                break;
            case 13:
                GameObject[] weaponsShotgun = GameObject.FindGameObjectsWithTag("WeaponShotgun");
                foreach (var item in weaponsShotgun)
                {
                    if (item.GetComponent<Shotgun>())
                    {
                        item.GetComponent<Shotgun>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[13].text = "Estimated DPS: " + ((settings.shotgunDamage + 0.2f * upgrade1Levels[13]) * (settings.shotgunBulletCount + upgrade3Levels[13]) / (settings.shotgunBulletCount - 0.2f * upgrade2Levels[13])).ToString("F2");
                break;
            case 14:
                GameObject[] weaponsGrenades = GameObject.FindGameObjectsWithTag("WeaponGrenades");
                foreach (var item in weaponsGrenades)
                {
                    if (item.GetComponent<Grenades>())
                    {
                        item.GetComponent<Grenades>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[14].text = "Estimated DPS: " + ((settings.grenadesDamage + upgrade1Levels[14]) * 3f / (settings.grenadesReload - 0.5f * upgrade2Levels[14])).ToString("F2");
                break;
            case 15:
                GameObject[] weaponsPump = GameObject.FindGameObjectsWithTag("WeaponPump");
                foreach (var item in weaponsPump)
                {
                    if (item.GetComponent<Pump>())
                    {
                        item.GetComponent<Pump>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[15].text = "Estimated DPS: " + (settings.pumpDamage + 0.7f * (upgrade1Levels[15] + upgrade2Levels[15])).ToString("F2");
                break;
            case 16:
                GameObject[] weaponsMinigun = GameObject.FindGameObjectsWithTag("WeaponMinigun");
                foreach (var item in weaponsMinigun)
                {
                    if (item.GetComponent<Minigun>())
                    {
                        item.GetComponent<Minigun>().UpgradeDPS();
                    }
                }
                weaponDPSTexts[16].text = "Estimated DPS: " + ((settings.minigunDamage + 0.1f * upgrade1Levels[16]) / (settings.minigunReload-0.02f * upgrade3Levels[16])).ToString("F2");
                break;
            case 17:
                weaponDPSTexts[17].text = "Estimated DPS: " + (settings.virusDamage + 0.5f * 5 * (upgrade1Levels[17] + upgrade2Levels[17])).ToString("F2");
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
                    weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * 3 * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
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
                    weaponDPSTexts[3].text = "Estimated DPS: " + ((settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]) * (1 + 0.2f * (upgrade2Levels[3] + upgrade3Levels[3]))).ToString("F2");
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
                    weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) * 4 / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
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
                    weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.7f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
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
                    weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * ((settings.spikesCount + upgrade2Levels[7]) / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                    break;
                case 8:
                    weaponDPSTexts[8].text = "Estimated DPS: " + ((settings.poisonDPS + upgrade1Levels[8] + 0.5f * upgrade3Levels[8]) * 4).ToString("F2");
                    break;
                case 9:
                    GameObject[] weaponsDarkMagic = GameObject.FindGameObjectsWithTag("WeaponDarkMagic");
                    foreach (var item in weaponsDarkMagic)
                    {
                        if (item.GetComponent<DarkMagic>())
                        {
                            item.GetComponent<DarkMagic>().SetDPS(upgrade1Levels[9]);
                        }
                    }
                    weaponDPSTexts[9].text = "Estimated DPS: " + ((settings.darkMagicDPS + 0.5f * upgrade1Levels[9]) * (4 + 0.1f * upgrade3Levels[9])).ToString("F2");
                    break;
                case 10:
                    GameObject[] weaponsSaw = GameObject.FindGameObjectsWithTag("WeaponSaw");
                    foreach (var item in weaponsSaw)
                    {
                        if (item.GetComponent<Saw>())
                        {
                            item.GetComponent<Saw>().SetDPS(upgrade1Levels[10]);
                        }
                    }
                    weaponDPSTexts[10].text = "Estimated DPS: " + ((settings.sawDPS + 0.5f * (upgrade1Levels[10] + upgrade3Levels[10])) * 3).ToString("F2");
                    break;
                case 11:
                    GameObject[] weaponsSniper = GameObject.FindGameObjectsWithTag("WeaponSniper");
                    foreach (var item in weaponsSniper)
                    {
                        if (item.GetComponent<Sniper>())
                        {
                            item.GetComponent<Sniper>().SetDPS(upgrade1Levels[11]);
                        }
                    }
                    weaponDPSTexts[11].text = "Estimated DPS: " + ((settings.sniperDamage + upgrade1Levels[11]) * (1 + 0.05f * upgrade3Levels[11]) * 3 / (settings.sniperReload - 0.2f * upgrade2Levels[11])).ToString("F2");
                    break;
                case 12:
                    GameObject[] weaponsShocker = GameObject.FindGameObjectsWithTag("WeaponShocker");
                    foreach (var item in weaponsShocker)
                    {
                        if (item.GetComponent<Shocker>())
                        {
                            item.GetComponent<Shocker>().SetDPS(upgrade1Levels[12]);
                        }
                    }
                    weaponDPSTexts[12].text = "Estimated DPS: " + ((settings.shockerDamage + 0.5f * upgrade1Levels[12]) * 3 / (settings.shockerReload - 0.1f * upgrade2Levels[12])).ToString("F2");
                    break;
                case 13:
                    GameObject[] weaponsShotgun = GameObject.FindGameObjectsWithTag("WeaponShotgun");
                    foreach (var item in weaponsShotgun)
                    {
                        if (item.GetComponent<Shotgun>())
                        {
                            item.GetComponent<Shotgun>().SetDPS(upgrade1Levels[13]);
                        }
                    }
                    weaponDPSTexts[13].text = "Estimated DPS: " + ((settings.shotgunDamage + 0.2f * upgrade1Levels[13]) * (settings.shotgunBulletCount + upgrade3Levels[13]) / (settings.shotgunBulletCount - 0.2f * upgrade2Levels[13])).ToString("F2");
                    break;
                case 14:
                    GameObject[] weaponsGrenades = GameObject.FindGameObjectsWithTag("WeaponGrenades");
                    foreach (var item in weaponsGrenades)
                    {
                        if (item.GetComponent<Grenades>())
                        {
                            item.GetComponent<Grenades>().SetDPS(upgrade1Levels[14]);
                        }
                    }
                    weaponDPSTexts[14].text = "Estimated DPS: " + ((settings.grenadesDamage + upgrade1Levels[14]) * 3f / (settings.grenadesReload - 0.5f * upgrade2Levels[14])).ToString("F2");
                    break;
                case 15:
                    GameObject[] weaponsPump = GameObject.FindGameObjectsWithTag("WeaponPump");
                    foreach (var item in weaponsPump)
                    {
                        if (item.GetComponent<Pump>())
                        {
                            item.GetComponent<Pump>().SetDPS(upgrade1Levels[15]);
                        }
                    }
                    weaponDPSTexts[15].text = "Estimated DPS: " + (settings.pumpDamage + 0.7f * (upgrade1Levels[15] + upgrade2Levels[15])).ToString("F2");
                    break;
                case 16:
                    GameObject[] weaponsMinigun = GameObject.FindGameObjectsWithTag("WeaponMinigun");
                    foreach (var item in weaponsMinigun)
                    {
                        if (item.GetComponent<Minigun>())
                        {
                            item.GetComponent<Minigun>().SetDPS(upgrade1Levels[16]);
                        }
                    }
                    weaponDPSTexts[16].text = "Estimated DPS: " + ((settings.minigunDamage + 0.1f * upgrade1Levels[16]) / (settings.minigunReload - 0.02f * upgrade3Levels[16])).ToString("F2");
                    break;
                case 17:
                    weaponDPSTexts[17].text = "Estimated DPS: " + (settings.virusDamage + 0.5f * 5 * (upgrade1Levels[17] + upgrade2Levels[17])).ToString("F2");
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

    public void BuyMaxUpgrade2(int weapon)
    {
        double totalPrice = 0;
        int avaliableLevels = 0;
        for (int i = 0; i < upgrade2MaxLevels[weapon] - upgrade2Levels[weapon]; i++)
        {
            totalPrice += upgrade2Prices[weapon] * Mathf.Pow(upgrade2PriceIncrease[weapon], i);
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= totalPrice)
            {
                avaliableLevels++;
            }
        }
        for (int j = 0; j < avaliableLevels; j++)
        {
            BuyUpgrade2(weapon);
        }
    }

    public void BuyUpgrade2(int weapon)
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
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
                weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * 3 * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
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
                weaponDPSTexts[3].text = "Estimated DPS: " + ((settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]) * (1 + 0.2f * (upgrade2Levels[3] + upgrade3Levels[3]))).ToString("F2");
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
                weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) * 4 / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
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
                weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.7f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
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
                weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * ((settings.spikesCount + upgrade2Levels[7]) / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                break;
            case 8:
                weaponDPSTexts[8].text = "Estimated DPS: " + ((settings.poisonDPS + upgrade1Levels[8] + 0.5f * upgrade3Levels[8]) * 4).ToString("F2");
                break;
            case 9:                
                weaponDPSTexts[9].text = "Estimated DPS: " + ((settings.darkMagicDPS + 0.5f * upgrade1Levels[9]) * (4 + 0.1f * upgrade3Levels[9])).ToString("F2");
                break;
            case 10:
                GameObject[] weaponsSaw = GameObject.FindGameObjectsWithTag("WeaponSaw");
                foreach (var item in weaponsSaw)
                {
                    if (item.GetComponent<Saw>())
                    {
                        item.GetComponent<Saw>().UpgradeRange();
                    }
                }
                weaponDPSTexts[10].text = "Estimated DPS: " + ((settings.sawDPS + 0.5f * (upgrade1Levels[10] + upgrade3Levels[10])) * 3).ToString("F2");
                break;
            case 11:
                GameObject[] weaponsSniper = GameObject.FindGameObjectsWithTag("WeaponSniper");
                foreach (var item in weaponsSniper)
                {
                    if (item.GetComponent<Sniper>())
                    {
                        item.GetComponent<Sniper>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[11].text = "Estimated DPS: " + ((settings.sniperDamage + upgrade1Levels[11]) * (1 + 0.05f * upgrade3Levels[11]) * 3 / (settings.sniperReload - 0.2f * upgrade2Levels[11])).ToString("F2");
                break;
            case 12:
                GameObject[] weaponsShocker = GameObject.FindGameObjectsWithTag("WeaponShocker");
                foreach (var item in weaponsShocker)
                {
                    if (item.GetComponent<Shocker>())
                    {
                        item.GetComponent<Shocker>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[12].text = "Estimated DPS: " + ((settings.shockerDamage + 0.5f * upgrade1Levels[12]) * 3 / (settings.shockerReload - 0.1f * upgrade2Levels[12])).ToString("F2");
                break;
            case 13:
                GameObject[] weaponsShotgun = GameObject.FindGameObjectsWithTag("WeaponShotgun");
                foreach (var item in weaponsShotgun)
                {
                    if (item.GetComponent<Shotgun>())
                    {
                        item.GetComponent<Shotgun>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[13].text = "Estimated DPS: " + ((settings.shotgunDamage + 0.2f * upgrade1Levels[13]) * (settings.shotgunBulletCount + upgrade3Levels[13]) / (settings.shotgunBulletCount - 0.2f * upgrade2Levels[13])).ToString("F2");
                break;
            case 14:
                GameObject[] weaponsGrenades = GameObject.FindGameObjectsWithTag("WeaponGrenades");
                foreach (var item in weaponsGrenades)
                {
                    if (item.GetComponent<Grenades>())
                    {
                        item.GetComponent<Grenades>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[14].text = "Estimated DPS: " + ((settings.grenadesDamage + upgrade1Levels[14]) * 3f / (settings.grenadesReload - 0.5f * upgrade2Levels[14])).ToString("F2");
                break;
            case 15:
                GameObject[] weaponsPump = GameObject.FindGameObjectsWithTag("WeaponPump");
                foreach (var item in weaponsPump)
                {
                    if (item.GetComponent<Pump>())
                    {
                        item.GetComponent<Pump>().UpgradeBoost();
                    }
                }
                weaponDPSTexts[15].text = "Estimated DPS: " + (settings.pumpDamage + 0.7f * (upgrade1Levels[15] + upgrade2Levels[15])).ToString("F2");
                break;
            case 16:
                GameObject[] weaponsMinigun = GameObject.FindGameObjectsWithTag("WeaponMinigun");
                foreach (var item in weaponsMinigun)
                {
                    if (item.GetComponent<Minigun>())
                    {
                        item.GetComponent<Minigun>().UpgradeRange();
                    }
                }
                weaponDPSTexts[16].text = "Estimated DPS: " + ((settings.minigunDamage + 0.1f * upgrade1Levels[16]) / (settings.minigunReload - 0.02f * upgrade3Levels[16])).ToString("F2");
                break;
            case 17:
                weaponDPSTexts[17].text = "Estimated DPS: " + (settings.virusDamage + 0.5f * 5 * (upgrade1Levels[17] + upgrade2Levels[17])).ToString("F2");
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
                    weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * 3 * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
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
                    weaponDPSTexts[3].text = "Estimated DPS: " + ((settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]) * (1 + 0.2f * (upgrade2Levels[3] + upgrade3Levels[3]))).ToString("F2");
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
                    weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) * 4 / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
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
                    weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.7f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
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
                    weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * ((settings.spikesCount + upgrade2Levels[7]) / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                    break;
                case 8:
                    weaponDPSTexts[8].text = "Estimated DPS: " + ((settings.poisonDPS + upgrade1Levels[8] + 0.5f * upgrade3Levels[8]) * 4).ToString("F2");
                    break;
                case 9:
                    weaponDPSTexts[9].text = "Estimated DPS: " + ((settings.darkMagicDPS + 0.5f * upgrade1Levels[9]) * (4 + 0.1f * upgrade3Levels[9])).ToString("F2");
                    break;
                case 10:
                    GameObject[] weaponsSaw = GameObject.FindGameObjectsWithTag("WeaponSaw");
                    foreach (var item in weaponsSaw)
                    {
                        if (item.GetComponent<Saw>())
                        {
                            item.GetComponent<Saw>().SetRange(upgrade2Levels[10]);
                        }
                    }
                    weaponDPSTexts[10].text = "Estimated DPS: " + ((settings.sawDPS + 0.5f * (upgrade1Levels[10] + upgrade3Levels[10])) * 3).ToString("F2");
                    break;
                case 11:
                    GameObject[] weaponsSniper = GameObject.FindGameObjectsWithTag("WeaponSniper");
                    foreach (var item in weaponsSniper)
                    {
                        if (item.GetComponent<Sniper>())
                        {
                            item.GetComponent<Sniper>().SetSpeed(upgrade2Levels[11]);
                        }
                    }
                    weaponDPSTexts[11].text = "Estimated DPS: " + ((settings.sniperDamage + upgrade1Levels[11]) * (1 + 0.05f * upgrade3Levels[11]) * 3 / (settings.sniperReload - 0.2f * upgrade2Levels[11])).ToString("F2");
                    break;
                case 12:
                    GameObject[] weaponsShocker = GameObject.FindGameObjectsWithTag("WeaponShocker");
                    foreach (var item in weaponsShocker)
                    {
                        if (item.GetComponent<Shocker>())
                        {
                            item.GetComponent<Shocker>().SetSpeed(upgrade2Levels[12]);
                        }
                    }
                    weaponDPSTexts[12].text = "Estimated DPS: " + ((settings.shockerDamage + 0.5f * upgrade1Levels[12]) * 3 / (settings.shockerReload - 0.1f * upgrade2Levels[12])).ToString("F2");
                    break;
                case 13:
                    GameObject[] weaponsShotgun = GameObject.FindGameObjectsWithTag("WeaponShotgun");
                    foreach (var item in weaponsShotgun)
                    {
                        if (item.GetComponent<Shotgun>())
                        {
                            item.GetComponent<Shotgun>().SetSpeed(upgrade2Levels[13]);
                        }
                    }
                    weaponDPSTexts[13].text = "Estimated DPS: " + ((settings.shotgunDamage + 0.2f * upgrade1Levels[13]) * (settings.shotgunBulletCount + upgrade3Levels[13]) / (settings.shotgunBulletCount - 0.2f * upgrade2Levels[13])).ToString("F2");
                    break;
                case 14:
                    GameObject[] weaponsGrenades = GameObject.FindGameObjectsWithTag("WeaponGrenades");
                    foreach (var item in weaponsGrenades)
                    {
                        if (item.GetComponent<Grenades>())
                        {
                            item.GetComponent<Grenades>().SetSpeed(upgrade2Levels[14]);
                        }
                    }
                    weaponDPSTexts[14].text = "Estimated DPS: " + ((settings.grenadesDamage + upgrade1Levels[14]) * 3f / (settings.grenadesReload - 0.5f * upgrade2Levels[14])).ToString("F2");
                    break;
                case 15:
                    GameObject[] weaponsPump = GameObject.FindGameObjectsWithTag("WeaponPump");
                    foreach (var item in weaponsPump)
                    {
                        if (item.GetComponent<Pump>())
                        {
                            item.GetComponent<Pump>().SetBoost(upgrade2Levels[15]);
                        }
                    }
                    weaponDPSTexts[15].text = "Estimated DPS: " + (settings.pumpDamage + 0.7f * (upgrade1Levels[15] + upgrade2Levels[15])).ToString("F2");
                    break;
                case 16:
                    GameObject[] weaponsMinigun = GameObject.FindGameObjectsWithTag("WeaponMinigun");
                    foreach (var item in weaponsMinigun)
                    {
                        if (item.GetComponent<Minigun>())
                        {
                            item.GetComponent<Minigun>().SetRange(upgrade2Levels[16]);
                        }
                    }
                    weaponDPSTexts[16].text = "Estimated DPS: " + ((settings.minigunDamage + 0.1f * upgrade1Levels[16]) / (settings.minigunReload - 0.02f * upgrade3Levels[16])).ToString("F2");
                    break;
                case 17:
                    weaponDPSTexts[17].text = "Estimated DPS: " + (settings.virusDamage + 0.5f * 5 * (upgrade1Levels[17] + upgrade2Levels[17])).ToString("F2");
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

    public void BuyMaxUpgrade3(int weapon)
    {
        double totalPrice = 0;
        int avaliableLevels = 0;
        for (int i = 0; i < upgrade3MaxLevels[weapon] - upgrade3Levels[weapon]; i++)
        {
            totalPrice += upgrade3Prices[weapon] * Mathf.Pow(upgrade3PriceIncrease[weapon], i);
            if (moneySystem.GetComponent<MoneySystem>().GetMoneyAmount() >= totalPrice)
            {
                avaliableLevels++;
            }
        }
        for (int j = 0; j < avaliableLevels; j++)
        {
            BuyUpgrade3(weapon);
        }
    }

    public void BuyUpgrade3(int weapon)
    {
        soundSystem.GetComponent<SoundSystem>().PlayCoin();
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
                weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * 3 * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
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
                weaponDPSTexts[3].text = "Estimated DPS: " + ((settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]) * (1 + 0.2f * (upgrade2Levels[3] + upgrade3Levels[3]))).ToString("F2");
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
                weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) * 4 / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
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
                weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.7f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
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
                weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * ((settings.spikesCount + upgrade2Levels[7]) / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                break;
            case 8:
                weaponDPSTexts[8].text = "Estimated DPS: " + ((settings.poisonDPS + upgrade1Levels[8] + 0.5f * upgrade3Levels[8]) * 4).ToString("F2");
                break;
            case 9:
                weaponDPSTexts[9].text = "Estimated DPS: " + ((settings.darkMagicDPS + 0.5f * upgrade1Levels[9]) * (4 + 0.1f * upgrade3Levels[9])).ToString("F2");
                break;
            case 10:
                GameObject[] weaponsSaw = GameObject.FindGameObjectsWithTag("WeaponSaw");
                foreach (var item in weaponsSaw)
                {
                    if (item.GetComponent<Saw>())
                    {
                        item.GetComponent<Saw>().UpgradeDamageBoost();
                    }
                }
                weaponDPSTexts[10].text = "Estimated DPS: " + ((settings.sawDPS + 0.5f * (upgrade1Levels[10] + upgrade3Levels[10])) * 3).ToString("F2");
                break;
            case 11:
                GameObject[] weaponsSniper = GameObject.FindGameObjectsWithTag("WeaponSniper");
                foreach (var item in weaponsSniper)
                {
                    if (item.GetComponent<Sniper>())
                    {
                        item.GetComponent<Sniper>().UpgradeCrit();
                    }
                }
                weaponDPSTexts[11].text = "Estimated DPS: " + ((settings.sniperDamage + upgrade1Levels[11]) * (1 + 0.05f * upgrade3Levels[11]) * 3 / (settings.sniperReload - 0.2f * upgrade2Levels[11])).ToString("F2");
                break;
            case 12:
                GameObject[] weaponsShocker = GameObject.FindGameObjectsWithTag("WeaponShocker");
                foreach (var item in weaponsShocker)
                {
                    if (item.GetComponent<Shocker>())
                    {
                        item.GetComponent<Shocker>().UpgradeRange();
                    }
                }
                weaponDPSTexts[12].text = "Estimated DPS: " + ((settings.shockerDamage + 0.5f * upgrade1Levels[12]) * 3 / (settings.shockerReload - 0.1f * upgrade2Levels[12])).ToString("F2");
                break;
            case 13:
                GameObject[] weaponsShotgun = GameObject.FindGameObjectsWithTag("WeaponShotgun");
                foreach (var item in weaponsShotgun)
                {
                    if (item.GetComponent<Shotgun>())
                    {
                        item.GetComponent<Shotgun>().UpgradeBullets();
                    }
                }
                weaponDPSTexts[13].text = "Estimated DPS: " + ((settings.shotgunDamage + 0.2f * upgrade1Levels[13]) * (settings.shotgunBulletCount + upgrade3Levels[13]) / (settings.shotgunBulletCount - 0.2f * upgrade2Levels[13])).ToString("F2");
                break;
            case 14:                
                weaponDPSTexts[14].text = "Estimated DPS: " + ((settings.grenadesDamage + upgrade1Levels[14]) * 3f / (settings.grenadesReload - 0.5f * upgrade2Levels[14])).ToString("F2");
                break;
            case 15:
                GameObject[] weaponsPump = GameObject.FindGameObjectsWithTag("WeaponPump");
                foreach (var item in weaponsPump)
                {
                    if (item.GetComponent<Pump>())
                    {
                        item.GetComponent<Pump>().UpgradeTime();
                    }
                }
                weaponDPSTexts[15].text = "Estimated DPS: " + (settings.pumpDamage + 0.7f * (upgrade1Levels[15] + upgrade2Levels[15])).ToString("F2");
                break;
            case 16:
                GameObject[] weaponsMinigun = GameObject.FindGameObjectsWithTag("WeaponMinigun");
                foreach (var item in weaponsMinigun)
                {
                    if (item.GetComponent<Minigun>())
                    {
                        item.GetComponent<Minigun>().UpgradeSpeed();
                    }
                }
                weaponDPSTexts[16].text = "Estimated DPS: " + ((settings.minigunDamage + 0.1f * upgrade1Levels[16]) / (settings.minigunReload - 0.02f * upgrade3Levels[16])).ToString("F2");
                break;
            case 17:
                weaponDPSTexts[17].text = "Estimated DPS: " + (settings.virusDamage + 0.5f * 5 * (upgrade1Levels[17] + upgrade2Levels[17])).ToString("F2");
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
                    weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * 3 * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
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
                    weaponDPSTexts[3].text = "Estimated DPS: " + ((settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]) * (1 + 0.2f * (upgrade2Levels[3] + upgrade3Levels[3]))).ToString("F2");
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
                    weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) * 4 / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
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
                    weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.7f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
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
                    weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * ((settings.spikesCount + upgrade2Levels[7]) / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
                    break;
                case 8:
                    weaponDPSTexts[8].text = "Estimated DPS: " + ((settings.poisonDPS + upgrade1Levels[8] + 0.5f * upgrade3Levels[8]) * 4).ToString("F2");
                    break;
                case 9:
                    weaponDPSTexts[9].text = "Estimated DPS: " + ((settings.darkMagicDPS + 0.5f * upgrade1Levels[9]) * (4 + 0.1f * upgrade3Levels[9])).ToString("F2");
                    break;
                case 10:
                    GameObject[] weaponsSaw = GameObject.FindGameObjectsWithTag("WeaponSaw");
                    foreach (var item in weaponsSaw)
                    {
                        if (item.GetComponent<Saw>())
                        {
                            item.GetComponent<Saw>().SetDamageBoost(upgrade3Levels[10]);
                        }
                    }
                    weaponDPSTexts[10].text = "Estimated DPS: " + ((settings.sawDPS + 0.5f * (upgrade1Levels[10] + upgrade3Levels[10])) * 3).ToString("F2");
                    break;
                case 11:
                    GameObject[] weaponsSniper = GameObject.FindGameObjectsWithTag("WeaponSniper");
                    foreach (var item in weaponsSniper)
                    {
                        if (item.GetComponent<Sniper>())
                        {
                            item.GetComponent<Sniper>().SetCrit(upgrade3Levels[11]);
                        }
                    }
                    weaponDPSTexts[11].text = "Estimated DPS: " + ((settings.sniperDamage + upgrade1Levels[11]) * (1 + 0.05f * upgrade3Levels[11]) * 3 / (settings.sniperReload - 0.2f * upgrade2Levels[11])).ToString("F2");
                    break;
                case 12:
                    GameObject[] weaponsShocker = GameObject.FindGameObjectsWithTag("WeaponShocker");
                    foreach (var item in weaponsShocker)
                    {
                        if (item.GetComponent<Shocker>())
                        {
                            item.GetComponent<Shocker>().SetRange(upgrade3Levels[12]);
                        }
                    }
                    weaponDPSTexts[12].text = "Estimated DPS: " + ((settings.shockerDamage + 0.5f * upgrade1Levels[12]) * 3 / (settings.shockerReload - 0.1f * upgrade2Levels[12])).ToString("F2");
                    break;
                case 13:
                    GameObject[] weaponsShotgun = GameObject.FindGameObjectsWithTag("WeaponShotgun");
                    foreach (var item in weaponsShotgun)
                    {
                        if (item.GetComponent<Shotgun>())
                        {
                            item.GetComponent<Shotgun>().SetBullets(upgrade3Levels[13]);
                        }
                    }
                    weaponDPSTexts[13].text = "Estimated DPS: " + ((settings.shotgunDamage + 0.2f * upgrade1Levels[13]) * (settings.shotgunBulletCount + upgrade3Levels[13]) / (settings.shotgunBulletCount - 0.2f * upgrade2Levels[13])).ToString("F2");
                    break;
                case 14:
                    weaponDPSTexts[14].text = "Estimated DPS: " + ((settings.grenadesDamage + upgrade1Levels[14]) * 3f / (settings.grenadesReload - 0.5f * upgrade2Levels[14])).ToString("F2");
                    break;
                case 15:
                    GameObject[] weaponsPump = GameObject.FindGameObjectsWithTag("WeaponPump");
                    foreach (var item in weaponsPump)
                    {
                        if (item.GetComponent<Pump>())
                        {
                            item.GetComponent<Pump>().SetTime(upgrade3Levels[15]);
                        }
                    }
                    weaponDPSTexts[15].text = "Estimated DPS: " + (settings.pumpDamage + 0.7f * (upgrade1Levels[15] + upgrade2Levels[15])).ToString("F2");
                    break;
                case 16:
                    GameObject[] weaponsMinigun = GameObject.FindGameObjectsWithTag("WeaponMinigun");
                    foreach (var item in weaponsMinigun)
                    {
                        if (item.GetComponent<Minigun>())
                        {
                            item.GetComponent<Minigun>().SetSpeed(upgrade3Levels[16]);
                        }
                    }
                    weaponDPSTexts[16].text = "Estimated DPS: " + ((settings.minigunDamage + 0.1f * upgrade1Levels[16]) / (settings.minigunReload - 0.02f * upgrade3Levels[16])).ToString("F2");
                    break;
                case 17:
                    weaponDPSTexts[17].text = "Estimated DPS: " + (settings.virusDamage + 0.5f * 5 * (upgrade1Levels[17] + upgrade2Levels[17])).ToString("F2");
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
        upgrade1Prices = new double[] { 5, 10, 8, 8, 10, 30, 8, 5, 10, 5, 10, 10, 8, 7, 15, 10, 10, 5 };
        upgrade2Prices = new double[] { 10, 20, 10, 10, 20, 10, 8, 50, 4, 20, 20, 10, 100, 15, 100, 10, 40, 50 };
        upgrade3Prices = new double[] { 1000, 100, 4, 15, 30, 2, 8, 20, 10, 30, 100, 30, 40, 70, 100, 10, 40, 20 };
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
            RefreshUpgrades();
            weaponDamages[i] = 0;
            weaponDamageTexts[i].text = "Total damage dealt: " + weaponDamages[i].ToString("F0");
        }
        weaponDPSTexts[0].text = "Estimated DPS: " + ((settings.laserDPS + 0.5f * upgrade1Levels[0]) * (1 + upgrade3Levels[0])).ToString("F2");
        weaponDPSTexts[1].text = "Estimated DPS: " + (settings.gasDPS + 0.5f * 3 * (upgrade1Levels[1] + upgrade3Levels[1])).ToString("F2");
        weaponDPSTexts[2].text = "Estimated DPS: " + ((settings.gunDamage + upgrade1Levels[2]) / (settings.gunReload - (0.05f * upgrade3Levels[2]))).ToString("F2");
        weaponDPSTexts[3].text = "Estimated DPS: " + ((settings.flamethrowerDPS + 0.8f * upgrade1Levels[3]) * (1 + 0.2f * (upgrade2Levels[3] + upgrade3Levels[3]))).ToString("F2");
        weaponDPSTexts[4].text = "Estimated DPS: " + ((settings.hiveDPS + 0.5f * upgrade1Levels[4]) / (settings.hiveSpawnTime - 0.5f * upgrade2Levels[4]) * (settings.hiveLifeTime + 0.5f * upgrade3Levels[4])).ToString("F2");
        weaponDPSTexts[5].text = "Estimated DPS: " + ((settings.cannonDamage + upgrade1Levels[5]) * 4 / (settings.cannonReload - 0.1f * upgrade3Levels[5])).ToString("F2");
        weaponDPSTexts[6].text = "Estimated DPS: " + ((settings.lightningDamage + 0.7f * upgrade1Levels[6]) / (settings.lightningReload - 0.05f * upgrade3Levels[6])).ToString("F2");
        weaponDPSTexts[7].text = "Estimated DPS: " + ((settings.spikesDamage + 0.5f * upgrade1Levels[7]) * ((settings.spikesCount + upgrade2Levels[7]) / 2) / (settings.spikesReload - 0.2f * upgrade3Levels[7])).ToString("F2");
        weaponDPSTexts[8].text = "Estimated DPS: " + ((settings.poisonDPS + upgrade1Levels[8] + 0.5f * upgrade3Levels[8]) * 4).ToString("F2");
        weaponDPSTexts[9].text = "Estimated DPS: " + ((settings.darkMagicDPS + 0.5f * upgrade1Levels[9]) * (4 + 0.1f * upgrade3Levels[9])).ToString("F2");
        weaponDPSTexts[10].text = "Estimated DPS: " + ((settings.sawDPS + 0.5f * (upgrade1Levels[10] + upgrade3Levels[10])) * 3).ToString("F2");
        weaponDPSTexts[11].text = "Estimated DPS: " + ((settings.sniperDamage + upgrade1Levels[11]) * (1 + 0.05f * upgrade3Levels[11]) * 3 / (settings.sniperReload - 0.2f * upgrade2Levels[11])).ToString("F2");
        weaponDPSTexts[12].text = "Estimated DPS: " + ((settings.shockerDamage + 0.5f * upgrade1Levels[12]) * 3 / (settings.shockerReload - 0.1f * upgrade2Levels[12])).ToString("F2");
        weaponDPSTexts[13].text = "Estimated DPS: " + ((settings.shotgunDamage + 0.2f * upgrade1Levels[13]) * (settings.shotgunBulletCount + upgrade3Levels[13]) / (settings.shotgunBulletCount - 0.2f * upgrade2Levels[13])).ToString("F2");
        weaponDPSTexts[14].text = "Estimated DPS: " + ((settings.grenadesDamage + upgrade1Levels[14]) * 3f / (settings.grenadesReload - 0.5f * upgrade2Levels[14])).ToString("F2");
        weaponDPSTexts[15].text = "Estimated DPS: " + (settings.pumpDamage + 0.7f * (upgrade1Levels[15] + upgrade2Levels[15])).ToString("F2");
        weaponDPSTexts[16].text = "Estimated DPS: " + ((settings.minigunDamage + 0.1f * upgrade1Levels[16]) / (settings.minigunReload - 0.02f * upgrade3Levels[16])).ToString("F2");
        weaponDPSTexts[17].text = "Estimated DPS: " + (settings.virusDamage + 0.5f * 5 * (upgrade1Levels[17] + upgrade2Levels[17])).ToString("F2");
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
            upgradeMax1Buttons[weapon].interactable = false;
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
                upgradeMax1Buttons[weapon].interactable = true;
            }
            else
            {
                upgrade1Buttons[weapon].interactable = false;
                upgradeMax1Buttons[weapon].interactable = false;
            }
        }
    }

    private void Upgrade2Refresh(int weapon)
    {
        upgrade2Sliders[weapon].value = (float)upgrade2Levels[weapon] / (float)upgrade2MaxLevels[weapon];
        if (upgrade2Levels[weapon] >= upgrade2MaxLevels[weapon])
        {
            upgrade2Buttons[weapon].interactable = false;
            upgradeMax2Buttons[weapon].interactable = false;
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
                upgradeMax2Buttons[weapon].interactable = true;
            }
            else
            {
                upgrade2Buttons[weapon].interactable = false;
                upgradeMax2Buttons[weapon].interactable = false;
            }
        }
    }

    private void Upgrade3Refresh(int weapon)
    {
        upgrade3Sliders[weapon].value = (float)upgrade3Levels[weapon] / (float)upgrade3MaxLevels[weapon];
        if (upgrade3Levels[weapon] >= upgrade3MaxLevels[weapon])
        {
            upgrade3Buttons[weapon].interactable = false;
            upgradeMax3Buttons[weapon].interactable = false;
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
                upgradeMax3Buttons[weapon].interactable = true;
            }
            else
            {
                upgrade3Buttons[weapon].interactable = false;
                upgradeMax3Buttons[weapon].interactable = false;
            }
        }
    }
}