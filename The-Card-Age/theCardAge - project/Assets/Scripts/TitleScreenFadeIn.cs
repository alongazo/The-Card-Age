using UnityEngine;
using System.Collections;

public class TitleScreenFadeIn : MonoBehaviour {
    public bool increaseFade = false;
    public Transform objectToFade;
    public float timeBeforeFade = 0;
    private bool beginFade = false;
	// Use this for initialization
	void Start () {
        StartCoroutine(Wait(timeBeforeFade,"beginFade"));
        //StartCoroutine(Wait(5,"increaseFade"));
	}
	
	// Update is called once per frame
	void Update () {
        if (beginFade)
        {
            Color color = objectToFade.GetComponent<Renderer>().material.color;
            if (!increaseFade)
            {
                color.a -= .03f * Time.deltaTime;
            }
            else
            {
                color.a -= .1f * Time.deltaTime;
            }
            objectToFade.GetComponent<Renderer>().material.color = color;
        }
    }
    IEnumerator Wait(float time,string action)
    {
        yield return new WaitForSeconds(time);
        if(action=="beginFade")
        {
            beginFade = true;
        }
    }
}
