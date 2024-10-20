using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoopGameManager : MonoBehaviour
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

    void Start()
    {
        // Spawn both players
        player1Instance = Instantiate(player1Prefab, player1SpawnPoint.position, Quaternion.identity);
        player2Instance = Instantiate(player2Prefab, player2SpawnPoint.position, Quaternion.identity);

        // Reset the powerup and enemy timers
        powerupSpawnTimer = powerupSpawnInterval;
        enemySpawnTimer = enemySpawnInterval;
    }

    void Update()
    {
        // Manage powerup spawning over time
        HandlePowerupSpawning();

        // Manage enemy spawning
        if (!bossSpawned && enemiesSpawnedThisRound < maxEnemiesPerRound)
        {
            HandleEnemySpawning();
        }
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
        int randomIndex = Random.Range(0, enemySpawnPoints.Length);
        Instantiate(enemyPrefab, enemySpawnPoints[randomIndex].position, Quaternion.identity);
        enemiesSpawnedThisRound++;  // Track how many enemies have spawned in this round
    }

    private void SpawnBoss()
    {
        Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        bossSpawned = true;
    }

    private void SpawnPowerup()
    {
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
