using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class JumpMovement : MonoBehaviour
{
    public float jumpForce = 5f; // the upward force applied when jumping
    public float fallMultiplier = 2.5f; // the multiplier applied to the falling gravity
    public float lowJumpMultiplier = 2f; // the multiplier applied to the low jumping gravity

    private Rigidbody2D rb; // the character's rigidbody
    private bool isGrounded = false; // a flag to check if the character is grounded
   public Text ScoreText;

    void Start()
    {
        // get the character's rigidbody component
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // apply horizontal movement
        /*float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * 5f, rb.velocity.y);*/
        
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                rb.velocity = new Vector2(touch.deltaPosition.x /2.5f, rb.velocity.y);
            }
        }
        // apply automatic jumping and falling only when grounded
        if (isGrounded)
        {
            // apply upward force to jump
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            ScoreText.text = ScoreCounter.Score.ToString();
        }
        else
        {
            // apply falling gravity
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.velocity.y > 0 && rb.velocity.y < 5f)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // set the grounded flag to true when colliding with a platform from the top
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
