using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    public float moveAmt = 2.0f;
    public int walkchance = 30;
    public GameObject shell;

    SpriteRenderer renderer;
    float direction;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        int num = Random.Range(0, 2);
        float[] arr = { -1.0f, 1.0f };
        direction = arr[num];
    }

    void Update()
    {
        if (Random.Range(0, walkchance) == 0)
        {
            direction *= -1;
        }
        if (direction > 0)
        {
            renderer.flipX = true;
        }
        else if (direction < 0)
        {
            renderer.flipX = false;
        }
        rb.velocity = new Vector2(moveAmt * direction, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    }
}
