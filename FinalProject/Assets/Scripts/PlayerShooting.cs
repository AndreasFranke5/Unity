using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab;  // The projectile prefab
    public Transform firePoint;          // The location from where the projectile is fired
    public float projectileSpeed = 10f;  // Speed of the projectile

    void Update()
    {
        // Check for shooting input (Left Ctrl for Player 1)
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the firePoint's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        // Add forward force to the projectile
        rb.AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse);
    }
}
