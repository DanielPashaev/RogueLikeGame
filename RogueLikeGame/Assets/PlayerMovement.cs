using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character

    private Rigidbody2D rb;
    private Vector2 movement; // Movement vector
    private Animator animator; // Reference to Animator
    private SpriteRenderer spriteRenderer; // To flip the sprite

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Grab the Animator component
        spriteRenderer = GetComponent<SpriteRenderer>(); // Grab the SpriteRenderer for flipping
    }

    void Update()
    {
        // Capture input from WASD or Arrow Keys
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize movement to ensure diagonal speed isn't faster
        movement = movement.normalized;

        // Set Animator Parameters
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetBool("isRunning", movement.magnitude > 0); // Check if the player is moving

        // Flip the sprite for left/right movement
        if (movement.x < 0)
        {
            spriteRenderer.flipX = true; // Face left
        }
        else if (movement.x > 0)
        {
            spriteRenderer.flipX = false; // Face right
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bandit")) {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero; // Stop the player from being pushed
        }
    }


    void FixedUpdate()
    {
        // Move the character
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
