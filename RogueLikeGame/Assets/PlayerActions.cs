using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Animator animator;

    private bool isBlocking = false;
    private bool isAttacking = false;

    private float attackRange = 1f;
    private BanditBehavior bandit;
    public GameManager gameManager;
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject banditObject = GameObject.Find("Bandit");
        if (banditObject != null)
        {
            bandit = banditObject.GetComponent<BanditBehavior>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager != null && gameManager.IsPaused()) {
            return;
        }
        if (Input.GetMouseButton(0) && !isAttacking) {
            isAttacking = true;
            animator.SetTrigger("Attack1");
        }

        
        if (Input.GetMouseButton(1) && !isBlocking) {
            isBlocking = true;
             animator.SetTrigger("Block");
        
        }
    }
    public void CheckAndHitIfInRange() {
        if (bandit == null) {
        // The bandit no longer exists; just return early or handle this case
        return;
        }
            float distance = Vector2.Distance(transform.position, bandit.transform.position);
            if (distance <= attackRange) {
                BanditHealth banditHealth = bandit.GetComponent<BanditHealth>();
                if (banditHealth != null)
                {
                    banditHealth.HurtBandit();
            }

        }
    }
    void endOfAttack() {
        isAttacking = false;
    }
    void endOfBlock() {
        isBlocking = false;
    }
}
