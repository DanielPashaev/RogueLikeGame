using UnityEngine;
using System.Collections;

public class WizardHealth : MonoBehaviour
{
    private int health = 1;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;


    void Awake()
{
    spriteRenderer = GetComponent<SpriteRenderer>();
}
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void HurtWizard()
    {

        health--;
        StartCoroutine(FlashWhite());


        // Optional: freeze movement on hit
        if (rb != null)
            rb.velocity = Vector2.zero;

        // Optionally flip or flash here

        if (health <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator HurtEffect()
{
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    Color originalColor = sr.color;
    Vector3 originalPosition = transform.position;

    sr.color = Color.red;

    // Shake position
    float shakeTime = 0.2f;
    float elapsed = 0f;
    while (elapsed < shakeTime)
    {
        transform.position = originalPosition + (Vector3)Random.insideUnitCircle * 0.05f;
        elapsed += Time.deltaTime;
        yield return null;
    }

    transform.position = originalPosition;
    sr.color = originalColor;
}
IEnumerator FlashWhite()
{
     Color originalColor = spriteRenderer.color;
    spriteRenderer.color = Color.white;
    yield return new WaitForSeconds(0.1f);
    spriteRenderer.color = originalColor;
}

    IEnumerator Die()
    {
        // Disable other animator parameters if needed
    animator.SetBool("IsRunning", false);

    // Force death animation
    animator.Play("Death", 0, 0f);  // Layer 0, time = 0

    // Optional: freeze movement
    if (TryGetComponent<Rigidbody2D>(out var rb))
        rb.velocity = Vector2.zero;

    yield return new WaitForSeconds(2f); // Wait for animation

    Destroy(gameObject);
    }
}
