using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerGameManager : MonoBehaviour, IGameManager
{
    public GameObject playerPrefab;      // Prefab for the player
    public GameObject enemyPrefab;       // Prefab for enemies
    public GameObject bossPrefab;        // Prefab for the boss

    // Powerup prefabs
    public GameObject powerupHomingPrefab;
    public GameObject powerupLaserPrefab;
    public GameObject powerupMortarPrefab;
    public GameObject powerupMultishotPrefab;

    // Player spawn point
    public Transform player1SpawnPoint;

    // Enemy and boss spawn points
    public Transform[] enemySpawnPoints;
    public Transform bossSpawnPoint;

    // Powerup spawn points
    public Transform[] powerupSpawnPoints;

    // Timing for powerups
    public float powerupSpawnInterval = 10f;
    private float powerupSpawnTimer;

    // Enemy and boss spawn management
    private int enemiesShot = 0;
    private int maxEnemiesPerRound = 5;  // Start with 5 enemies for the first round
    private bool bossSpawned = false;
    private int enemiesSpawnedThisRound = 0;  // Number of enemies spawned in the current round
    private float enemySpawnInterval = 3f;    // Time between enemy spawns
    private float enemySpawnTimer;

    // Reference to the player
    private GameObject playerInstance;

    // Game state control
    private bool gameStarted = false;  // Flag to check if the game has started

    void Start()
    {
        // Reset the powerup and enemy timers
        powerupSpawnTimer = powerupSpawnInterval;
        enemySpawnTimer = enemySpawnInterval;
    }

    void Update()
    {
        if (gameStarted)
        {
            // Manage powerup spawning over time
            HandlePowerupSpawning();

            // Manage enemy spawning
            if (!bossSpawned && enemiesSpawnedThisRound < maxEnemiesPerRound)
            {
                HandleEnemySpawning();
            }
        }
    }

    // Method to start the game and spawn the player
    public void StartGame()
{
    Debug.Log("StartGame() method called in " + this.GetType().Name);
    gameStarted = true;  // Set the flag to true to start the game

    // Spawn the player
    playerInstance = Instantiate(playerPrefab, player1SpawnPoint.position, Quaternion.identity);
    enemiesShot = 0;
    enemiesSpawnedThisRound = 0;
    bossSpawned = false;

    // Log message to confirm game started
    Debug.Log("Game Started!");
}


    // Powerup spawning logic
    private void HandlePowerupSpawning()
    {
        powerupSpawnTimer -= Time.deltaTime;

        if (powerupSpawnTimer <= 0)
        {
            SpawnPowerup();
            powerupSpawnTimer = powerupSpawnInterval;  // Reset timer
        }
    }

    private void HandleEnemySpawning()
    {
        enemySpawnTimer -= Time.deltaTime;

        if (enemySpawnTimer <= 0 && enemiesSpawnedThisRound < maxEnemiesPerRound)
        {
            SpawnEnemy();
            enemySpawnTimer = enemySpawnInterval;  // Reset timer
        }
    }

    // Method to spawn enemies at random points
    private void SpawnEnemy()
    {
        Debug.Log("Spawning Enemy...");
        int randomIndex = Random.Range(0, enemySpawnPoints.Length);
        Instantiate(enemyPrefab, enemySpawnPoints[randomIndex].position, Quaternion.identity);
        enemiesSpawnedThisRound++;  // Track how many enemies have spawned in this round
    }

    // Method to spawn the boss
    private void SpawnBoss()
    {
        Debug.Log("Spawning Boss...");
        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossSpawned = true;
    }

    // Method to spawn powerups at random points with random powerup types
    private void SpawnPowerup()
    {
        Debug.Log("Spawning Powerup...");
        int randomIndex = Random.Range(0, powerupSpawnPoints.Length);
        int randomPowerup = Random.Range(0, 4); // 4 powerup types

        GameObject powerupToSpawn = null;

        // Randomly select the powerup type to spawn
        switch (randomPowerup)
        {
            case 0:
                powerupToSpawn = powerupHomingPrefab;
                break;
            case 1:
                powerupToSpawn = powerupLaserPrefab;
                break;
            case 2:
                powerupToSpawn = powerupMortarPrefab;
                break;
            case 3:
                powerupToSpawn = powerupMultishotPrefab;
                break;
        }

        Instantiate(powerupToSpawn, powerupSpawnPoints[randomIndex].position, Quaternion.identity);
    }

    // Method to call when an enemy is shot
    public void OnEnemyShot()
    {
        enemiesShot++;

        // If all enemies in this round have been shot, spawn the boss
        if (enemiesShot >= maxEnemiesPerRound && !bossSpawned)
        {
            SpawnBoss();
        }
    }

    // Method to call when the boss is shot
    public void OnBossShot()
    {
        Debug.Log("Boss defeated!");

        // Increment the enemy limit for the next round
        maxEnemiesPerRound++;

        // Reset the round for enemies and boss
        enemiesShot = 0;
        bossSpawned = false;
        enemiesSpawnedThisRound = 0; // Reset the number of enemies spawned for the next round

        // Restart the enemy spawn cycle for the next round
        SpawnEnemy();
    }

    // Method to handle player transformation after picking up a powerup
    public void TransformPlayer(GameObject newPlayerPrefab)
    {
        // Destroy the current player
        Destroy(playerInstance);

        // Spawn the new player (transformed tank) at the same position
        playerInstance = Instantiate(newPlayerPrefab, player1SpawnPoint.position, Quaternion.identity);
    }
}
