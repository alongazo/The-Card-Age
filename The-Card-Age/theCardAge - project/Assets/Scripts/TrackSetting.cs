using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrackSetting : MonoBehaviour {

    public string currentSetting;
    public bool cardPackInMotion = false;
    public bool blocked = false;
    public bool paused = false;

    public void changeSetting(string setting)
    {
        currentSetting = setting;
    }
    public void changeCardPackInMotion()
    {
        cardPackInMotion = false;
        GameObject.Find("Collect Button").GetComponent<Button>().interactable = false;
    }
}
