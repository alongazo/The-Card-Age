using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FindCardInDeck : MonoBehaviour {
    public Image x;
	public void findAndRemoveCard()
    {
        GameObject.Find("PlayerDeck").GetComponent<VillageDeck>().removeCard(x);
    }
}
