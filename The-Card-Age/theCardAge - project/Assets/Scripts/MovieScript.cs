using UnityEngine;
using System.Collections;

public class MovieScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void playMovie()
    {
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
    }
}
