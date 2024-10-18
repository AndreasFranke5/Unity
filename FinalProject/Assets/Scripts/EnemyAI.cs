using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target;           // The player tank that the enemy will follow
    public float shootingRange = 20f;  // Range within which the enemy can shoot
    public GameObject projectilePrefab;
    public Transform firePoint;        // Fire point for shooting projectiles
    public float fireRate = 2f;        // Time between shots
    public float projectileSpeed = 10f;

    private NavMeshAgent agent;        // NavMeshAgent for pathfinding
    private float nextFireTime = 0f;

    void Start()
    {
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Make the enemy move toward the player
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

    void Shoot()
    {
        // Instantiate the projectile and shoot toward the player
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Make the projectile move toward the player
        Vector3 direction = (target.position - firePoint.position).normalized;
        rb.AddForce(direction * projectileSpeed, ForceMode.Impulse);
    }
}
