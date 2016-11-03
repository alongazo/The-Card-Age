using UnityEngine;
using System.Collections;

public class hand : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            child.gameObject.transform.localPosition =  Vector3.zero;

        }
	}
	
	// Update is called once per frame
	void Update () {

    }
}
