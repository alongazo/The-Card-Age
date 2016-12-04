using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class AdjustAudioSettings : MonoBehaviour {

    public AudioSource music;
    public Transform SFX;
    public Slider musicSlider;
    public Slider sfxSlider;
    public void changeMusicVolume()
    {
        music.volume = musicSlider.value;
    }
    public void changeSFXVolume()
    {
        foreach(Transform sfx in SFX)
        {
            if (sfx.GetComponent<AudioSource>())
            {
                sfx.GetComponent<AudioSource>().volume = sfxSlider.value;
            }
        }
    }
    public void restoreDefaults()
    {
        musicSlider.value = 1;
        sfxSlider.value = 1;
        music.volume = 1;
        foreach (Transform sfx in SFX)
        {
            if (sfx.GetComponent<AudioSource>())
            {
                sfx.GetComponent<AudioSource>().volume = 1;
            }
        }
    }
}
