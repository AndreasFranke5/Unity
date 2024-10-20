using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpin : MonoBehaviour
{
    public float rotationSpeed = 50f;  // Adjust as needed

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
