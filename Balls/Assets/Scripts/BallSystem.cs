using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSystem : MonoBehaviour
{
    private float spawnPosition;
    private GameObject spawnedBall;

    public Color[] ballColors;
    public GameObject ballPrefab;

    private void Start()
    {
        SpawnFirstBall();
    }

    private void SpawnFirstBall()
    {
        spawnPosition = Random.Range(-1f, 1f);
        spawnedBall = Instantiate(ballPrefab, new Vector3(spawnPosition, 7, 0), Quaternion.identity);
        spawnedBall.GetComponent<SpriteRenderer>().color = ballColors[Random.Range(0, ballColors.Length)];
        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(10f);
        spawnPosition = Random.Range(-1f, 1f);
        spawnedBall = Instantiate(ballPrefab, new Vector3(spawnPosition, 7, 0), Quaternion.identity);
        spawnedBall.GetComponent<SpriteRenderer>().color = ballColors[Random.Range(0, ballColors.Length)];
        StartCoroutine(SpawnBall());
    }
}