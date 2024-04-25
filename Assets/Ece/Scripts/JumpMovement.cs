using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class JumpMovement : MonoBehaviour
{
    private Animator animator;
    public float jumpForce = 3f; // the upward force applied when jumping
    public float fallMultiplier = 2.5f; // the multiplier applied to the falling gravity
    public float lowJumpMultiplier = 2f; // the multiplier applied to the low jumping gravity
    public Camera cam;
    private GameObject ScoreCounter;
    private ScoreCounter scoreCounter;
    private Rigidbody2D rb; // the character's rigidbody
    private bool isGrounded = false; // a flag to check if the character is grounded
    private float maxYValue; // maximum Y value for collision
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        ScoreCounter = GameObject.Find("ScoreCounter");
        // get the character's rigidbody component
        rb = GetComponent<Rigidbody2D>();
        maxYValue = transform.position.y; // initialize maxYValue with player's initial Y position
        // Time.timeScale = 0f;
        // cam.transform.position = new Vector3(0, 0, -10);
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
                rb.velocity = new Vector2(touch.deltaPosition.x /5.5f, rb.velocity.y);
            }
        }

        // Update maxYValue if player's Y position exceeds the previous maximum value
        if (transform.position.y > maxYValue)
        {
            maxYValue = transform.position.y;
        }

        // apply automatic jumping and falling only when grounded
        if (isGrounded)
        {
            // apply upward force to jump
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetTrigger("Jump");
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
        if (collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            
        }
    }
}
