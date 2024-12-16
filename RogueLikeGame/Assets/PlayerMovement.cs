using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the character
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //reset movement vector
        movement = Vector2.zero;
        
        // Capture input from arrow keys or WASD
        if (Input.GetKey(KeyCode.D)) {
            movement.x = 1;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W)) {
            movement.x = 1;
            movement.y = 1;
        }

         if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S)) {
            movement.x = 1;
            movement.y = -1;
        }

         if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) {
            movement.x = -1;
            movement.y = -1;
        }

         if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) {
            movement.x = -1;
            movement.y = 1;
        }

         if (Input.GetKey(KeyCode.A)) {
            movement.x = -1;
        }

    }

       

    void FixedUpdate()
    {
        // Move the character
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
