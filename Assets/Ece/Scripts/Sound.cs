using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    private Sprite soundOn;
    public Sprite soundOff;
    public Button button;
    private bool isOn = true;
    public AudioSource audioSource;
    public AudioSource audioSourcefirefly;

    // Start is called before the first frame update
    void Start()
    {
        soundOn = button.image.sprite;
        audioSource.mute = false;
        audioSourcefirefly.mute = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundOnOff()
    {
        if (isOn)
        {
            button.image.sprite = soundOff;
            isOn = false;
            audioSource.mute = true;
            audioSourcefirefly.mute = true;
        }
        else
        {
            button.image.sprite = soundOn;
            isOn = true;
            audioSource.mute = false;
            audioSourcefirefly.mute = false;
        }
    }
}
