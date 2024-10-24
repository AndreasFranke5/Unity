using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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

    void OnDestroy()
    {
        if (singleplayerGameManager != null)
        {
            singleplayerGameManager.OnEnemyShot();
        }
        else if (coopGameManager != null)
        {
            coopGameManager.OnEnemyShot();
        }
        else
        {
            Debug.LogWarning("No valid GameManager reference found.");
        }
    }
}
