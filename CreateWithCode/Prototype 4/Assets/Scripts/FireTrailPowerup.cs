using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FireTrailPowerup : Powerup
{
    public GameObject fireTrailPrefab; // Prefab for the fire trail
    public float trailDuration = 5.0f; // Duration of the trail effect
    public float launchForce = 20.0f;  // Force to launch enemies

    private Coroutine trailCoroutine;

    public override void ApplyEffect(PlayerController player)
    {
        Debug.Log("Fire trail activated");

        // Start the coroutine to create the fire trail
        trailCoroutine = player.StartCoroutine(CreateFireTrail(player));
    }

    public override void DeactivateEffect(PlayerController player)
    {
        // Stop the coroutine if it's still running
        if (trailCoroutine != null)
        {
            player.StopCoroutine(trailCoroutine);
            trailCoroutine = null;
        }

        Debug.Log("Fire trail deactivated");
    }

    private IEnumerator CreateFireTrail(PlayerController player)
    {
        float elapsed = 0f;

        while (elapsed < trailDuration)
        {
            // Check if the fireTrailPrefab is not null
            if (fireTrailPrefab != null)
            {
                // Instantiate a fire trail segment at the player's position
                GameObject fireTrail = Instantiate(fireTrailPrefab, player.transform.position, Quaternion.identity);
                if (fireTrail != null)
                {
                Destroy(fireTrail, 2.0f);
                }
            }
            else
            {
                Debug.LogWarning("FireTrailPrefab is missing. Please assign it in the Inspector.");
                yield break; // Exit the coroutine if the prefab is missing
            }

            // Wait for a short interval before creating the next segment
            yield return new WaitForSeconds(0.1f);

            elapsed += 0.1f;
        }
    }
}
