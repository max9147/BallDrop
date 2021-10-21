using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSystem : MonoBehaviour
{
    private float globalMul = 1;
    private GameObject spawnedBall;

    public Color[] ballColors;
    public GameObject ballPrefab;
    public GameObject weaponSystem;
    public GameObject[] levels;
    public GameSettings settings;

    private void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (weaponSystem.GetComponent<WeaponSystem>().CheckWeapon(i))
            {
                SpawnBall(i);
            }
        }
    }

    public void UpgradeGlobalMul()
    {
        globalMul -= 0.1f;
    }

    public void SetGlobalMul(int level)
    {
        globalMul = 1 - (0.1f * level);
    }

    public void SpawnBall(int id)
    {
        spawnedBall = Instantiate(ballPrefab, new Vector3(Random.Range(-1f, 1f) + levels[id].transform.position.x, 7f, 0f), Quaternion.identity);
        spawnedBall.GetComponent<SpriteRenderer>().color = ballColors[Random.Range(0, ballColors.Length)];
        switch (id)
        {
            case 0:                
                StartCoroutine(WaitTimePochinko());
                break;
            case 1:
                StartCoroutine(WaitTimeFunnel());
                break;
            case 2:
                StartCoroutine(WaitTimeGaps());
                break;
            case 3:
                StartCoroutine(WaitTimeSqueeze());
                break;
            case 4:
                StartCoroutine(WaitTimeMills());
                break;
            case 5:
                StartCoroutine(WaitTimeVerticals());
                break;
            case 6:
                StartCoroutine(WaitTimeMovement());
                break;
            case 7:
                StartCoroutine(WaitTimeTraps());
                break;
            case 8:
                StartCoroutine(WaitTimeMixer());
                break;
            case 9:
                StartCoroutine(WaitTimeElevator());
                break;
            case 10:
                StartCoroutine(WaitTimeBoulders());
                break;
            case 11:
                StartCoroutine(WaitTimeShrink());
                break;
            case 12:
                StartCoroutine(WaitTimePlatforms());
                break;
            case 13:
                StartCoroutine(WaitTimeDiamonds());
                break;
            case 14:
                StartCoroutine(WaitTimeSpinners());
                break;
            case 15:
                StartCoroutine(WaitTimeChoise());
                break;
            case 16:
                StartCoroutine(WaitTimeZigzag());
                break;
            case 17:
                StartCoroutine(WaitTimeFinal());
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitTimePochinko()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(0);
    }

    private IEnumerator WaitTimeFunnel()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(1);
    }

    private IEnumerator WaitTimeGaps()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(2);
    }

    private IEnumerator WaitTimeSqueeze()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(3);
    }

    private IEnumerator WaitTimeMills()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(4);
    }

    private IEnumerator WaitTimeVerticals()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(5);
    }

    private IEnumerator WaitTimeMovement()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(6);
    }

    private IEnumerator WaitTimeTraps()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(7);
    }

    private IEnumerator WaitTimeMixer()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(8);
    }

    private IEnumerator WaitTimeElevator()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(9);
    }

    private IEnumerator WaitTimeBoulders()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(10);
    }

    private IEnumerator WaitTimeShrink()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(11);
    }

    private IEnumerator WaitTimePlatforms()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(12);
    }

    private IEnumerator WaitTimeDiamonds()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(13);
    }

    private IEnumerator WaitTimeSpinners()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(14);
    }

    private IEnumerator WaitTimeChoise()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(15);
    }

    private IEnumerator WaitTimeZigzag()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(16);
    }

    private IEnumerator WaitTimeFinal()
    {
        yield return new WaitForSeconds(settings.ballSpawnTime * globalMul);
        SpawnBall(17);
    }
}