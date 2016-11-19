using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextChanger : MonoBehaviour {
    public GameObject deck;
    public Text deckAmount;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        int deckSize = 0;
        if (deck.GetComponent<VillageDeck>().playableDeck != null)
        {
            foreach (GameObject card in deck.GetComponent<VillageDeck>().playableDeck)
            {
                if (card != null)
                    deckSize++;
            }
        }
        deckAmount.text = deckSize.ToString() + "/31";
    }
    public void ChangeText()
    {
        int deckSize = 0;
        foreach(GameObject card in deck.GetComponent<VillageDeck>().playableDeck)
        {
            deckSize++;
        }
        deckAmount.text = deckSize.ToString() + "/31";
    }
}
