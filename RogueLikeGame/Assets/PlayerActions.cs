using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)) {
             animator.SetTrigger("Attack1");
        }
        if (Input.GetMouseButton(1)) {
             animator.SetTrigger("Block");
        }
        
    }
}
