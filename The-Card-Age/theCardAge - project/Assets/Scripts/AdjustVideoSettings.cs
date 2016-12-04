using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DigitalRuby.SimpleLUT;

public class AdjustVideoSettings : MonoBehaviour {
    public Slider videoSlider;
    public float origValue;
    public float origSliderValue;
    public void Start()
    {
        Camera.main.gameObject.GetComponent<SimpleLUT>().Brightness = videoSlider.value - .5f;
        origValue = Camera.main.gameObject.GetComponent<SimpleLUT>().Brightness;
        origSliderValue = videoSlider.value;
    }
	public void changeBrightness()
    {
        Camera.main.gameObject.GetComponent<SimpleLUT>().Brightness = videoSlider.value-.5f;
    }
    public void restoreDefaults()
    {
        Camera.main.gameObject.GetComponent<SimpleLUT>().Brightness = origValue;
        videoSlider.value = origSliderValue;
    }
}
