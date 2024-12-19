using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class OverMenuManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timeLastedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverText.text = "Game Over";
        float survivalTime = PlayerPrefs.GetFloat("SurvivalTime", 0);
        int minutes = Mathf.FloorToInt(survivalTime / 60);
        int seconds = Mathf.FloorToInt(survivalTime % 60);
        timeLastedText.text = $"Time Lasted: {minutes:00}:{seconds:00}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TryAgain() {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame() {
        Application.Quit();
    }
}
