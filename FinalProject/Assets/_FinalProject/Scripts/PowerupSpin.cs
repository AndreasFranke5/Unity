using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpin : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
