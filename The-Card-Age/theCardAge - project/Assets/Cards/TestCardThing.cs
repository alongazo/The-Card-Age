using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestCardThing : MonoBehaviour {

    public string cardName;
    public drag drag;

    PlayingCard testCard;


	// Use this for initialization
	void Start () {
        testCard = new PlayingCard(Globals.cardDatabase[cardName]);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
