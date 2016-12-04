using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseGame : MonoBehaviour {
    public bool paused = false;
    public bool options = false;
    public bool sound = false;
    public bool video = false;
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public GameObject soundPanel;
    public GameObject videoPanel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                pausePanel.transform.localPosition = new Vector3(0, 0, 0);
                paused = true;
                GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused = true;
            }
            else
            {
                pausePanel.transform.localPosition = new Vector3(1000, 1000, 1000);
                optionsPanel.transform.localPosition = new Vector3(1000, 1000, 1000);
                videoPanel.transform.localPosition = new Vector3(1000, 1000, 1000);
                soundPanel.transform.localPosition = new Vector3(1000, 1000, 1000);
                paused = false;
                options = false;
                video = false;
                sound = false;
                GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused = false;
            }
        }
	}
    public void pauseUnpause()
    {
        if (!paused)
        {
            pausePanel.transform.localPosition = new Vector3(0, 0, 0);
            paused = true;
            GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused = true;
        }
        else
        {
            pausePanel.transform.localPosition = new Vector3(1000, 1000, 1000);
            paused = false;
            GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused = false;
        }
    }
    public void changeToOptions()
    {
        if(!options)
        {
            pausePanel.transform.localPosition = new Vector3(1000, 1000, 1000);
            optionsPanel.transform.localPosition = new Vector3(0, 0, 0);
            options = true;
        }
        else
        {
            pausePanel.transform.localPosition = new Vector3(0, 0, 0);
            optionsPanel.transform.localPosition = new Vector3(1000, 1000, 1000);
            options = false;
        }
    }
    public void changeToSound()
    {
        if (!sound)
        {
            optionsPanel.transform.localPosition = new Vector3(1000, 1000, 1000);
            soundPanel.transform.localPosition = new Vector3(0, 0, 0);
            sound = true;
        }
        else
        {
            optionsPanel.transform.localPosition = new Vector3(0, 0, 0);
            soundPanel.transform.localPosition = new Vector3(1000, 1000, 1000);
            sound = false;
        }
    }
    public void changeToVideo()
    {
        if (!video)
        {
            optionsPanel.transform.localPosition = new Vector3(1000, 1000, 1000);
            videoPanel.transform.localPosition = new Vector3(0, 0, 0);
            video = true;
        }
        else
        {
            optionsPanel.transform.localPosition = new Vector3(0, 0, 0);
            videoPanel.transform.localPosition = new Vector3(1000, 1000, 1000);
            video = false;
        }
    }
}
