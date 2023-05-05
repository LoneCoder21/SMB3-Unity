using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGoomba : MonoBehaviour
{
    public float moveAmt = 2.0f;
    public float jumpAmt = 50.0f;
    public int walkchance = 30;
    public int jumpchance = 0;

    public GameObject goomba;
    
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
        if(Random.Range(0, walkchance) == 0)
        {
            direction *= -1;
        }
        rb.velocity = new Vector2(moveAmt * direction, rb.velocity.y);
        if (Random.Range(0, jumpchance) == 0) {
            rb.velocity = new Vector2(rb.velocity.x, jumpAmt);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }
}
