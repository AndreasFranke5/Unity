using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarProjectile : MonoBehaviour
{
    public float launchAngle = 45f;
    public float range = 4f; // The fixed distance where it lands
    public float explosionRadius = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Calculate velocity to hit target distance
        float gravity = Physics.gravity.magnitude;
        float angleRad = Mathf.Deg2Rad * launchAngle;
        float velocity = Mathf.Sqrt(range * gravity / Mathf.Sin(2 * angleRad));

        // Apply velocity
        rb.velocity = transform.forward * velocity * Mathf.Cos(angleRad) + transform.up * velocity * Mathf.Sin(angleRad);
    }

    void OnCollisionEnter(Collision other)
    {
        // Create explosion and destroy on impact
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        // Explosion logic here (optional)
        Debug.Log("Mortar exploded!");
    }
}
