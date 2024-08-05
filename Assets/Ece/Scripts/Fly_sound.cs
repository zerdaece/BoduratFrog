using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_sound : MonoBehaviour
{// Ses dosyalarının atanacağı dizi
    public AudioClip[] flySounds;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Eğer ses dosyaları atanmışsa rastgele bir ses çal
        if (flySounds.Length > 0)
        {
            PlayRandomSound();
        }
    }

    void PlayRandomSound()
    {
        // Rastgele bir ses dosyası seç
        int randomIndex = Random.Range(0, flySounds.Length);
        AudioClip randomClip = flySounds[randomIndex];

        // Ses dosyasını AudioSource'a ata ve çal
        audioSource.clip = randomClip;
        audioSource.Play();
    }
}