using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarProjectile : MonoBehaviour
{
    public float launchAngle = 45f;
    public float launchSpeed = 10f;
    public float explosionRadius = 3f;
    public LayerMask damageLayerMask; // Layer mask for objects that can be damaged

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Calculate initial velocity components
        float angleRad = Mathf.Deg2Rad * launchAngle;
        Vector3 velocity = (transform.forward * Mathf.Cos(angleRad) + transform.up * Mathf.Sin(angleRad)) * launchSpeed;

        // Apply velocity to the Rigidbody
        rb.velocity = velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Explode upon collision
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        Debug.Log("Mortar exploded!");

        // Find all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, damageLayerMask);

        foreach (Collider nearbyObject in colliders)
        {
            // Check if the collider belongs to a player
            if (nearbyObject.CompareTag("Player1") || nearbyObject.CompareTag("Player2"))
            {
                Debug.Log("Mortar hit player: " + nearbyObject.gameObject.name);
                Destroy(nearbyObject.gameObject);

                // Handle player respawn
                MultiplayerGameManager gameManager = FindObjectOfType<MultiplayerGameManager>();
                if (gameManager != null)
                {
                    if (nearbyObject.CompareTag("Player1"))
                    {
                        gameManager.RespawnPlayer1();
                    }
                    else if (nearbyObject.CompareTag("Player2"))
                    {
                        gameManager.RespawnPlayer2();
                    }
                }
                else
                {
                    Debug.LogError("MultiplayerGameManager not found in the scene.");
                }
            }
        }
    }
}
