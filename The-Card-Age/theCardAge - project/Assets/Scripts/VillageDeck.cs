using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class VillageDeck : MonoBehaviour {

    public int currentSize = 0;
    public int maxSize = 30;
    public int allowableCopies = 2;
    public Dictionary<string,int> playableDeck;
	// Use this for initialization
	public void Start () {
        playableDeck = new Dictionary<string, int>();
	    /*foreach(GameObject card in GameObject.Find("CardCollection").GetComponent<CollectionManager>().cardCollection)
        {
            playableDeck.Add(card.name, 0);
        }*/
	}
	
	// Update is called once per frame
	public void Update () {
        
	}
    public void removeCard(Image x)
    {
        string cardName = x.transform.parent.GetComponent<RemoveFromDeck>().cardName;
        foreach (var card in playableDeck)
        {
            Debug.Log(card.Key + card.Value.ToString());
        }
        if(playableDeck.ContainsKey(cardName))
        {
            playableDeck[cardName] -= 1;
            if(playableDeck[cardName]==0)
            {
                playableDeck.Remove(cardName);
            }
            currentSize--;
            Destroy(x.transform.parent.gameObject);
        }
        Debug.Log("After Deletion");
        foreach (var card in playableDeck)
        {
            Debug.Log(card.Key + card.Value.ToString());
        }
    }
}
