using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float moveAmt = 2.0f;
    float direction = 0.0f;

    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        int num = Random.Range(0, 2);
        float[] arr = { -1.0f, 1.0f };
        direction = arr[num];
    }

    void Update()
    {
        rb.velocity = new Vector2(moveAmt * direction, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Kill" || other.tag == "FlyKill" || other.tag == "Enemy")
        {
            Destroy(other.gameObject);
            return;
        }
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().KillMyself();
            return;
        }
        direction *= -1;
    }
}