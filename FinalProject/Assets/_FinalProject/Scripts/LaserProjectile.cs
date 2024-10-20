using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public float laserDuration = 0.2f;  // Short duration since laser is instant
    public float laserDistance = 50f;    // Adjust as needed
    public LayerMask hitMask;            // Layer mask to specify what the laser can hit

    public Material laserMaterial;       // Material for the laser

    private LineRenderer lineRenderer;

    void Start()
    {
        // Initialize the LineRenderer component
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            // Add a LineRenderer component if not already attached
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Configure LineRenderer settings
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.positionCount = 2;
        // lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        // lineRenderer.material.color = Color.red; // Set laser color
        lineRenderer.material = laserMaterial;

        // Shoot the laser
        ShootLaser();

        // Destroy the laser after a short duration
        Destroy(gameObject, laserDuration);
    }

    void ShootLaser()
    {
        RaycastHit hit;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + transform.forward * laserDistance;

        // Cast a ray forward from the laser's position
        if (Physics.Raycast(startPosition, transform.forward, out hit, laserDistance, hitMask))
        {
            endPosition = hit.point;

            // Check if we hit a player
            if (hit.collider.CompareTag("Player1") || hit.collider.CompareTag("Player2"))
            {
                Debug.Log("Laser hit player: " + hit.collider.gameObject.name);

                // Destroy the player
                Destroy(hit.collider.gameObject);

                // Handle player respawn
                MultiplayerGameManager gameManager = FindObjectOfType<MultiplayerGameManager>();
                if (gameManager != null)
                {
                    if (hit.collider.CompareTag("Player1"))
                    {
                        gameManager.RespawnPlayer1();
                    }
                    else if (hit.collider.CompareTag("Player2"))
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

        // Update the positions of the LineRenderer to match the laser
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
