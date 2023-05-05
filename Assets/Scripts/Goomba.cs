using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
    public float moveAmt = 2.0f;
    public int chance = 30;
    float direction;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int num = Random.Range(0, 2);
        float[] arr = { -1.0f,1.0f };
        direction = arr[num];
    }

    void Update()
    {
        if(Random.Range(0, chance) == 0)
        {
            direction *= -1;
        }
        rb.velocity = new Vector2(moveAmt * direction, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
    }
}
