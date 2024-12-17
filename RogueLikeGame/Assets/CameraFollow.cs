using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public Vector3 offset;   // Offset to keep some space between player and camera

    void Update()
    {
        if (player != null)
        {
            // Move the camera to the player's position with an offset
            transform.position = player.position + offset;
        }
    }
}
