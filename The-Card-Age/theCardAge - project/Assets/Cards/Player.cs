using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // Trying to connect the hand with Player
    //  if we keep hand here, then maybe we have its transform?
    //  We'd have to initialize it to something, perhaps make this public?
    //  What about the enemy's?
    public hand handPlace;


    public string bossName;
    public TextAsset deckInfo;


    Deck wholeDeck;
    List<PlayingCard> handDeck;
    bool turnIsDone;
    string selectedAction;

    void Start()
    {
        string[] boss_suboordinates = deckInfo.text.Split(':');
        bossName = boss_suboordinates[0];
        string[] cardNames = boss_suboordinates[1].Split(',');
        Debug.Log(bossName);
        wholeDeck = new Deck(bossName, cardNames);
        Debug.Log(cardNames[0]);
        handDeck = new List<PlayingCard>();
        turnIsDone = false;
        selectedAction = "";

        DrawCard(false);

    }

    void Update()
    {

    }

    public Player(string bossName, string deckInfo)
    {
        string[] cardNames = deckInfo.Split(',');
        Debug.Log(bossName);
        wholeDeck = new Deck(bossName, cardNames);
        Debug.Log(deckInfo[0]);
        handDeck = new List<PlayingCard>();
        turnIsDone = false;
        selectedAction = "";
    }
    
    public void DrawCard(bool again)
    {
        if (handDeck.Count < 7)
        {
            if (handDeck.Count == 0)
            {
                for (int i=0; i<4; i++)
                {
                    PlayingCard card = wholeDeck.DrawCard();
                    if (card == null) {
                        Debug.Log("Deck is empty!!!");
                        return;
                    }
                    handDeck.Add(card);
                }
            }
            else {
                PlayingCard card = wholeDeck.DrawCard();
                if (card == null)
                {
                    Debug.Log("Deck is empty!!!");
                    return;
                }
                handDeck.Add(card);
                if (again)
                {
                    turnIsDone = true;
                }
            }
        }
    }

    public bool getTurnIsDone() {
        bool toReturn = turnIsDone;
        if (turnIsDone)
        {
            turnIsDone = false;
        }
        return toReturn;
    }
    public void selectACard(PlayingCard card)
    {
        selectedAction = card.GetActions()[0];
    }

    public string getAction() { return selectedAction; }
}