using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSystem : MonoBehaviour
{
    private float spawnPosition;

    public GameObject ballPrefab;

    private void Start()
    {
        StartCoroutine(SpawnBall());
    }

    private IEnumerator SpawnBall()
    {
        yield return new WaitForSeconds(10f);
        spawnPosition = Random.Range(-1f, 1f);
        Instantiate(ballPrefab, new Vector3(spawnPosition, 7, 0), Quaternion.identity);
        StartCoroutine(SpawnBall());
    }
}