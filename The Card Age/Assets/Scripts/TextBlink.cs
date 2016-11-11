using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TextBlink : MonoBehaviour {
    public Text play;
    private bool fade = true;
    private bool initiateFade = false;
 	// Use this for initialization
	void Start () {
        StartCoroutine(Wait(10));
	}
	
	// Update is called once per frame
	void Update () {
        Color textcolor = play.color;
        if (initiateFade)
        {
            if (fade)
            {
                textcolor.a -= .3f * Time.deltaTime;
            }
            else
            {
                textcolor.a += .3f * Time.deltaTime;
            }
            if (play.color.a <= 0)
            {
                fade = false;
            }
            else if (play.color.a >= .5)
            {
                fade = true;
            }
            play.color = textcolor;
        }
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        initiateFade = true;
    }
}
