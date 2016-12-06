using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SellScreenButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "shop-sell")
        {
            transform.GetComponent<Button>().interactable = true;
        }
        else
        {
            transform.GetComponent<Button>().interactable = false;
        }
	}
}
