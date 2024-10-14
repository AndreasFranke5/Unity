using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    public float duration = 5.0f; // Default duration for the powerup effect

    // Method to apply the powerup effect
    public abstract void ApplyEffect(PlayerController player);

    // Method to deactivate the powerup effect
    public abstract void DeactivateEffect(PlayerController player);
}
