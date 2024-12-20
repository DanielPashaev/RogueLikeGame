using UnityEngine;
using System.Collections;

public class BanditBehavior : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float attackRange = 1f;

    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isAttacking = false;
    private bool isCoolingDown = false;
    private SpriteRenderer spriteRenderer;
    private Vector2 initialPosition;

    

    void Awake()
    {
        // Get references here, so they're ready before Respawn is called
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Capture the initial position now, before the bandit moves
        initialPosition = transform.position;
    }

    void Start()
    {
        // Player reference: safe to do this in Start
        player = GameObject.FindWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    void Update()
    {
        if (PlayerHealth.isPlayerDead)
        {
            StopMovement();
            return;
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
            if (player == null)
            {
                StopMovement();
                return;
            }
        }

        Vector2 direction = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        // Update sprite direction based on movement.x
        if (movement.x < 0) spriteRenderer.flipX = false;
        else if (movement.x > 0) spriteRenderer.flipX = true;

        if (distanceToPlayer <= attackRange && !isCoolingDown && !isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
        else if (!isAttacking && !isCoolingDown)
        {
            movement = direction;
            animator.SetBool("IsRunning", true);
        }
        else
        {
            movement = Vector2.zero;
            animator.SetBool("IsRunning", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision2D.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
                Debug.Log("Player collided, take damage!");
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        animator.SetBool("IsRunning", false);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        isCoolingDown = true;
        yield return new WaitForSeconds(1f);
        isCoolingDown = false;
    }

    void CheckAttackHit()
    {
        if(PlayerActions.isBlocking) {
            return;
        }
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
                Debug.Log("Player hit by Bandit!");
            }
        }
    }

    void FixedUpdate()
    {
        if (!isAttacking && !isCoolingDown && movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void StopMovement()
    {
        movement = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("IsRunning", false);
    }

    public void Respawn()
    {
        // Re-acquire player if needed
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
        }

        // Move back to initial position
        transform.position = initialPosition;

        isAttacking = false;
        isCoolingDown = false;
        movement = Vector2.zero;

        // Let Update decide animation state based on distance to player
        animator.SetBool("IsRunning", false);

        Debug.Log("Bandit respawned and reinitialized!");
    }
}
