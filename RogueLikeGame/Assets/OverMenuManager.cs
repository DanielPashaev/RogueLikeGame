using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class OverMenuManager : MonoBehaviour
{
    public TextMeshProUGUI timeLastedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float elapsedTime = TimeManager.survivalTime; // Access the static variable
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeLastedText.text = $"{minutes:00}:{seconds:00}";
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
