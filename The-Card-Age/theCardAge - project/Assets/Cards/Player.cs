﻿using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IEndDragHandler
{
    // Trying to connect the hand with Player
    //  if we keep hand here, then maybe we have its transform?
    //  We'd have to initialize it to something, perhaps make this public?
    //  What about the enemy's?
    public dropzone handPlace;
    public Board board;
    public Text deckCount;

    public string bossName;
    public TextAsset deckInfo;

    GameObject wholeDeck;
    static List<PlayingCard> handDeck;
    bool turnIsDone;
    string selectedAction;

    static bool canSkill; public static void SetCanSkill(bool truth) { canSkill = truth; }
    public static bool doubleSummon;



    List<Coordinate> cardsOnBoard = new List<Coordinate>();

    void Start()
    {
        wholeDeck = new GameObject();
        string[] deck_text = deckInfo.text.Split('\n');
        Debug.Log(bossName);
        wholeDeck.AddComponent<Deck>();
        wholeDeck.GetComponent<Deck>().Initialize(deck_text, true);
        ////Debug.Log(cardNames[0]);
        handDeck = new List<PlayingCard>();
        turnIsDone = false;
        selectedAction = "";
        Initialize();
        board.EndTurn();

        canSkill = true;
        doubleSummon = false;
    }

    void Initialize()
    {
        FirstDraw();
        PlaceFirstCard();
    }

    void Update()
    {
        if (doubleSummon)
        {
            RandomSummon();
            RandomSummon();
            doubleSummon = false;
        }
    }

    void FirstDraw()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayingCard card = wholeDeck.GetComponent<Deck>().DrawCard();
            if (card == null) { return; }
            handDeck.Add(card);
            SetCardToHand(card);
        }
        deckCount.text = wholeDeck.GetComponent<Deck>().SizeOFDeck().ToString();
    }

    public void DrawCard(bool again)
    {
        if (handDeck.Count < 7)
        {
            PlayingCard card = wholeDeck.GetComponent<Deck>().DrawCard();
            if (card == null) { return; }
            handDeck.Add(card);
            SetCardToHand(card);
            if (again) { turnIsDone = true; }
        }
        deckCount.text = wholeDeck.GetComponent<Deck>().SizeOFDeck().ToString();
    }

    void PlaceFirstCard()
    {
        GameObject newDrag = handPlace.transform.GetChild(0).gameObject;
        newDrag.GetComponent<drag>().OnEndDrag(3, 1);
        board.SetBossCardLocation(new Coordinate(3, 1), true);
        cardsOnBoard.Add(new Coordinate(3, 1));
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
        newDrag.AddComponent<CanvasGroup>();
        newDrag.GetComponent<CanvasGroup>().alpha = 1;
        newDrag.GetComponent<CanvasGroup>().interactable = true;
        newDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

        newDrag.AddComponent<drag>();
        newDrag.GetComponent<drag>().setItemIndex(prefabIndex);
        newDrag.GetComponent<drag>().setCard(card);
        newDrag.GetComponent<drag>().setOriginator(ref handDeck);
        newDrag.GetComponent<drag>().boardScript = board;

        newDrag.transform.SetParent(handPlace.transform, false);
    }

    void RandomSummon()
    {
        int prefabIndex = board.FindPrefabIndex("WyvernPrefab");
        GameObject newDrag = new GameObject("Card " + handDeck.Count.ToString());
        newDrag.AddComponent<drag>();
        newDrag.GetComponent<drag>().setItemIndex(prefabIndex);
        newDrag.GetComponent<drag>().setCard(Globals.cardDatabase["Wyvern"]);
        newDrag.GetComponent<drag>().setOriginator(ref handDeck);
        newDrag.GetComponent<drag>().boardScript = board;
        int col, row;
        do
        {
            col = Random.Range(0, Globals.numCols);
            row = Random.Range(0, 3);
        } while (!newDrag.GetComponent<drag>().OnEndDrag(col, row));
    }

    PlayingCard GetCardOfType(CardType cardtype)
    {
        foreach (PlayingCard card in handDeck)
        {
            if (card.GetCardType() == cardtype)
            {
                return card;
            }
        }
        return new PlayingCard();
    }


    public void SetCardAt(Coordinate place)
    {
        cardsOnBoard.Add(place);
    }

    public void ResetTurn()
    {
        foreach (Coordinate card in cardsOnBoard)
        {
            board.cards[card.col, card.row].ResetTurn();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetCardAt(new Coordinate(board.selectionX, board.selectionY));
    }

    public void RemoveCardAt(int x, int y)
    {
        cardsOnBoard.Remove(new Coordinate(x, y));
    }

    public void UpdateCardAt(Coordinate prev, Coordinate next)
    {
        cardsOnBoard.Remove(prev);
        cardsOnBoard.Add(next);
    }

    static public int HandSize() { return handDeck.Count; }
}