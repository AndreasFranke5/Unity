using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasedCollisionForcePowerup : Powerup
{
    public float massMultiplier = 3.0f; // How much to increase the player's mass
    private float originalMass; // The original mass value
    private float originalSpeed; // The original speed value

    public override void ApplyEffect(PlayerController player)
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();

        // Store the original mass and speed
        originalMass = playerRb.mass;
        originalSpeed = player.speed;

        // Increase the player's mass
        playerRb.mass *= massMultiplier;

        // Increase the player's speed to compensate for the increased mass
        player.speed *= massMultiplier;
        
        Debug.Log("Increased mass and speed activated. New mass: " + playerRb.mass + ", New speed: " + player.speed);
    }

    public override void DeactivateEffect(PlayerController player)
    {
        Rigidbody playerRb = player.GetComponent<Rigidbody>();

        // Reset the player's mass and speed to the original values
        playerRb.mass = originalMass;
        player.speed = originalSpeed;
        
        Debug.Log("Increased mass and speed deactivated. Reset mass: " + playerRb.mass + ", Reset speed: " + player.speed);
    }
}
