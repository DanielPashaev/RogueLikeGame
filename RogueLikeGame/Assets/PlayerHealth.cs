using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfMaxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private Animator animator;
    private BanditBehavior bandit; // Cached reference to BanditBehavior

    void Start()
    {
        // Find and cache the Bandit reference
        GameObject banditObject = GameObject.Find("Bandit");
        if (banditObject != null)
        {
            bandit = banditObject.GetComponent<BanditBehavior>();
        }
        else
        {
            Debug.LogError("Bandit GameObject not found. Make sure it's named 'Bandit' in the Hierarchy.");
        }

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Prevent health from exceeding max health
        if (health > numOfMaxHealth)
        {
            health = numOfMaxHealth;
        }

        // Update heart UI
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < health ? fullHeart : emptyHeart;
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        animator.SetTrigger("Hurt");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("noBlood", false);
        animator.SetTrigger("Death");

        // Stop the Bandit's movement if it exists
        if (bandit != null)
        {
            bandit.StopMovement();
        }
        else
        {
            Debug.LogWarning("Bandit reference not found when trying to stop movement.");
        }
    }
}
