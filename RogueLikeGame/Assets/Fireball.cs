using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;
    private bool hasExploded = false;

    private Animator animator;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!hasExploded)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
        void OnTriggerEnter2D(Collider2D other)
{
    if (hasExploded) return;

    if (other.CompareTag("Player"))
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage();
        }

        TriggerExplosion(); // Always explode after hitting player
    }
    else if (!other.isTrigger)
    {
        TriggerExplosion(); // Explode on hitting any solid (wall, ground, etc.)
    }
}

void TriggerExplosion()
{
    hasExploded = true;
    speed = 0f; // Stop movement
    GetComponent<Collider2D>().enabled = false;

    if (animator != null)
    {
        animator.Play("Explode", 0); // Play explosion animation
    }

    Destroy(gameObject, 0.5f); // Wait for animation to finish
}
        
            }
        

