using UnityEngine;

public class PlayAudioOnArea : MonoBehaviour
{
    public AudioSource audioSource;  // Assign in Inspector
    private bool hasPlayed = false;  // Prevents replaying the audio

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name);

        // Check if the object entering is tagged "Player" and audio hasn't played yet
        if (!hasPlayed && other.CompareTag("Player"))
        {
            Debug.Log("✅ Player detected. Playing audio once.");
            audioSource.Play();
            hasPlayed = true;
        }
        else
        {
            Debug.Log("⛔ Audio already played or object is not player.");
        }
    }
}
