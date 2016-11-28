using UnityEngine;
using System;
using System.Collections.Generic;

public class CardDestroyer : MonoBehaviour {

    public void destroyCards()
    {
        foreach(Transform card in transform)
        {
            GameObject.Destroy(card.gameObject);
        }
    }

}
