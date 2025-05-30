using UnityEngine;
using System.Collections;

public class WizardHealth : MonoBehaviour
{
    private int health = 1;

    private bool isDead = false;


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
        if (isDead) return; // Prevent duplicate death calls

        health--;
        StartCoroutine(FlashWhite());

        if (rb != null)
            rb.velocity = Vector2.zero;

        if (health <= 0)
        {
            isDead = true; // Mark as dead
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
        animator.SetBool("IsRunning", false);
        animator.Play("Death", 0, 0f);

        if (rb != null)
            rb.velocity = Vector2.zero;

        // STOP behavior immediately
        WizardBehavior behavior = GetComponent<WizardBehavior>();
        if (behavior != null)
            behavior.enabled = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        yield return new WaitForSeconds(2f); // Let death animation play

        Destroy(gameObject);
    }
}
