using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class Options : MonoBehaviour {
    bool muted;
    public Text volumeValueText;
    public Slider volumeSlider;

    private float currentVol;
    public float defaultVol = 0.8f;

    [SerializeField]
    Text mutetext;
	// Use this for initialization
	void Start ()
    {
        AudioListener.volume = defaultVol;
        currentVol = defaultVol;
        volumeSlider.value = defaultVol;
        volumeValueText.text = (Mathf.Round(AudioListener.volume * 100)).ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (muted)
        {
            AudioListener.volume = 0;
            mutetext.text = "Unmute";
        }
        else if (!muted)
        {
            //AudioListener.volume = 1;
            AudioListener.volume = currentVol;
            mutetext.text = "Mute";
        }
	
	}

    public void Quit()
    {
        Application.Quit();
    }
    public void Mute()
    {
        muted = !muted;
    }


    public void volumeSlide(float value)
    {
        currentVol = value;
        volumeValueText.text = (Mathf.Round(value * 100)).ToString();

    }
}
