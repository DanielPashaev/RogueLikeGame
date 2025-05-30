using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 direction;
    private bool hasExploded = false;
    private bool isReflected = false;

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
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (hasExploded) return;

    // Case: Reflected fireball hits wizard
    if (isReflected && other.CompareTag("Wizard"))
    {
        Debug.Log("Reflected fireball hit wizard!");

        WizardHealth wizardHealth = other.GetComponent<WizardHealth>();
        if (wizardHealth != null)
        {
            Debug.Log("WizardHealth found. Damaging wizard.");
            wizardHealth.HurtWizard();
        }

        TriggerExplosion();
        return;
    }

    // Prevent reflected fireball from hitting player
    if (isReflected && other.CompareTag("Player"))
    {
        return;
    }

    // Case: Original fireball hits player
    if (!isReflected && other.CompareTag("Player"))
    {
        PlayerActions playerActions = other.GetComponent<PlayerActions>();
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

        if (playerActions != null && PlayerActions.isBlocking)
        {
            Debug.Log("Parried fireball! Reflecting.");
            Reflect();
            playerActions.ParryProjectile();
            return;
        }
        else if (playerHealth != null)
        {
            playerHealth.TakeDamage();
            TriggerExplosion();
            return;
        }
    }

    // Hit any non-trigger (wall, ground, etc.)
    if (!other.isTrigger)
    {
        TriggerExplosion();
    }
}

    void Reflect()
    {
        isReflected = true;

        GameObject wizard = FindClosestWizard();
        if (wizard != null)
        {
            direction = ((Vector2)wizard.transform.position - (Vector2)transform.position).normalized;

            // Face the new direction (optional)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            direction = -direction; // fallback
        }

        GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    GameObject FindClosestWizard()
    {
        WizardBehavior[] wizards = GameObject.FindObjectsOfType<WizardBehavior>();
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (var wizard in wizards)
        {
            float dist = Vector2.Distance(transform.position, wizard.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = wizard.gameObject;
            }
        }

        return closest;
    }

    void TriggerExplosion()
    {
        hasExploded = true;
        speed = 0f;
        GetComponent<Collider2D>().enabled = false;

        if (animator != null)
        {
            animator.Play("Explode", 0);
        }

        Destroy(gameObject, 0.5f);
    }
}
