using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner; // Reference to the EnemySpawner
    [SerializeField] private int startingEnemiesPerWave = 3; // Initial number of enemies per wave
    [SerializeField] private float waveInterval = 10f; // Time between waves
    [SerializeField] private float difficultyMultiplier = 1.1f; // Multiplier for enemy attributes each wave

    private int currentWave = 0;
    private int enemiesToSpawn;
    private float waveTimer;
    private bool isWaveActive;

    private void Start()
    {
        enemiesToSpawn = startingEnemiesPerWave;
        StartNewWave();
    }

    private void Update()
    {
        if (!isWaveActive)
        {
            waveTimer += Time.deltaTime;
            if (waveTimer >= waveInterval)
            {
                StartNewWave();
            }
        }
    }

    private void StartNewWave()
    {
        currentWave++;
        waveTimer = 0f;
        isWaveActive = true;

        // Spawn enemies for the wave
        StartCoroutine(SpawnWaveEnemies());
    }

    private System.Collections.IEnumerator SpawnWaveEnemies()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            // Get a random enemy and adjust its stats
            EnemyData enemyData = enemySpawner.GetRandomEnemyData();
            EnemyData adjustedEnemy = AdjustEnemyAttributes(enemyData);

            // Spawn the enemy with the adjusted stats
            enemySpawner.SpawnEnemy(adjustedEnemy);

            yield return new WaitForSeconds(0.5f); // Delay between spawns
        }

        // Increase difficulty for the next wave
        enemiesToSpawn += 2; // Increase the number of enemies
        isWaveActive = false;
    }

    private EnemyData AdjustEnemyAttributes(EnemyData original)
    {
        EnemyData adjusted = ScriptableObject.CreateInstance<EnemyData>();

        adjusted.enemyName = original.enemyName;
        adjusted.enemyPrefab = original.enemyPrefab;

        // Scale attributes with wave count
        adjusted.health = original.health * Mathf.Pow(difficultyMultiplier, currentWave - 1);
        adjusted.attackDamage = original.attackDamage * Mathf.Pow(difficultyMultiplier, currentWave - 1);
        adjusted.speed = original.speed; // Optionally adjust speed
        adjusted.scoreValue = original.scoreValue * currentWave; // Optional: increase score value

        adjusted.attackRange = original.attackRange;
        adjusted.projectilePrefab = original.projectilePrefab;
        adjusted.fireRate = original.fireRate;

        return adjusted;
    }
}
