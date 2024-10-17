using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;  // How long before the projectile is destroyed

    void Start()
    {
        // Destroy the projectile after a certain amount of time
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Optional: Add logic for what happens when the projectile hits something
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);  // Destroy the enemy object
            Destroy(gameObject);            // Destroy the projectile
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);  // Destroy the projectile when it hits a wall
        }
    }
}
