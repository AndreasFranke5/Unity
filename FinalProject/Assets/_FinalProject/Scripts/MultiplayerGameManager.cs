using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerGameManager : MonoBehaviour
{
    public GameObject player1Prefab;     // Prefab for player 1
    public GameObject player2Prefab;     // Prefab for player 2

    // Powerup prefabs (tank transformations)
    public GameObject powerupHomingPrefab;
    public GameObject powerupLaserPrefab;
    public GameObject powerupMortarPrefab;
    public GameObject powerupMultishotPrefab;

    // Player spawn points
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    // Powerup spawn points
    public Transform[] powerupSpawnPoints;

    // Timing for powerups
    public float powerupSpawnInterval = 10f;  // Interval between powerup spawns
    private float powerupSpawnTimer;

    // Reference to the player GameObjects in the scene
    private GameObject player1Instance;
    private GameObject player2Instance;

    void Start()
    {
        // Spawn both players
        player1Instance = Instantiate(player1Prefab, player1SpawnPoint.position, Quaternion.identity);
        player2Instance = Instantiate(player2Prefab, player2SpawnPoint.position, Quaternion.identity);

        // Reset the powerup timer
        powerupSpawnTimer = powerupSpawnInterval;
    }

    void Update()
    {
        // Manage powerup spawning over time
        HandlePowerupSpawning();
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

    // Method to spawn powerups at random points with random powerup types
    private void SpawnPowerup()
    {
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

        // Spawn the selected powerup
        Instantiate(powerupToSpawn, powerupSpawnPoints[randomIndex].position, Quaternion.identity);
    }

    // Method to handle player 1 transformation after picking up a powerup
    public void TransformPlayer1(GameObject newPlayerPrefab)
    {
        // Destroy the current player
        Destroy(player1Instance);

        // Spawn the new player (transformed tank) at the same position
        player1Instance = Instantiate(newPlayerPrefab, player1SpawnPoint.position, Quaternion.identity);
    }

    // Method to handle player 2 transformation after picking up a powerup
    public void TransformPlayer2(GameObject newPlayerPrefab)
    {
        // Destroy the current player
        Destroy(player2Instance);

        // Spawn the new player (transformed tank) at the same position
        player2Instance = Instantiate(newPlayerPrefab, player2SpawnPoint.position, Quaternion.identity);
    }
}
