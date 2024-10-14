using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{

    private Vector3 startPos;
    private float offset;
    public float speed = 5f;

    void Start()
    {
        startPos = transform.position;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        offset = spriteRenderer.sprite.bounds.size.x / 2;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        if (transform.position.x < startPos.x - offset)
        {
            transform.position = startPos;
        }
    }
}