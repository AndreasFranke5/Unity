using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float rotateSpeed = 100f;
    public Transform firePoint;
    public GameObject projectilePrefab;
    private Rigidbody rb;

void Start()
{
    rb = GetComponent<Rigidbody>();
}

void Update()
{
    // Get input
    float moveInput = Input.GetAxis("Vertical") * moveSpeed;
    float rotateInput = Input.GetAxis("Horizontal") * rotateSpeed;

    // Calculate movement and rotation
    Vector3 movement = transform.forward * moveInput * Time.deltaTime;
    Quaternion rotation = Quaternion.Euler(0f, rotateInput * Time.deltaTime, 0f);

    // Move and rotate using Rigidbody
    rb.MovePosition(rb.position + movement);
    rb.MoveRotation(rb.rotation * rotation);

    // Shooting code remains the same
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
