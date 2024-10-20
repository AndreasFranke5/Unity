using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float spreadAngle = 45f;

    void Start()
    {
        // Fire the main shot
        FireProjectile(0);

        // Fire the diagonal shots
        FireProjectile(spreadAngle);
        FireProjectile(-spreadAngle);
    }

    void FireProjectile(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = projectile.transform.forward * 10f;
    }
}
