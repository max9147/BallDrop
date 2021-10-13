using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSystem : MonoBehaviour
{
    private GameObject spawnedBall;

    public Color[] ballColors;
    public GameObject ballPrefab;
    public GameObject weaponSystem;
    public GameObject[] levels;

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
            default:
                break;
        }
    }

    private IEnumerator WaitTimePochinko()
    {
        yield return new WaitForSeconds(10f);
        SpawnBall(0);
    }

    private IEnumerator WaitTimeFunnel()
    {
        yield return new WaitForSeconds(10f);
        SpawnBall(1);
    }

    private IEnumerator WaitTimeGaps()
    {
        yield return new WaitForSeconds(10f);
        SpawnBall(2);
    }

    private IEnumerator WaitTimeSqueeze()
    {
        yield return new WaitForSeconds(10f);
        SpawnBall(3);
    }

    private IEnumerator WaitTimeMills()
    {
        yield return new WaitForSeconds(10f);
        SpawnBall(4);
    }

    private IEnumerator WaitTimeVerticals()
    {
        yield return new WaitForSeconds(10f);
        SpawnBall(5);
    }
}