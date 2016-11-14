﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // Trying to connect the hand with Player
    //  if we keep hand here, then maybe we have its transform?
    //  We'd have to initialize it to something, perhaps make this public?
    //  What about the enemy's?
    public dropzone handPlace;
    public Board board;


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
                    SetCardToHand(card);
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
                SetCardToHand(card);
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


    void SetCardToHand(PlayingCard card)
    {
        // Trying to add this to a drag object?
        string prefabName = card.GetName().Replace(" ", "") + "Prefab";
        int prefabIndex = board.FindPrefabIndex(prefabName);
        // want to add the required components to here:
        //  Drag (script) with TypeOfItem = Weapon and ItemIndex = prefabIndex
        //      maybe should make a prefab? :/
        //  Layout Element (preferred: 70,110)
        //  Canvas Group (alpha = 1, check everything except Ignore Parent Group)

        GameObject newDrag = new GameObject("Card " + handDeck.Count.ToString());
        newDrag.AddComponent<Image>();
        newDrag.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Card_Images/" + card.GetImage());
        newDrag.GetComponent<Image>().preserveAspect = true;
        newDrag.AddComponent<LayoutElement>();
        newDrag.GetComponent<LayoutElement>().preferredWidth = 70;
        newDrag.GetComponent<LayoutElement>().preferredHeight = 110;
        newDrag.GetComponent<LayoutElement>().flexibleWidth = 0;
        newDrag.GetComponent<LayoutElement>().flexibleHeight = 0;
        newDrag.AddComponent<CanvasGroup>();
        newDrag.GetComponent<CanvasGroup>().alpha = 1;
        newDrag.GetComponent<CanvasGroup>().interactable = true;
        newDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        newDrag.AddComponent<drag>();
        newDrag.GetComponent<drag>().setItemIndex(prefabIndex);
        newDrag.GetComponent<drag>().boardScript = board;

        newDrag.transform.parent = handPlace.transform;
    }
}