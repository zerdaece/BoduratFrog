using System.Runtime.InteropServices;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class JumpMovement : MonoBehaviour
{
    public JumpMovement instance;
    private Animator animator;
    public float jumpForce = 8f; // the upward force applied when jumping
    public float DefaultJumpForce = 8f;
    public float fallMultiplier = 2.5f; // the multiplier applied to the falling gravity
    public float lowJumpMultiplier = 2f; // the multiplier applied to the low jumping gravity

    private bool hascombo = false;
    private Rigidbody2D rb; // the character's rigidbody
    private bool isGrounded = false; // a flag to check if the character is grounded
    private float maxYValue; // maximum Y value for collision
    public int Combocounter = 0; // combo counter for the player
    [SerializeField] private Text ComboCounterText; // UI text to display the combo counter

    // New combo system variables
    private bool justJumped = false; // Did player just jump
    private bool isDescending = false; // Is player descending
    [SerializeField] int sensitivity = 5; // sensitivity for touch input
    [SerializeField] GameObject platformsContainer; // Reference to Platforms container

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        // get the character's rigidbody component
        rb = GetComponent<Rigidbody2D>();
        maxYValue = transform.position.y; // initialize maxYValue with player's initial Y position

        // Find the Platforms container
        if (platformsContainer == null)
        {
            platformsContainer = GameObject.Find("Platforms");
            if (platformsContainer == null)
            {
                Debug.LogError("Platforms container not found!");
            }
        }
    }

    void FixedUpdate()
    {
        // Check if game has started
        if (!GameManager.isGameStarted)
        {
            // Oyun başlamadıysa, kurbağayı idle durumda tutuyoruz
            rb.linearVelocity = Vector2.zero;
            return;
        }

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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Gyroscope movement


            if (touch.phase == TouchPhase.Moved)
            {
                // Dokunma girişini ekran genişliğine göre normalize ediyoruz
                float normalizedDeltaX = touch.deltaPosition.x / Screen.width;

                // Bu değeri kullanarak karakterin hızını ayarlıyoruz
                rb.linearVelocity = new Vector2(normalizedDeltaX * sensitivity * 30f, rb.linearVelocity.y);
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
            justJumped = true;
            isDescending = false;
            animator.SetTrigger("Jump");
        }
        else
        {
            // Track jump peak and descending state
            if (justJumped && rb.linearVelocity.y <= 0 && !isDescending)
            {
                // Player just started descending
                maxYValue = transform.position.y;
                isDescending = true;
            }

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
        // Check every contact point, not just the first - a single sideways/corner
        // clip can otherwise report contacts[0] as a near-vertical normal and be
        // mistaken for a top landing.
        float bestNormalY = float.NegativeInfinity;
        for (int i = 0; i < collision.contactCount; i++)
        {
            float normalY = collision.GetContact(i).normal.y;
            if (normalY > bestNormalY)
            {
                bestNormalY = normalY;
            }
        }

        // set the grounded flag to true when colliding with a platform from the top
        if (bestNormalY > 0.7f && rb.linearVelocity.y <= 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Reset vertical velocity to prevent bouncing
            isGrounded = true;

            if (collision.gameObject.GetComponent<platform>() != null)
            {
                platform collidedPlatform = collision.gameObject.GetComponent<platform>();
                // New combo system: only count if player was descending from a jump and landed on closest platform below
                if (justJumped && isDescending && !collidedPlatform.stepped)
                {
                    GameObject closestPlatform = FindClosestPlatformBelow(maxYValue);

                    if (closestPlatform != null && closestPlatform == collision.gameObject)
                    {
                        // This is the closest platform below the jump peak
                        collidedPlatform.stepped = true;
                        Combocounter++;
                        print("Combo Counter: " + Combocounter);

                        if (Combocounter > 0)
                        {
                            hascombo = true;
                            ComboCounterText.text = "" + Combocounter; // Update the UI text with the current combo counter
                            // Prevent log(0) which would give -Infinity
                            float comboMultiplier = Mathf.Max(1f, 1 + Mathf.Log(Mathf.Sqrt(Mathf.Max(1, Combocounter))));
                            jumpForce = 8 * comboMultiplier; // Increase jump force based on combo counter
                            print("Jumpforce: " + jumpForce);
                        }
                        PlayerSpriteHandler.Instance?.ChangeRandomPose();
                    }
                    else
                    {
                        // Player didn't land on the closest platform - reset combo
                        jumpForce = DefaultJumpForce;
                        Combocounter = 0;
                        ComboCounterText.text = "0";
                        if (hascombo)
                        {
                            PlayerSpriteHandler.Instance?.SetPoseSprites("SadPose");
                            hascombo = false;
                        }
                        else
                        {
                            PlayerSpriteHandler.Instance?.ChangeRandomPose();
                        }
                    }
                }
                else
                {
                    PlayerSpriteHandler.Instance?.ChangeRandomPose();
                }

                // Reset jump tracking variables
                justJumped = false;
                isDescending = false;
            }
        }
    }

    GameObject FindClosestPlatformBelow(float maxYValue)
    {
        if (platformsContainer == null) return null;

        GameObject closestPlatform = null;
        float closestDistance = float.MaxValue;
        // Check all child platforms in the Platforms container
        foreach (Transform child in platformsContainer.transform)
        {
            if (child.gameObject.activeInHierarchy && child.gameObject.GetComponent<platform>() != null)
            {
                platform platformScript = child.gameObject.GetComponent<platform>();
                Vector2 platformPosition = child.position;

                // Only consider platforms that are below the jump peak and not already stepped
                if (platformPosition.y < maxYValue && !platformScript.stepped)
                {
                    // Calculate vertical distance (more important than horizontal)
                    float distance = maxYValue - platformPosition.y;
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPlatform = child.gameObject;
                    }
                }
            }
        }

        return closestPlatform;
    }
}
