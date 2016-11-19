using UnityEngine;
using System.Collections;

public class TrackSetting : MonoBehaviour {

    public string currentSetting;

    public void changeSetting(string setting)
    {
        currentSetting = setting;
    }
}
