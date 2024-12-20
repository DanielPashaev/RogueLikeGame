using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OverMenuManager : MonoBehaviour
{
    public TextMeshProUGUI timeLastedText;
    public GameObject bandit;

    void Start()
    {
        float elapsedTime = TimeManager.survivalTime; 
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeLastedText.text = $"{minutes:00}:{seconds:00}";
    }

    public void TryAgain()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnGameSceneLoaded;

        // Load the GameScene
        TimeManager.survivalTime = 0f;
        SceneManager.LoadScene("GameScene");
    }

    private void OnGameSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            // Reset player death state
            PlayerHealth.isPlayerDead = false;

            // Find the Bandit GameObject
            bandit = GameObject.FindWithTag("Bandit");

            if (bandit != null)
            {
                bandit.GetComponent<BanditBehavior>().Respawn();
                Debug.Log("Bandit respawned successfully!");
            }
            else
            {
                Debug.LogError("Bandit not found in the GameScene!");
            }

            // Unsubscribe from the event to avoid multiple calls
            SceneManager.sceneLoaded -= OnGameSceneLoaded;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
