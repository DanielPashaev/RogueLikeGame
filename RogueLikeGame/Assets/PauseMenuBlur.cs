using UnityEngine;

public class PauseMenuBlur : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject globalVolume;
    public void EnableBlur(bool enable) {
        globalVolume.SetActive(enable);
    }
}
