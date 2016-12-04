using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonActivation : MonoBehaviour {
    public Button thisButton;
	void Update () {
        bool block = GameObject.Find("SettingTracker").GetComponent<TrackSetting>().blocked;
        if(block)
        {
            thisButton.interactable = false;
        }
        else
        {
            thisButton.interactable = true;
        }
	}
}
