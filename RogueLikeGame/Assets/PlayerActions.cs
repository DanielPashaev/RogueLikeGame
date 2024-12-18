using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Animator animator;

    private PlayerDistance playerDistance;

    private bool isBlocking = false;
    private bool isAttacking = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerDistance = GetComponent<PlayerDistance>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !isAttacking) {
            isAttacking = true;
            animator.SetTrigger("Attack1");
        }

        
        if (Input.GetMouseButton(1) && !isBlocking) {
            isBlocking = true;
             animator.SetTrigger("Block");
        
        }
    }
    void endOfAttack() {
        isAttacking = false;
    }
    void endOfBlock() {
        isBlocking = false;
    }
}
