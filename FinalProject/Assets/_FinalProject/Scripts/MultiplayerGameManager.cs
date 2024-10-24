using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerGameManager : MonoBehaviour, IGameManager
{
    public GameObject player1Prefab;     // Prefab for player 1
    public GameObject player2Prefab;     // Prefab for player 2

    // Powerup prefabs (tank transformations)
    // public GameObject powerupHomingPrefab;
    public GameObject powerupLaserPrefab;
    // public GameObject powerupMortarPrefab;
    // public GameObject powerupMultishotPrefab;

    // Player spawn points
    public Transform player1SpawnPoint;
    public Transform player2SpawnPoint;

    public PowerupSpawnPoint[] powerupSpawnPoints;
    public PowerupSpawnPoint spawnPoint;


    // Timing for powerups
    public float powerupSpawnInterval = 10f;  // Interval between powerup spawns
    private float powerupSpawnTimer;

    // Reference to the player GameObjects in the scene
    public GameObject player1Instance;
    public GameObject player2Instance;

    // Game state control
    private bool gameStarted = false;  // Flag to check if the game has started

    void Start()
    {
        Debug.Log("MultiplayerGameManager Start() method called.");
        // Reset the powerup timer
        powerupSpawnTimer = powerupSpawnInterval;
        StartGame();
    }

    void Update()
    {
        if (gameStarted)
        {
            // Manage powerup spawning over time
            HandlePowerupSpawning();
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
        Debug.Log("Multiplayer Game Started!");
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

    [System.Serializable]
    public class PowerupSpawnPoint
    {
        public Transform spawnTransform;
        public bool isOccupied;
    }

private void SpawnPowerup()
{
    Debug.Log("Spawning Powerup...");

    // Get a list of unoccupied spawn points
    List<PowerupSpawnPoint> availableSpawnPoints = new List<PowerupSpawnPoint>();
    foreach (var spawnPoint in powerupSpawnPoints)
    {
        if (!spawnPoint.isOccupied)
        {
            availableSpawnPoints.Add(spawnPoint);
        }
    }

    if (availableSpawnPoints.Count == 0)
    {
        Debug.Log("No available spawn points for power-ups.");
        return; // Exit if no spawn points are available
    }

    // Select a random available spawn point
    int randomIndex = Random.Range(0, availableSpawnPoints.Count);
    PowerupSpawnPoint selectedSpawnPoint = availableSpawnPoints[randomIndex];

    // Select the power-up to spawn (since only laser is enabled)
    GameObject powerupToSpawn = powerupLaserPrefab;

    // Spawn the selected power-up
    GameObject spawnedPowerup = Instantiate(powerupToSpawn, selectedSpawnPoint.spawnTransform.position, powerupToSpawn.transform.rotation);

    // Mark the spawn point as occupied
    selectedSpawnPoint.isOccupied = true;

    // Let the power-up know which spawn point it came from
    PowerupController powerupController = spawnedPowerup.GetComponent<PowerupController>();
    if (powerupController != null)
    {
        powerupController.spawnPoint = selectedSpawnPoint;
    }
    else
    {
        Debug.LogError("PowerupController not found on the spawned power-up.");
    }
}

    // Method to spawn powerups at random points with random powerup types
    // private void SpawnPowerup()
    // {
    //     Debug.Log("Spawning Powerup...");
    //     int randomIndex = Random.Range(0, powerupSpawnPoints.Length);
        // int randomPowerup = Random.Range(0, 1); // 4 powerup types

        // GameObject powerupToSpawn = null;
        // GameObject powerupToSpawn = powerupLaserPrefab; // Temporarily only allow laser powerup

        // Randomly select the powerup type to spawn
        // switch (randomPowerup)
        // {
            // case 0:
            //     powerupToSpawn = powerupHomingPrefab;
            //     break;
            // case 1:
            //     powerupToSpawn = powerupLaserPrefab;
            //     break;
            // case 2:
            //     powerupToSpawn = powerupMortarPrefab;
            //     break;
            // case 3:
            //     powerupToSpawn = powerupMultishotPrefab;
            //     break;
        // }

        // Spawn the selected powerup
    //     Instantiate(powerupToSpawn, powerupSpawnPoints[randomIndex].position, powerupToSpawn.transform.rotation);
    // }

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

    public void RespawnPlayer1()
{
    StartCoroutine(RespawnPlayerCoroutine(player1Prefab, player1SpawnPoint, "Player1"));
}

public void RespawnPlayer2()
{
    StartCoroutine(RespawnPlayerCoroutine(player2Prefab, player2SpawnPoint, "Player2"));
}

private IEnumerator RespawnPlayerCoroutine(GameObject playerPrefab, Transform spawnPoint, string playerTag)
{
    Debug.Log("Respawning " + playerTag + " in 3 seconds...");
    yield return new WaitForSeconds(3f); // Wait for 3 seconds before respawning

    // Instantiate the player at the spawn point
    GameObject newPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    newPlayer.tag = playerTag; // Ensure the tag is set correctly

    // Update the player instance reference
    if (playerTag == "Player1")
    {
        player1Instance = newPlayer;
    }
    else if (playerTag == "Player2")
    {
        player2Instance = newPlayer;
    }

    Debug.Log(playerTag + " has respawned.");
}

}
