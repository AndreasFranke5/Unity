using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrailSegment : MonoBehaviour
{
    public float launchForce = 20.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = other.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                // Launch the enemy in the direction they are currently moving
                Vector3 launchDirection = enemyRb.velocity.normalized;
                enemyRb.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            }
        }
    }
}
