using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationBurstPowerup : Powerup
{
    public float burstRadius = 5.0f;
    public float burstForce = 20.0f;
    public GameObject particleEffectPrefab; // Reference to the particle effect prefab

    public override void ApplyEffect(PlayerController player)
    {
        Debug.Log("Radiation Burst Activated");

        // Instantiate the particle effect at the player's position
        if (particleEffectPrefab != null)
        {
            GameObject particleEffect = Instantiate(particleEffectPrefab, player.transform.position, Quaternion.identity);
            Destroy(particleEffect, 2.0f); // Destroy the particle effect after 2 seconds
        }

        // Apply radiation burst effect to nearby enemies
        Collider[] enemies = Physics.OverlapSphere(player.transform.position, burstRadius);
        foreach (Collider enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    Vector3 pushDirection = (enemy.transform.position - player.transform.position).normalized;
                    enemyRb.AddForce(pushDirection * burstForce, ForceMode.Impulse);
                    Debug.Log("Enemy pushed: " + enemy.name);
                }
            }
        }
    }

    public override void DeactivateEffect(PlayerController player)
    {
        // No ongoing effect to deactivate in this case
    }
}
