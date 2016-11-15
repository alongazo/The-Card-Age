using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class VillageDrag : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("Down");
        }
            
	}
    public void onPointerDown(PointerEventData eventData)
    {
        Debug.Log("working");
    }
}
