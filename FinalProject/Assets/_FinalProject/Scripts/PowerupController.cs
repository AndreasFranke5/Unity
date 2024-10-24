using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public enum PowerupType { Homing, Laser, Mortar, Multishot }  // Types of powerups
    public PowerupType powerupType;

    public GameObject player1HomingPrefab;
    public GameObject player2HomingPrefab;
    public GameObject player1LaserPrefab;
    public GameObject player2LaserPrefab;
    public GameObject player1MortarPrefab;
    public GameObject player2MortarPrefab;
    public GameObject player1MultishotPrefab;
    public GameObject player2MultishotPrefab;
    private bool isCollected = false;


    private void OnTriggerEnter(Collider other)
{
    Debug.Log("Power-up collided with: " + other.gameObject.name);

    if (other.CompareTag("Player1") || other.CompareTag("Player2"))
    {
        Debug.Log("Player detected: " + other.gameObject.name);

        // Prevent multiple pickups
        if (!isCollected)
        {
            isCollected = true;

            // Disable the collider to prevent further triggers
            Collider powerupCollider = GetComponent<Collider>();
            if (powerupCollider != null)
            {
                powerupCollider.enabled = false;
            }

            TransformPlayer(other.gameObject);

            // Mark the spawn point as unoccupied
            if (spawnPoint != null)
            {
                spawnPoint.isOccupied = false;
            }
            
            // Destroy the power-up
            Destroy(gameObject);
        }
    }
}


    // Transforms the player based on the powerup collected
    void TransformPlayer(GameObject player, bool isPlayer1)
    {
        GameObject newPrefab = null;

        switch (powerupType)
        {
            case PowerupType.Homing:
                newPrefab = isPlayer1 ? player1HomingPrefab : player2HomingPrefab;
                break;
            case PowerupType.Laser:
                newPrefab = isPlayer1 ? player1LaserPrefab : player2LaserPrefab;
                break;
            case PowerupType.Mortar:
                newPrefab = isPlayer1 ? player1MortarPrefab : player2MortarPrefab;
                break;
            case PowerupType.Multishot:
                newPrefab = isPlayer1 ? player1MultishotPrefab : player2MultishotPrefab;
                break;
            default:
                Debug.LogError("Invalid powerup type: " + powerupType);
                return;
        }

        // If the correct prefab is found, replace the player with the new one
        if (newPrefab != null)
        {
            Vector3 position = player.transform.position;
            Quaternion rotation = player.transform.rotation;
            Destroy(player);  // Destroy the current player

            // Instantiate the new player prefab
            GameObject newPlayer = Instantiate(newPrefab, position, rotation);
            newPlayer.tag = isPlayer1 ? "Player1" : "Player2";

            // Update the player instance in the GameManager
            MultiplayerGameManager gameManager = FindObjectOfType<MultiplayerGameManager>();
            if (gameManager != null)
            {
                if (isPlayer1)
                {
                    gameManager.player1Instance = newPlayer;
                }
                else
                {
                    gameManager.player2Instance = newPlayer;
                }
            }
            Destroy(gameObject);

            Debug.Log("Player transformed to: " + newPrefab.name);
        }
        else
        {
            Debug.LogError("No prefab assigned for the power-up type: " + powerupType);
        }
    }
}
