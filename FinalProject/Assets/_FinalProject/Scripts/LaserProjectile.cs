using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    public float laserDuration = 3f;

    void Start()
    {
        Invoke("DestroyLaser", laserDuration);  // Laser disappears after a few seconds
    }

    void Update()
    {
        // Move the laser forward continuously
        transform.Translate(Vector3.forward * Time.deltaTime * 50f);
    }

    void DestroyLaser()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        // Stop moving once it hits something, but don’t destroy it immediately
        laserDuration -= 1f;  // Optional: shorten remaining time
    }
}
