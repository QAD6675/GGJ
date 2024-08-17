using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{
    public AudioClip[] gameTracks; // Array to hold game tracks
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (gameTracks.Length > 0)
        {
            PlayRandomTrack();
        }
    }

    public void PlayRandomTrack()
    {
        int randomIndex = Random.Range(0, gameTracks.Length);
        audioSource.clip = gameTracks[randomIndex];
        audioSource.loop = true;
        audioSource.Play();
    }
}
