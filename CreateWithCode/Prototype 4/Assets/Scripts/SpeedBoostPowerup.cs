using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostPowerup : Powerup
{
    public float speedMultiplier = 2.0f;

    public override void ApplyEffect(PlayerController player)
    {
        player.speed *= speedMultiplier; // Increase player speed
    }

    public override void DeactivateEffect(PlayerController player)
    {
        player.speed /= speedMultiplier; // Reset speed back to normal
    }
}
