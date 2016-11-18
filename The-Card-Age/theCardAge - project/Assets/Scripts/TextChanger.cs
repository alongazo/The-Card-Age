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
        /*if (deck.GetComponent<Deck>().playableDeck != null)
        {
            foreach (PlayingCard card in deck.GetComponent<Deck>().playableDeck)
            {
                if (card != null)
                    deckSize++;
            }
        }*/
        deckAmount.text = deckSize.ToString() + "/30";
    }
    public void ChangeText()
    {
        int deckSize = 0;
        /*foreach(PlayingCard card in deck.GetComponent<Deck>().playableDeck)
        {
            deckSize++;
        }*/
        deckAmount.text = deckSize.ToString() + "/30";
    }
}
