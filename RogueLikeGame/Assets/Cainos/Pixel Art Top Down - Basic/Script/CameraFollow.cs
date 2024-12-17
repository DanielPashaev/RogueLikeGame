﻿using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Assign the player's Transform in the Inspector
    public Vector3 offset;   // Optional offset to adjust camera position
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }        
    }
}
