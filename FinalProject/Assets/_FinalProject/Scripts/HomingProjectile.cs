using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float speed = 8f;
    public float rotateSpeed = 200f;
    private Transform target;
    
    void Start()
    {
        // Find the nearest enemy (or player in multiplayer)
        FindTarget();
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Vector3 rotateAmount = Vector3.Cross(transform.forward, direction);
            transform.Rotate(rotateAmount * rotateSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
    }

    private void FindTarget()
    {
        // In Singleplayer/Coop, find the closest enemy
        // In Multiplayer, find the other player
        // (This can be updated to reference the correct game mode logic)
        // Example for simplicity:
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            target = enemies[0].transform;  // Pick the first enemy for now (you can refine this later)
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Destroy the projectile on impact
        Destroy(gameObject);
    }
}
