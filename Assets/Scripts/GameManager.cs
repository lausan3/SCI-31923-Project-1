using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Enemy Spawning")]
    public float spawnRange;
    public int howManyEnemiesToSpawn = 5;
    public GameObject[] enemies;

    [Header("Difficulty Ramping")] 
    public float difficultyRampingAdjust;

    [HideInInspector] public int enemiesKilled = 0;
    
    
    // Spawn enemies
    private void SpawnEnemy()
    {
        
    }

    // Get spawn point to spawn enemy
    private void GetRandomSpawnPoint()
    {
        
    }
}
