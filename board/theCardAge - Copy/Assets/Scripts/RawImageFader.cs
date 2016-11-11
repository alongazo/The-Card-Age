using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RawImageFader : MonoBehaviour {
    public RawImage imageToFade;
    private bool beginFade = false;
    public bool increaseFade = false;
    public float timeBeforeFade = 0;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Wait(timeBeforeFade, "beginFade"));
        //StartCoroutine(Wait(5,"increaseFade"));
    }

    // Update is called once per frame
    void Update()
    {
        if (beginFade)
        {
            Color color = imageToFade.color;
            if (!increaseFade)
            {
                color.a -= .03f * Time.deltaTime;
            }
            else
            {
                color.a -= .1f * Time.deltaTime;
            }
            imageToFade.color = color;
        }
    }
    IEnumerator Wait(float time, string action)
    {
        yield return new WaitForSeconds(time);
        if (action == "beginFade")
        {
            beginFade = true;
        }
    }
}
