using UnityEngine;
using System;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    BossCard heroCard;
    List<PlayingCard> playableDeck;
    List<PlayingCard> discardDeck;

    System.Random RNG;

    bool firstDraw;
    int removedCardIndex;

    public void Initialize(string[] cardNames, bool isPlayer)
    {
        playableDeck = new List<PlayingCard>();
        //Debug.Log("Inside Deck constructor for this bosscard: " + bossName);

        heroCard = Globals.bossDatabase[cardNames[0].Split(':')[0]];
        heroCard.SetIsPlayer(isPlayer);
        int index = 0;
        cardNames[0] = "Heal:1"; // This is the third heal
        foreach (string card in cardNames)
        {
            string[] info = card.Split(':');
            for (int i = 0; i < Convert.ToInt32(info[1]); i++)
            {
                playableDeck.Add(Globals.cardDatabase[info[0]]);
                playableDeck[index].idNumber = index;
                playableDeck[index].SetIsPlayer(isPlayer);
                index++;
            }
        }
        discardDeck = new List<PlayingCard>();
        RNG = new System.Random();

        firstDraw = true;
    }

    public PlayingCard DrawCard()
    {
        PlayingCard cardToGive = null;
        if (firstDraw)
        {
            firstDraw = false;
            removedCardIndex = 0;
            return heroCard;
        }
        if (playableDeck.Count > 0)
        {
            removedCardIndex = RNG.Next() % playableDeck.Count;
            cardToGive = playableDeck[removedCardIndex];
            playableDeck.RemoveAt(removedCardIndex);
        }
        return cardToGive;
    }

    public int RemovedCardIndex() // this is just the index of the latest card drawn
    {
        return removedCardIndex;
    }

    public void DiscardCard(PlayingCard cardToDelete)
    {
        playableDeck.Remove(cardToDelete);
        discardDeck.Add(cardToDelete);
    }

    public string SaveDeck()
    {
        string saveString = playableDeck.Count.ToString() + 1 + " : " + heroCard.GetName();
        for (int i=0; i<playableDeck.Count; i++)
        {
            saveString += "," + playableDeck[i].GetName();
        }
        return saveString;
    }

    public int SizeOFDeck() { return playableDeck.Count; }
}