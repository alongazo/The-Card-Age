using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackSmithButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith")
        {
            transform.GetComponent<Button>().interactable = true;
        }
        else
        {
            transform.GetComponent<Button>().interactable = false;
        }
    }
}
