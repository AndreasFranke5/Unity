using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultishotTankController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;
    public Transform firePoint;
    public GameObject projectilePrefab;
    public int numberOfShots = 3;
    public float spreadAngle = 45f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Movement code
        float moveInput = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float rotateInput = Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime;

        rb.MovePosition(rb.position + transform.forward * moveInput);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotateInput, 0f));

        // Shooting code
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        float angleStep = spreadAngle / (numberOfShots - 1);
        float angle = -spreadAngle / 2;

        for (int i = 0; i < numberOfShots; i++)
        {
            float currentAngle = angle + (angleStep * i);
            Quaternion rotation = Quaternion.Euler(0f, currentAngle, 0f);
            Quaternion projectileRotation = firePoint.rotation * rotation;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, projectileRotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = projectile.transform.forward * 10f;
            }
        }
    }
}
