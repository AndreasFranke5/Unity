using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float rotateSpeed = 200f;
    private Transform target;

    private GameObject shooter;

    void Start()
    {
        // Get reference to the shooter
        shooter = GetComponent<ProjectileShooterReference>().shooter;

        // Find the other player as the target
        FindTarget();
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to the target
            Vector3 direction = (target.position - transform.position).normalized;
            // Calculate the rotation step
            float rotateStep = rotateSpeed * Time.deltaTime;
            // Determine the new direction by rotating towards the target
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateStep * Mathf.Deg2Rad, 0f);
            // Apply the rotation
            transform.rotation = Quaternion.LookRotation(newDirection);
            // Move forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            // If no target, move forward
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    void FindTarget()
    {
        // Find the other player
        if (shooter.CompareTag("Player1"))
        {
            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            if (player2 != null)
            {
                target = player2.transform;
            }
        }
        else if (shooter.CompareTag("Player2"))
        {
            GameObject player1 = GameObject.FindGameObjectWithTag("Player1");
            if (player1 != null)
            {
                target = player1.transform;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == shooter)
        {
            // Ignore collision with the shooter
            return;
        }

        if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
        {
            Debug.Log("Homing projectile hit player: " + collision.gameObject.name);
            Destroy(collision.gameObject);

            // Handle player respawn
            MultiplayerGameManager gameManager = FindObjectOfType<MultiplayerGameManager>();
            if (gameManager != null)
            {
                if (collision.gameObject.CompareTag("Player1"))
                {
                    gameManager.RespawnPlayer1();
                }
                else if (collision.gameObject.CompareTag("Player2"))
                {
                    gameManager.RespawnPlayer2();
                }
            }
            else
            {
                Debug.LogError("MultiplayerGameManager not found in the scene.");
            }
        }

        // Destroy the homing projectile on impact
        Destroy(gameObject);
    }
}
