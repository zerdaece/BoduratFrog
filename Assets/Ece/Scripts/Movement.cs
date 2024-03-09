using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 7f;

    private bool jump = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveControl();
        jumpControl();
    }

    void MoveControl()
    {
        float move = Input.GetAxis("Horizontal");
        Vector2 moveVector = new Vector2(move, rb.velocity.y);
        rb.velocity = new Vector2(moveVector.x * moveSpeed, moveVector.y);
    }

    void jumpControl()
    {
        if (Input.GetButtonDown("Jump") && !jump)
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Zemin"))
        {
            jump = false;
        }
    }
}

