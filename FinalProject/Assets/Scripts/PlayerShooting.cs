using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject[] projectilePrefabs;  // Array of projectile prefabs for different tiers
    public Transform firePoint;
    public float projectileSpeed = 10f;
    private int currentProjectileTier = 0;  // Index of current projectile tier

    void Update()
    {
        // Shoot when Left Ctrl is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate the current tier projectile
        GameObject projectile = Instantiate(projectilePrefabs[currentProjectileTier], firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * projectileSpeed, ForceMode.Impulse);
    }

    // Function to upgrade projectile tier
    public void UpgradeProjectile()
    {
        if (currentProjectileTier < projectilePrefabs.Length - 1)
        {
            currentProjectileTier++;
        }
    }
}
