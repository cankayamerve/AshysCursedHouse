using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;  // Variable to hold a reference to the audio source
    public AudioClip soundClip;     // The audio file you want to play

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundClip;
        audioSource.Play();
    }
}
