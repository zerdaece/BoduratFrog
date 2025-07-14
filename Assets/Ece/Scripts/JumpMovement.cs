using System.Runtime.InteropServices;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class JumpMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private float gyrosensitivity = 5f; // sensitivity for gyroscope input
    public float jumpForce = 8f; // the upward force applied when jumping
    public float DefaultJumpForce = 8f;
    public float fallMultiplier = 2.5f; // the multiplier applied to the falling gravity
    public float lowJumpMultiplier = 2f; // the multiplier applied to the low jumping gravity
    public Camera cam;
    private GameObject ScoreCounter;
    private ScoreCounter scoreCounter;
    private Rigidbody2D rb; // the character's rigidbody
    private bool isGrounded = false; // a flag to check if the character is grounded
    private float maxYValue; // maximum Y value for collision
    public int Combocounter = 0; // combo counter for the player
    [SerializeField] private Text ComboCounterText; // UI text to display the combo counter
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

        /*if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Moved)
            {
                rb.velocity = new Vector2(touch.deltaPosition.x /4f, rb.velocity.y);
            }
        }*/
        if (SystemInfo.supportsGyroscope)
        {
            print("Gyroscope is supported");
            Input.gyro.enabled = true;
            // Map gyroscope attitude to horizontal movement
            float gyroInput = Input.gyro.attitude.y;
            rb.linearVelocity = new Vector2(gyroInput * 10f * gyrosensitivity, rb.linearVelocity.y);
        }
        else
        {
            print("Gyroscope is not supported");
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Gyroscope movement


            if (touch.phase == TouchPhase.Moved)
            {
                // Dokunma girişini ekran genişliğine göre normalize ediyoruz
                float normalizedDeltaX = touch.deltaPosition.x / Screen.width;

                // Bu değeri kullanarak karakterin hızını ayarlıyoruz
                rb.linearVelocity = new Vector2(normalizedDeltaX * 150f, rb.linearVelocity.y);
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
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.linearVelocity.y > 0 && rb.linearVelocity.y < 4f)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // set the grounded flag to true when colliding with a platform from the top
        if (collision.contacts[0].normal.y > 0.7f && rb.linearVelocityY <= 0)
        {
            rb.linearVelocityY = 0; // Reset vertical velocity to prevent bouncing
            isGrounded = true;
            if (collision.gameObject.GetComponent<platform>() != null)
            {
                platform collidedPlatform = collision.gameObject.GetComponent<platform>();
                if (!collidedPlatform.stepped)
                {
                    collidedPlatform.stepped = true;
                    Combocounter++;
                    print("Combo Counter: " + Combocounter);
                    if (Combocounter > 0 )
                    {
                        ComboCounterText.text = "" + Combocounter; // Update the UI text with the current combo counter
                        jumpForce = 8 * (1 + Mathf.Log10(Combocounter)); // Increase jump force based on combo counter
                        print("Jumpforce: " + jumpForce);
                    }
                }
                else
                {
                    jumpForce = DefaultJumpForce; // Reset jump force if the player has already stepped on this platform
                    Combocounter = 0;
                }
            }
        }
    }
}
