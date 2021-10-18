using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [Header("Laser")]
    public float laserRange;
    public float laserDPS;

    [Header("Gas")]
    public float gasRange;
    public float gasDPS;

    [Header("Gun")]
    public float gunRange;
    public float gunDamage;
    public float gunReload;

    [Header("Flamethrower")]
    public float flamethrowerRange;
    public float flamethrowerDPS;

    [Header("Hive")]
    public float hiveSpawnTime;
    public float hiveLifeTime;
    public float hiveDPS;

    [Header("Cannon")]
    public float cannonRange;
    public float cannonDamage;
    public float cannonReload;

    [Header("Lightning")]
    public float lightningRange;
    public float lightningDamage;
    public float lightningReload;

    [Header("Spikes")]
    public float spikesRange;
    public float spikesLifeTime;
    public float spikesDamage;
    public float spikesReload;
    public int spikesCount;

    [Header("Poison")]
    public float poisonTime;
    public float poisonDPS;

    [Header("Dark magic")]
    public float darkMagicSpawnTime;
    public float darkMagicLifeTime;
    public float darkMagicDPS;

    [Header("Saw")]
    public float sawRange;
    public float sawDPS;

    [Header("Sniper")]
    public float sniperDamage;
    public float sniperReload;

    [Header("Shocker")]
    public float shockerRange;
    public float shockerDamage;
    public float shockerReload;

    [Header("Shotgun")]
    public float shotgunRange;
    public float shotgunDamage;
    public float shotgunReload;
    public int shotgunBulletCount;

    [Header("Grenades")]
    public float grenadesRange;
    public float grenadesDamage;
    public float grenadesReload;
    public float grenadesDamageRange;

    [Header("Pump")]
    public float pumpDamage;

    [Header("Minigun")]
    public float minigunRange;
    public float minigunDamage;
    public float minigunReload;

    [Header("Virus")]
    public float virusTime;
    public float virusDamage;

    [Header("General")]
    public float ballSpawnTime;
    public float ballCost;
    public float ballMinHP;
    public float startMoney;
}