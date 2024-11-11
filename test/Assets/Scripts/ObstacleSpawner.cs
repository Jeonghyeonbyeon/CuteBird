using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private PlayerController playerController;
    private float spawnInterval = 3f;
    private float minSpawnInterval = 1.25f;
    private float spawnReductionRate = 0.05f;

    void Start()
    {
        StartCoroutine(ObstacleSpawn());
    }

    IEnumerator ObstacleSpawn()
    {
        while (!playerController.isDie)
        {
            yield return new WaitForSecondsRealtime(spawnInterval);
            Instantiate(obstaclePrefab, new Vector3(5f, Random.Range(2.5f, -0.25f), 0f), Quaternion.identity);

            spawnInterval = Mathf.Max(spawnInterval - spawnReductionRate, minSpawnInterval);
        }
    }
}