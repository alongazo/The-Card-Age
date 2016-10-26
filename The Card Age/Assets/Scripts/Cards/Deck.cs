using System;
using System.Collections.Generic;

public class Deck
{
    BossCard heroCard;
    List<PlayingCard> playableDeck;
    List<PlayingCard> discardDeck;

    Random RNG;


    public Deck(string text)
    {

    }


    public Deck(string bossName, string[]cardNames)
    {
        playableDeck = new List<PlayingCard>();
        heroCard = Globals.bossDatabase[bossName];
        foreach (string name in cardNames)
        {
            playableDeck.Add(Globals.cardDatabase[name]);
        }
        discardDeck = new List<PlayingCard>();
        RNG = new Random();
    }

    public PlayingCard DrawCard()
    {
        int index = RNG.Next() % playableDeck.Count;
        PlayingCard cardToGive = playableDeck[index];
        playableDeck.RemoveAt(index);
        return cardToGive;
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
}