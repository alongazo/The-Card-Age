using UnityEngine;
using System.Collections;

public class TitleScreenFadeIn : MonoBehaviour {
    public bool increaseFade = false;
	// Use this for initialization
	void Start () {
        Wait(5);
        increaseFade = true;
	}
	
	// Update is called once per frame
	void Update () {
        Color color = transform.GetComponent<Renderer>().material.color;
        if (!increaseFade)
        {
            color.a -= .03f * Time.deltaTime;
        }
        else
        {
            color.a -= .1f * Time.deltaTime;
        }
        transform.GetComponent<Renderer>().material.color = color;

    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
