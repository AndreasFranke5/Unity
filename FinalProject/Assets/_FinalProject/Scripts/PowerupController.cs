using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public enum PowerupType { Homing, Laser, Mortar, Multishot }  // Types of powerups
    public PowerupType powerupType;  // The type of this powerup, set in the Inspector

    public GameObject player1HomingPrefab;
    public GameObject player2HomingPrefab;
    public GameObject player1LaserPrefab;
    public GameObject player2LaserPrefab;
    public GameObject player1MortarPrefab;
    public GameObject player2MortarPrefab;
    public GameObject player1MultishotPrefab;
    public GameObject player2MultishotPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1"))
        {
            TransformPlayer(other.gameObject, true);  // Player1 collects the powerup
        }
        else if (other.CompareTag("Player2"))
        {
            TransformPlayer(other.gameObject, false);  // Player2 collects the powerup
        }

        // Destroy the powerup after it's collected
        Destroy(gameObject);
    }

    // Transforms the player based on the powerup collected
    void TransformPlayer(GameObject player, bool isPlayer1)
    {
        GameObject newPrefab = null;

        // Choose the correct prefab based on the powerup type and which player is collecting it
        if (powerupType == PowerupType.Homing)
        {
            newPrefab = isPlayer1 ? player1HomingPrefab : player2HomingPrefab;
        }
        else if (powerupType == PowerupType.Laser)
        {
            newPrefab = isPlayer1 ? player1LaserPrefab : player2LaserPrefab;
        }
        else if (powerupType == PowerupType.Mortar)
        {
            newPrefab = isPlayer1 ? player1MortarPrefab : player2MortarPrefab;
        }
        else if (powerupType == PowerupType.Multishot)
        {
            newPrefab = isPlayer1 ? player1MultishotPrefab : player2MultishotPrefab;
        }

        // If the correct prefab is found, replace the player with the new one
        if (newPrefab != null)
        {
            Vector3 position = player.transform.position;
            Quaternion rotation = player.transform.rotation;
            Destroy(player);  // Destroy the current player

            // Instantiate the new player prefab at the same position and rotation
            Instantiate(newPrefab, position, rotation);
        }
    }
}
