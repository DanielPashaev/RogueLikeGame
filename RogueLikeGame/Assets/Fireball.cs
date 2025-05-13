using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private Vector2 direction;
    private bool hasExploded = false;

    private Animator animator;

    public void SetDirection(Vector2 dir) {
        direction = dir.normalized;
        Destroy(gameObject, lifetime);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasExploded) {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (!hasExploded && (other.CompareTag("Player") || other.CompareTag("Bandit")))
        hasExploded = true;
       if (rb != null)
{
        rb.linearVelocity = Vector2.zero;
}

        if (animator != null) {
            animator.Play("Explode");
            Destroy(gameObject, .4f);
        }}
        private Rigidbody2D rb;

        private void Awake() {
            rb = GetComponent<Rigidbody2D>();
        }
    }

