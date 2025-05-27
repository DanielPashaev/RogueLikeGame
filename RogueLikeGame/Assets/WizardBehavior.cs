using UnityEngine;
using System.Collections;

public class WizardBehavior : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 8f;
    public float attackCooldown = 2f;

    public GameObject fireballPrefab;
    public Transform firePoint;

    private Transform player;
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private Vector2 movement;
    private bool canAttack = true;
    private bool isAttacking = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        // Flip sprite
        spriteRenderer.flipX = direction.x < 0;

        if (distanceToPlayer <= attackRange)
        {
            movement = Vector2.zero;
            animator.SetBool("IsRunning", false);

            if (canAttack && !isAttacking)
            {
                StartCoroutine(AttackPlayer(direction));
            }
        }
        else
        {
            if (!isAttacking)
            {
                movement = direction;
                animator.SetBool("IsRunning", true);
            }
        }
    }

    IEnumerator AttackPlayer(Vector2 direction)
    {
        canAttack = false;
        isAttacking = true;

        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f); // small delay for animation timing

        if (fireballPrefab != null && firePoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
            fireball.GetComponent<Fireball>().SetDirection(direction);

            if (direction.x < 0)
                fireball.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        canAttack = true;
    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero && !isAttacking)
        {
            Vector2 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPos);
        }
    }
}
