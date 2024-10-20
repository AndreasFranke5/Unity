using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform firePoint;
    public GameObject projectilePrefab;

    void Update()
    {
        // Movement with arrow keys
        float moveX = Input.GetAxis("Horizontal2");
        float moveY = Input.GetAxis("Vertical2");

        transform.Translate(new Vector3(moveX, 0, moveY) * moveSpeed * Time.deltaTime);

        // Shooting with Num 0
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
