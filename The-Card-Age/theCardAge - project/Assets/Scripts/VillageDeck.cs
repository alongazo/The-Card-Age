using UnityEngine;
using System;
using System.Collections.Generic;

public class VillageDeck : MonoBehaviour {

    public int currentSize = 0;
    public int maxSize = 31;
    public int allowableCopies = 2;
    public Dictionary<string,int> playableDeck;
	// Use this for initialization
	void Start () {
        playableDeck = new Dictionary<string, int>();
	    /*foreach(GameObject card in GameObject.Find("CardCollection").GetComponent<CollectionManager>().cardCollection)
        {
            playableDeck.Add(card.name, 0);
        }*/
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    
}
