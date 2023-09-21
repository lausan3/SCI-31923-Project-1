using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Spawning")]
    public float spawnRange = 10f;

    public GameObject enemyPrefab;
    public float spawnTimeSecs = 4f;
    public int howManyEnemiesToSpawn = 5;
    public GameObject[] enemies;

    private GameObject player;
    private float timer;

    [Header("Difficulty Ramping")] 
    public float difficultyRampingAdjust;

    [HideInInspector] public int enemiesKilled = 0;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        timer = 1.5f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= -0.005f)
        {
            for (int i = 0; i < howManyEnemiesToSpawn; i++)
            {
                SpawnEnemy();
            }

            timer = spawnTimeSecs;
        }
    }

    // Spawn enemies
    private void SpawnEnemy()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint();

        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    // Get spawn point to spawn enemy
    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 playerPos = player.transform.position;

        float pointX = Random.Range(-playerPos.x + spawnRange, playerPos.x + spawnRange);
        float pointY = Random.Range(-playerPos.y + spawnRange, playerPos.y + spawnRange);

        return new Vector3(pointX, pointY, 1f);
    }
}
