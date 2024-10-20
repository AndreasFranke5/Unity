using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform firePoint;
    public GameObject projectilePrefab;

    void Update()
    {
        // Movement with WASD
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(moveX, 0, moveY) * moveSpeed * Time.deltaTime);

        // Shooting with Left Ctrl
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
