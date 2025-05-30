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

    private PlayerMovement playerMovement;
    private PlayerActions playerActions;
    private Animator animator;
    private BanditBehavior bandit;

    void Start()
    {
        isPlayerDead = false;

        playerMovement = GetComponent<PlayerMovement>();
        playerActions = GetComponent<PlayerActions>(); // NEW: Get reference to PlayerActions
        animator = GetComponent<Animator>();

        GameObject banditObject = GameObject.Find("Bandit");
        if (banditObject != null)
        {
            bandit = banditObject.GetComponent<BanditBehavior>();
        }
        else
        {
            Debug.LogError("Bandit GameObject not found. Make sure it's named 'Bandit' in the Hierarchy.");
        }
    }

    void Update()
    {
        if (health > numOfMaxHealth)
        {
            health = numOfMaxHealth;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] != null)
            {
                hearts[i].sprite = i < health ? fullHeart : emptyHeart;
            }
        }
    }

    public void TakeDamage()
    {
        if (isPlayerDead) return;

        health -= 1;

        // NEW: Reset attack and block state in case hurt interrupts them
        if (playerActions != null)
        {
            playerActions.ResetState();
        }

        animator.SetTrigger("Hurt");

        if (health <= 0)
        {
            Die();
        }
    }

    public void GiveHealth()
    {
        if (health < numOfMaxHealth)
        {
            health++;
        }
    }

    void Die()
    {
        isPlayerDead = true;
        animator.SetBool("noBlood", false);
        animator.SetTrigger("Death");

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        Debug.Log("Player has died!");
        SceneManager.LoadScene("GameOverScene");
    }
}
