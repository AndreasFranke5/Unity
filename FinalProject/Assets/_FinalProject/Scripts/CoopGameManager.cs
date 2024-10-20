using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopGameManager : MonoBehaviour, IGameManager
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    // Powerup prefabs
    public GameObject powerupHomingPrefab;
    public GameObject powerupLaserPrefab;
    public GameObject powerupMortarPrefab;
    public GameObject powerupMultishotPrefab;

    // Player spawn points
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

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

    // Reference to the player GameObjects
    private GameObject player1Instance;
    private GameObject player2Instance;

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

    // Method to start the game and spawn both players
    public void StartGame()
    {
        Debug.Log("StartGame() method called in " + this.GetType().Name);
        gameStarted = true;  // Set the flag to true to start the game

        // Spawn both players
        player1Instance = Instantiate(player1Prefab, player1SpawnPoint.position, Quaternion.identity);
        player2Instance = Instantiate(player2Prefab, player2SpawnPoint.position, Quaternion.identity);

        // Log message to confirm game started
        Debug.Log("Co-op Game Started!");
    }

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

    private void SpawnEnemy()
    {
        Debug.Log("Spawning enemy...");
        int randomIndex = Random.Range(0, enemySpawnPoints.Length);
        Instantiate(enemyPrefab, enemySpawnPoints[randomIndex].position, Quaternion.identity);
        enemiesSpawnedThisRound++;  // Track how many enemies have spawned in this round
    }

    private void SpawnBoss()
    {
        Debug.Log("Spawning boss...");
        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossSpawned = true;
    }

    private void SpawnPowerup()
    {
        Debug.Log("Spawning powerup...");
        int randomIndex = Random.Range(0, powerupSpawnPoints.Length);
        int randomPowerup = Random.Range(0, 4);

        GameObject powerupToSpawn = null;

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

    public void OnEnemyShot()
    {
        enemiesShot++;

        if (enemiesShot >= maxEnemiesPerRound && !bossSpawned)
        {
            SpawnBoss();
        }
    }

    public void OnBossShot()
    {
        maxEnemiesPerRound++;
        enemiesShot = 0;
        bossSpawned = false;
        enemiesSpawnedThisRound = 0;
        SpawnEnemy();
    }

    public void TransformPlayer1(GameObject newPlayerPrefab)
    {
        Destroy(player1Instance);
        player1Instance = Instantiate(newPlayerPrefab, player1SpawnPoint.position, Quaternion.identity);
    }

    public void TransformPlayer2(GameObject newPlayerPrefab)
    {
        Destroy(player2Instance);
        player2Instance = Instantiate(newPlayerPrefab, player2SpawnPoint.position, Quaternion.identity);
    }
}
