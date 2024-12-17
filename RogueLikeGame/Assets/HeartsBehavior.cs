using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HeartsBehavior : MonoBehaviour {
    public int health;
    public int numOfMaxHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    void Update() {
        if (health > numOfMaxHealth) {
            health = numOfMaxHealth;
        }
        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}

