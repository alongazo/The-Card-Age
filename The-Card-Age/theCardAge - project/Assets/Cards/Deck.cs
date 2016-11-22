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

    public Deck(string bossName, string[]cardNames)
    {
        playableDeck = new List<PlayingCard>();
        //Debug.Log("Inside Deck constructor for this bosscard: " + bossName);
        heroCard = Globals.bossDatabase[bossName];
        //Debug.Log(Globals.bossDatabase.ContainsKey(bossName));

        int index = 0;
        foreach (string name in cardNames)
        {
            //Debug.Log("is " + name + " in the dictionary? " + Globals.cardDatabase.ContainsKey(name).ToString());
            playableDeck.Add(Globals.cardDatabase[name]);
            playableDeck[index].idNumber = index;
            index++;
        }
        discardDeck = new List<PlayingCard>();
        RNG = new System.Random();

        firstDraw = true;
    }

    public void Initialize(string bossName, string[] cardNames)
    {
        playableDeck = new List<PlayingCard>();
        //Debug.Log("Inside Deck constructor for this bosscard: " + bossName);
        heroCard = Globals.bossDatabase[bossName];
        //Debug.Log(Globals.bossDatabase.ContainsKey(bossName));

        int index = 0;
        foreach (string name in cardNames)
        {
            //Debug.Log("is " + name + " in the dictionary? " + Globals.cardDatabase.ContainsKey(name).ToString());
            playableDeck.Add(Globals.cardDatabase[name]);
            playableDeck[index].idNumber = index;
            index++;
        }
        discardDeck = new List<PlayingCard>();
        RNG = new System.Random();

        firstDraw = true;
    }

    public PlayingCard DrawCard()
    {
        PlayingCard cardToGive = null;
        //Debug.Log(playableDeck.Count);
        if (firstDraw)
        {
            firstDraw = false;
            return heroCard;
        }
        if (playableDeck.Count > 0)
        {
            int index = RNG.Next() % playableDeck.Count;
            cardToGive = playableDeck[index];
            playableDeck.RemoveAt(index);
        }
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

    public int SizeOFDeck() { return playableDeck.Count + ((heroCard == null)? 0 : 1); }
}