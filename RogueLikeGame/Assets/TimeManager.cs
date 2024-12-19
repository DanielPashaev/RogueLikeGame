using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI timerText;
    private float survivalTime;
    private bool isGameActive = true;

    void Update() {
        if (isGameActive) {
            survivalTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }
    void UpdateTimerUI() {
        int minutes = Mathf.FloorToInt(survivalTime / 60);
        int seconds = Mathf.FloorToInt(survivalTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
    public void StopTimer() {
        isGameActive = false;
    }

}
