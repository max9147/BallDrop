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
    public float hiveBeeLifeTime;
    public float hiveBeeDamage;

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
    public float poisonDamage;

    [Header("General")]
    public float ballMinHP;
}