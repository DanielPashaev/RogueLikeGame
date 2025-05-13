using UnityEngine;

public class WizardScript : MonoBehaviour
{
    private Rigidbody2D rb;      
    private Animator animator;   
    public Transform player;
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float attackRange = 8f;
    public float moveSpeed = 2f;
    public float cooldown = 2f;

    private float lastAttack;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();  
    }

    void Update()
    {
        if (!player) return;

    float dist = Vector2.Distance(transform.position, player.position);
    Vector2 dir = (player.position - transform.position).normalized; 
    if (dist > attackRange)
    {
        // Keep moving toward player
        rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);
        animator?.SetBool("IsRunning", true);
    }
    else
    {
        animator?.SetBool("IsRunning", false);

        // Attack if cooldown allows
        if (Time.time - lastAttack > cooldown)
        {
            Attack(dir);     
            lastAttack = Time.time;
        }
    }
    }

     void Attack(Vector2 direction)  
    {
    animator?.SetTrigger("Attack");

    GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
    fireball.GetComponent<Fireball>().SetDirection(direction);
    }
}
