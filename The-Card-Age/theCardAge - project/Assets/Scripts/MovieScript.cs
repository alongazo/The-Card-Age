using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]
public class MovieScript : MonoBehaviour {

    public MovieTexture movie;
    private AudioSource audio;
    public AudioSource villageMusic;
    public AudioSource sfx;
    private float origVillageVolume;
    private float origSfxVolume;
    private bool moviePlaying = false;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	    if(moviePlaying)
        {
            if(!movie.isPlaying)
            {
                transform.localPosition = new Vector3(1000, 1000, 1000);
                moviePlaying = false;
                villageMusic.volume = origVillageVolume;
                sfx.volume = origSfxVolume;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                movie.Stop();
                audio.Stop();
                villageMusic.volume = origVillageVolume;
                sfx.volume = origSfxVolume;
                transform.localPosition = new Vector3(1000, 1000, 1000);
                moviePlaying = false;
            }

        }
	}
    public void playMovie()
    {
        origVillageVolume = villageMusic.volume;
        origSfxVolume = sfx.volume;
        transform.localPosition = new Vector3(0, 0, 0);
        villageMusic.volume = 0;
        sfx.volume = 0;
        GetComponent<RawImage>().texture = movie as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.clip = movie.audioClip;
        movie.Play();
        audio.Play();  
        if(movie.isPlaying)
        {
            moviePlaying = true;
        }
    }
}
