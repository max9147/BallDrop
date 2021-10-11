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
        switch (id)
        {
            case 0:
                spawnedBall = Instantiate(ballPrefab, new Vector3(Random.Range(-1f, 1f) + levels[1].transform.position.x, 7f, 0f), Quaternion.identity);
                spawnedBall.GetComponent<SpriteRenderer>().color = ballColors[Random.Range(0, ballColors.Length)];
                StartCoroutine(WaitTimeFunnel());
                break;
            case 1:
                spawnedBall = Instantiate(ballPrefab, new Vector3(Random.Range(-1f, 1f) + levels[0].transform.position.x, 7f, 0f), Quaternion.identity);
                spawnedBall.GetComponent<SpriteRenderer>().color = ballColors[Random.Range(0, ballColors.Length)];
                StartCoroutine(WaitTimePochinko());
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitTimeFunnel()
    {
        yield return new WaitForSeconds(10f);
        SpawnBall(0);
    }

    private IEnumerator WaitTimePochinko()
    {
        yield return new WaitForSeconds(10f);
        SpawnBall(1);
    }
}