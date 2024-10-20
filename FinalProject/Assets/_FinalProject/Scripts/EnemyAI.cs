using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    public float shootingRange = 20f;  // Range within which the enemy can shoot
    public GameObject projectilePrefab;
    public Transform firePoint;        // Fire point for shooting projectiles
    public float fireRate = 2f;        // Time between shots
    public float projectileSpeed = 10f;

    private NavMeshAgent agent;        // NavMeshAgent for pathfinding
    private float nextFireTime = 0f;
    private Transform target;          // The player the enemy will target

    void Start()
    {
        // Get the NavMeshAgent component
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

        // Make the enemy move toward the closest player
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

    // Method to shoot the enemy's projectile
    void Shoot()
    {
        if (firePoint != null && projectilePrefab != null && target != null)
        {
            // Instantiate the projectile at the fire point
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            // Shoot the projectile toward the player
            Vector3 direction = (target.position - firePoint.position).normalized;
            rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
        }
    }
}
