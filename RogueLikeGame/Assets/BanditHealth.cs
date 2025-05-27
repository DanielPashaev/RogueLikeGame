using UnityEngine;
using System.Collections;


public class BanditHealth : MonoBehaviour
{
    private int health = 2;

    private Animator animator;

    private BanditBehavior bandit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        
    }

    public void HurtBandit() {
        health--;
        animator.SetTrigger("Hurt");
        if (health <= 0) {
            StartCoroutine(Die());
        }
    }
    IEnumerator Die() {
        animator.SetTrigger("Death");
        if (bandit != null)
    {
        bandit.enabled = false; // stop AI logic
    }
        yield return new WaitForSeconds(.5f);
        GameObject current = gameObject;
    Destroy(current);
    }
}

