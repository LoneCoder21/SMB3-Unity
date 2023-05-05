using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float walkSpeed = 100.0f;
    public float jumpSpeed = 7f;
    public float killbounce = 7f;
    public LayerMask mask;
    public GameObject maincamera;
    public float cameraydist = 30.0f;
    public int up1chance = 0;

    public GameObject up1;
    public GameObject goomba;
    public GameObject shell;
    public TMP_Text coins;
    public TMP_Text lives;
    public TMP_Text time;
    public TMP_Text clearcourse;

    private Vector3 initposition;

    Rigidbody2D rb;
    BoxCollider2D coll;
    SpriteRenderer renderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        lives.text = "Mx   " + Scores.lives;
        initposition = transform.position;
        coins.text = "$ " + Scores.coins;
    }

    void Update()
    {
        time.text = "" + (int) Time.realtimeSinceStartup;
        float distance = walkSpeed * Time.deltaTime;
        float hAxis = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(hAxis * distance, 0.0f);
        Vector2 currPosition = transform.position;
        Vector2 center = currPosition;
        
        Vector2 newPosition = currPosition + movement;
        rb.velocity = new Vector2(hAxis * walkSpeed, rb.velocity.y);
        Vector2 newposwithsize = transform.position + new Vector3(0.0f, -renderer.bounds.size.y / 2, 0.0f);
        bool grounded = Physics2D.Raycast(newposwithsize, -Vector2.up, 0.0f, mask);
        
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
        
        if (hAxis > 0)
        {
            renderer.flipX = false;
        }
        else if(hAxis < 0)
        {
            renderer.flipX = true;
        }
        
        Vector3 pos = new Vector3(transform.position.x, cameraydist, maincamera.transform.position.z);
        maincamera.transform.position = pos;
    }

    public void KillMyself()
    {
        Vector3 respawn = initposition;
        transform.position = respawn;
        Scores.lives--;
        if (Scores.lives == 0)
        {
            SceneManager.LoadScene("Level1");
            Scores.coins = 0;
            Scores.lives = 3;
        }
        lives.text = "Mx   " + Scores.lives;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Coin") {
            Scores.coins++;
            coins.text = "$ "+Scores.coins;
            Destroy(other.gameObject);
        }
        if (other.tag == "Enemy" || other.tag == "Respawn")
        {
            KillMyself();
        }
        if (other.tag == "Kill")
        {
            Destroy(other.transform.parent.gameObject);
            rb.velocity = new Vector2(rb.velocity.x, killbounce);
        }
        if (other.tag == "FlyKill")
        {
            rb.velocity = new Vector2(rb.velocity.x, killbounce);
            Vector3 goombapos = other.transform.parent.gameObject.transform.position;
            Destroy(other.transform.parent.gameObject);
            goombapos.y -= 3.0f;
            Instantiate(goomba, goombapos, Quaternion.identity);
        }
        if (other.tag == "KoopaKill")
        {
            print("Killing...");
            rb.velocity = new Vector2(rb.velocity.x, killbounce);
            Vector3 koopapos = other.transform.parent.gameObject.transform.position;
            Destroy(other.transform.parent.gameObject);
            koopapos.y -= 3.0f;
            Instantiate(shell, koopapos, Quaternion.identity);
        }
        if (other.tag == "End")
        {
            Destroy(other.gameObject);
            clearcourse.enabled = true;
        }
        if (other.tag == "PowerBlock")
        {
            int num = Random.Range(0, up1chance);
            if(num==0) {
                Vector3 newpos = other.transform.position;
                newpos.y += 5.0f;
                Instantiate(up1, newpos, Quaternion.identity);
            }
            else
            {
                Scores.coins++;
                coins.text = "$ " + Scores.coins;
            }
            Destroy(other.gameObject);
        }
        if (other.tag == "1UP")
        {
            Scores.lives++;
            lives.text = "Mx   " + Scores.lives;
            Destroy(other.gameObject);
        }
    }
}