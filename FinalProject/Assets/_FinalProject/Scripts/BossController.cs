using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    private SingleplayerGameManager singleplayerGameManager;
    private CoopGameManager coopGameManager;

    void Start()
    {
        // Try to find any of the game managers
        singleplayerGameManager = FindObjectOfType<SingleplayerGameManager>();
        coopGameManager = FindObjectOfType<CoopGameManager>();

        if (singleplayerGameManager == null && coopGameManager == null)
        {
            Debug.LogError("No appropriate GameManager found in the scene. Make sure one is loaded.");
        }
    }

    void OnDestroy() // When the boss is defeated
    {
        if (singleplayerGameManager != null)
        {
            singleplayerGameManager.OnBossShot();
        }
        else if (coopGameManager != null)
        {
            coopGameManager.OnBossShot();
        }
        else
        {
            Debug.LogWarning("No valid GameManager reference found.");
        }
    }
}
