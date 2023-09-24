using System;
using Unity.PlasticSCM.Editor;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;
    public float spawnRange = 10f;
    public float spawnTimeSecs = 4f;
    public int howManyEnemiesToSpawn = 5;
    // public GameObject[] enemies; -> for in the event that we want multiple types of enemies
    
    private float enemyTimer;

    [Header("Pickup Spawning")] 
    public GameObject[] pickups;
    public float pickupSpawnRange = 5f;
    public float pickupSpawnTimeSeconds = 1.5f;
    public int howManyPickupsToSpawn = 3;

    private float pickupTimer;
    
    
    [Header("Difficulty Ramping")] 
    // public float difficultyRampingAdjust;
    // we're using levels as scoring here because I don't want to come up with a formula to adjust health and such
    public int levelEXPModifier = 5;
    public float levelHealthModifier = 10f;
    
    [HideInInspector] public int exp;
    [HideInInspector] public int nextLevelThreshold = 15;
    [HideInInspector] public int level;

    [HideInInspector] public int enemiesKilled = 0;

    #region Private references

    private GameObject player;
    private PlayerStats ps;

    #endregion
    
    [HideInInspector] public float timer;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        ps = player.GetComponent<PlayerStats>();

        exp = 0;
        enemyTimer = 1.5f;
        pickupTimer = 3f;
        timer = 0f;
        
        level = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        enemyTimer -= Time.deltaTime;
        pickupTimer -= Time.deltaTime;

        // enemy spawn timer
        if (enemyTimer <= -0.005f)
        {
            for (int i = 0; i < howManyEnemiesToSpawn; i++)
            {
                SpawnEnemy();
            }

            enemyTimer = spawnTimeSecs;
            
            Debug.Log("Spawned " + howManyEnemiesToSpawn + " enemies");
        }
        
        // pickup spawn timer
        if (pickupTimer <= -0.005f)
        {
            for (int i = 0; i < howManyPickupsToSpawn; i++)
            {
                SpawnPickup();
            }
            
            Debug.Log("Spawned " + howManyPickupsToSpawn + " pickups");

            pickupTimer = pickupSpawnTimeSeconds;
        }
        
        // level up
        if (exp >= nextLevelThreshold)
        {
            if (nextLevelThreshold != 0)
            {
                exp %= nextLevelThreshold;
            }
            else
            {
                exp = 0;
            }
            
            level += 1;
            nextLevelThreshold += levelEXPModifier;

            ps.maxHealth += levelHealthModifier;
            
            Debug.Log("Leveled up to level " + level + "!");
        }
    }
    
    // Spawn enemies
    private void SpawnEnemy()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint(spawnRange);

        Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
    }

    private void SpawnPickup()
    {
        Vector3 spawnPoint = GetRandomSpawnPoint(pickupSpawnRange);

        GameObject pickup = pickups[Random.Range(0, pickups.Length)];
        
        Instantiate(pickup, spawnPoint, Quaternion.identity);
    }

    // Get spawn point to spawn enemy
    private Vector3 GetRandomSpawnPoint(float range)
    {
        Vector3 playerPos = player.transform.position;

        float pointX = Random.Range(-playerPos.x - range, playerPos.x + range);
        float pointY = Random.Range(-playerPos.y - range, playerPos.y + range);

        return new Vector3(pointX, pointY, playerPos.z);
    }
}
