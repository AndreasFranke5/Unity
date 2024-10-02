using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float projectileScale = 1.0f; // The scale of the projectile
    private void OnTriggerEnter(Collider other) // When the projectile collides with the animal
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
        projectileScale += 0.1f; // Increase the scale of the projectile upon successful collision with an animal
    }
}
