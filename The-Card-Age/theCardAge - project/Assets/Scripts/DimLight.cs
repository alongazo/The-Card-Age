using UnityEngine;
using System.Collections;

public class DimLight : MonoBehaviour {
    public float timeBeforeDim = 0;
    public float speed = .005f;
    public float maxIntensity = .5f;
    private bool dimLight = false;
    public bool dimOut = true;
    public Light lightToDim;
	// Use this for initialization
	void Start () {
        StartCoroutine(Wait(timeBeforeDim));
	}
	
	// Update is called once per frame
	void Update () {
	    if(dimLight&&dimOut)
        {
            lightToDim.intensity -= speed;
        }
        else if(dimLight)
        {
            if (lightToDim.intensity < maxIntensity)
            {
                lightToDim.intensity += speed;
            }
        }
	}

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        dimLight = true;
    }
}
