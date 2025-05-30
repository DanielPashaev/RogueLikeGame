using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Animator animator;

    public static bool isBlocking = false;
    private bool isAttacking = false;

    private float attackRange = 1f;
    private float blockRange = 1f;

    private GameManager gameManager;
    private PlayerHealth playerHealth;

    public int numOfBlocks = 0;

    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    void Update()
{
    if (gameManager != null && gameManager.IsPaused())
        return;

    // Attack input
    if (Input.GetMouseButtonDown(0) && !isAttacking && !isBlocking)
    {
        isAttacking = true;
        animator.SetTrigger("Attack1");
        Debug.Log("Attack triggered");
    }

    // Block input
    if (Input.GetMouseButtonDown(1) && !isBlocking && !isAttacking)
    {
        isBlocking = true;
        animator.SetTrigger("Block");
        Debug.Log("Block triggered");
    }
}

public void ParryProjectile()
{
    numOfBlocks++;
    Debug.Log("Projectile parried! Block count: " + numOfBlocks);

    if (numOfBlocks >= 5)
    {
        playerHealth.GiveHealth();
        numOfBlocks = 0;
    }
}

    public void ResetState()
{
    isAttacking = false;
    isBlocking = false;
    Debug.Log("Player state reset after damage");
}
    public void CheckAndHitIfInRange()
    {
        foreach (BanditBehavior bandit in FindObjectsOfType<BanditBehavior>())
        {
            if (Vector2.Distance(transform.position, bandit.transform.position) <= attackRange)
            {
                BanditHealth banditHealth = bandit.GetComponent<BanditHealth>();
                if (banditHealth != null)
                    banditHealth.HurtBandit();
            }
        }

        foreach (WizardBehavior wizard in FindObjectsOfType<WizardBehavior>())
        {
            if (Vector2.Distance(transform.position, wizard.transform.position) <= attackRange)
            {
                WizardHealth wizardHealth = wizard.GetComponent<WizardHealth>();
                if (wizardHealth != null)
                    wizardHealth.HurtWizard();
            }
        }
    }

    public void CheckAndBlockIfInRange()
    {
        foreach (BanditBehavior bandit in FindObjectsOfType<BanditBehavior>())
        {
            if (Vector2.Distance(transform.position, bandit.transform.position) <= blockRange)
                numOfBlocks++;
        }

        foreach (WizardBehavior wizard in FindObjectsOfType<WizardBehavior>())
        {
            if (Vector2.Distance(transform.position, wizard.transform.position) <= blockRange)
                numOfBlocks++;
        }

        if (numOfBlocks >= 5)
        {
            playerHealth.GiveHealth();
            numOfBlocks = 0;
        }
    }

    void EndOfAttack()
    {
        isAttacking = false;
    }

    void EndOfBlock()
    {
        isBlocking = false;
    }
}
