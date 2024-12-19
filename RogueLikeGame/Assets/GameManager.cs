using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuCanvas; // Assign the Pause Menu Canvas in the 
    private bool isPaused = false;
    public PauseMenuBlur blur;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check for Escape key press
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        blur = GetComponent<PauseMenuBlur>();
        blur.EnableBlur(true);
        pauseMenuCanvas.SetActive(true); // Show the Pause Menu
        Time.timeScale = 0f;            // Freeze game time
        isPaused = true;
    }

    public void ResumeGame()
    {
        blur = GetComponent<PauseMenuBlur>();
        blur.EnableBlur(false);
        pauseMenuCanvas.SetActive(false); // Hide the Pause Menu
        Time.timeScale = 1f;             // Resume game time
        isPaused = false;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;             // Reset time scale before switching scenes
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene"); // Load Main Menu
    }

    public void QuitGame()
    {
        Application.Quit();              // Quit the application
    }
    public bool IsPaused() {
        return isPaused;
    }
}
