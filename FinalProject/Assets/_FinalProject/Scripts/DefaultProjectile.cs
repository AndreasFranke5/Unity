using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultProjectile : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

void OnCollisionEnter(Collision collision)
{
    // Check if the projectile hit a player
    if (collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
    {
        Debug.Log("Projectile hit: " + collision.gameObject.name);

        // Destroy the player
        Destroy(collision.gameObject);

        // Get the GameManager to respawn the player
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            MultiplayerGameManager gameManager = gameManagerObject.GetComponent<MultiplayerGameManager>();
            if (gameManager != null)
            {
                if (collision.gameObject.CompareTag("Player1"))
                {
                    gameManager.RespawnPlayer1();
                }
                else if (collision.gameObject.CompareTag("Player2"))
                {
                    gameManager.RespawnPlayer2();
                }
            }
            else
            {
                Debug.LogError("MultiplayerGameManager script not found on GameManager object.");
            }
        }
        else
        {
            Debug.LogError("GameManager object not found in the scene.");
        }
    }

    // Destroy the projectile on impact
    Destroy(gameObject);
}

}
