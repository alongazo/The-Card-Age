using UnityEngine;
using System;
using System.Collections.Generic;

public class UpdatePlayerCollection : MonoBehaviour {
    public Dictionary<GameObject, int> collection;

    public void addCard(GameObject card)
    {
        if(collection.ContainsKey(card))
        {
            collection[card] += 1;
        }
        else
        {
            collection.Add(card, 1);
        }
    }
	
}
