using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider2 : MonoBehaviour
{

   


    
    public Slider volumeSlider;
    public AudioSource[] audioSources;
    public Text volumeText; // Optional


    void Start()
    {
        // Initialize the slider value to 1 (max volume).
        volumeSlider.value = 1.0f;
    }

    public void OnVolumeChange()
    {
        float newVolume = volumeSlider.value;

        // Update the volume of all audio sources.
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = newVolume;
        }

        // Optional: Update the volume display text.
        if (volumeText != null)
        {
            volumeText.text = "Volume: " + (newVolume * 100).ToString("F0") + "%";
        }
    }
}
