using UnityEngine;
using System.Collections;

public class BanditBehavior : MonoBehaviour
{

    public float moveSpeed = 8f;

    public float attackRange = 1f; // Distance to stop and attack

    public Transform player; // Reference to the player's Transform

    private Animator animator;

    private Rigidbody2D rb;

    private Vector2 movement;

    private bool isAttacking = false; // Flag to track if attacking

    private bool isCoolingDown = false; // Flag to track cooldown after attack

    private SpriteRenderer spriteRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.isPlayerDead) {
            StopMovement();
            return;
        }
        Vector2 direction = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (movement.x < 0) {
            spriteRenderer.flipX = false;
        } else if (movement.x > 0) {
            spriteRenderer.flipX = true;
        }

        if (distanceToPlayer <= attackRange && !isCoolingDown && !isAttacking) { // in range to attack and off cooldown
            StartCoroutine(AttackPlayer());   // attack player
        } else if (!isAttacking && !isCoolingDown) {
            movement = direction;
            animator.SetBool("IsRunning", true);
        } else {
            movement = Vector2.zero;
             animator.SetBool("IsRunning", false);
        }

        }
        void OnCollisionEnter2D(Collision2D collision2D) {
            if (collision2D.gameObject.CompareTag("Player")) {
                PlayerHealth playerHealth = collision2D.gameObject.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage();
                Debug.Log("Player collided, take damage!");
            }
        }
        IEnumerator AttackPlayer() {

            isAttacking = true;
            animator.SetBool("IsRunning", false);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(0.5f); // time for animation of attack
            isAttacking = false;
            isCoolingDown = true;
            yield return new WaitForSeconds(1f); // time for cooldown after 
            isCoolingDown = false;
        }
        void CheckAttackHit() {
            // Check if the player is still within range
            float distanceToPlayer = Vector2.Distance(player.position, transform.position);
            if (distanceToPlayer <= attackRange) {
        // Damage the player
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
            {
            playerHealth.TakeDamage();
            Debug.Log("Player hit by Bandit!");
            }
     }
        }
        void FixedUpdate() {
        // Apply movement to the Rigidbody2D
        if (!isAttacking && !isCoolingDown)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        }
        void StopMovement() {
            movement = Vector2.zero;
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("IsRunning", false);
        }
    }

