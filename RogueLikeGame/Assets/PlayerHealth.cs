using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int numOfMaxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public static bool isPlayerDead = false;
    private Vector2 movement;
    private Animator animator;
    private BanditBehavior bandit;
    private PlayerMovement playerMovement;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
        if (isPlayerDead) {
            return;
        }
        health -= 1;
        animator.SetTrigger("Hurt");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isPlayerDead = true;
        animator.SetBool("noBlood", false);
        animator.SetTrigger("Death");
        playerMovement.enabled = false;
        Debug.Log("Player has died!");
        SceneManager.LoadScene("GameOverScene"); 

    }
}
