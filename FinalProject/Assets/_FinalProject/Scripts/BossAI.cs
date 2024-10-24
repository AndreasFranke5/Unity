using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 3f;          // Time between shots
    public float projectileSpeed = 20f;  // Speed of the boss's projectile
    public float moveSpeed = 2f;         // Speed of the boss movement
    public float shootingRange = 30f;    // Range within which the boss will shoot
    private float nextFireTime = 0f;     // Timer for shooting

    private Transform target;            // Current target (Player1 or Player2)
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Find Player1 and Player2 in the scene by their tags
        player1 = GameObject.FindWithTag("Player1").transform;
        player2 = GameObject.FindWithTag("Player2").transform;

        // Set the initial target to the closest player
        target = GetClosestPlayer();
    }

    void Update()
    {
        // Update target to the closest player
        target = GetClosestPlayer();

        // Move toward the closest player
        if (target != null)
        {
            agent.SetDestination(target.position);

            // If within shooting range, stop and shoot
            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            if (distanceToTarget <= shootingRange && Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    // Get the closest player (Player1 or Player2)
    Transform GetClosestPlayer()
    {
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.position);

        // Return the closest player
        return (distanceToPlayer1 < distanceToPlayer2) ? player1 : player2;
    }

    // Method to shoot the boss's projectile
    void Shoot()
    {
        if (firePoint != null && projectilePrefab != null && target != null)
        {
            // Instantiate the boss projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            // Shoot the projectile toward the player
            Vector3 direction = (target.position - firePoint.position).normalized;
            rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
        }
    }
}
