using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;          // Speed of the tank
    public float rotationSpeed = 150f;    // Rotation speed of the tank

    void Update()
    {
        // Tank Movement
        float moveDirection = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDirection);

        // Tank Rotation
        float rotateDirection = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotateDirection);
    }
}
