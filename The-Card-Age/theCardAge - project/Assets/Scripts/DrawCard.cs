using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class DrawCard : MonoBehaviour {

    public GameObject currentDeck;
    public Button confirmCards;
    public Canvas boardCanvas;
    public Vector3 cardPosition;
	// Use this for initialization
	void Start () {
        List<PlayingCard> deckInfo = currentDeck.GetComponent<Deck>().playableDeck;
        Quaternion rotateCard = Quaternion.identity;
        rotateCard.eulerAngles = new Vector3(0, 180, 0);
        for(int i=0;i<3;i++)
        {
            int rand = (int) Mathf.Floor(UnityEngine.Random.Range(0f, 31f));
            PlayingCard newCard = Instantiate(deckInfo[rand], cardPosition, rotateCard) as PlayingCard;
            newCard.transform.localScale = new Vector3(15f, .001f, 15f);
            cardPosition.x += 2.8f;
        }
        cardPosition.x -= 3*2.8f;
        //cardPosition.z -= 3;
        Button newButton = Instantiate(confirmCards, cardPosition, Quaternion.identity,boardCanvas.transform) as Button;
        

    }

    // Update is called once per frame
    void Update () {
	
	}
    public void onClick()
    {
        Debug.Log("Random number is...");
        Debug.Log(Mathf.Floor(UnityEngine.Random.Range(0f,31f)));
    }
}
