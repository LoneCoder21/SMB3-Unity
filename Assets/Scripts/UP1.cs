using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP1 : MonoBehaviour
{
    public float moveAmt = 2.0f;
    float direction = 1.0f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(moveAmt * direction, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        direction *= -1;
    }
}
