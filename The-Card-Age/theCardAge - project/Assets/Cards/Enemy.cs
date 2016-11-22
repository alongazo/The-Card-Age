using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public Board board;
    //public Text deckCount;

    public string bossName;
    public TextAsset deckInfo;

    private bool firstTurn;

    GameObject wholeDeck;
    List<PlayingCard> handDeck;
    bool turnIsDone;
    string selectedAction;
    bool hasBossCard;

    bool isMovingCard;

    void Start()
    {
        string[] boss_suboordinates = deckInfo.text.Split(':');
        bossName = boss_suboordinates[0];
        string[] cardNames = boss_suboordinates[1].Split(',');
        ////////Debug.Log(bossName);
        wholeDeck = new GameObject();
        wholeDeck.AddComponent<Deck>();
        wholeDeck.GetComponent<Deck>().Initialize(bossName, cardNames);
        //////Debug.Log(cardNames[0]);
        handDeck = new List<PlayingCard>();
        turnIsDone = false;
        selectedAction = "";
        firstTurn = true;
        DrawCard(false);
        hasBossCard = true;
    }

    void Update()
    {
        if (isMovingCard)
        {

        }

        if (!board.IsWhiteTurn())
        {
            // time for the enemy to make a move
            DrawCard(false);

            // dumb thing: summons all the things it can and then moves them
            if (firstTurn)
            {
                PlayingCard toPlace = GetCardOfType(CardType.Boss);
                if (toPlace != null)
                {
                    SetCardToHand(toPlace);
                    firstTurn = false;
                }
            }
            else {
                PlayingCard toPlace = GetCardOfType(CardType.Minion);
                if (toPlace != null)
                {
                    SetCardToHand(toPlace);
                }

            }
            board.EndTurn();
        }
    }

    public void DrawCard(bool again)
    {
        if (firstTurn)
        {
            for (int i = 0; i < 4; i++)
            {
                PlayingCard card = wholeDeck.GetComponent<Deck>().DrawCard();
                if (card == null)
                {
                    ////Debug.Log("Deck is empty!!!");
                    return;
                }
                handDeck.Add(card);
            }
        }
        else if (handDeck.Count < 7)
        {
            PlayingCard card = wholeDeck.GetComponent<Deck>().DrawCard();
            if (card == null)
            {
                ////Debug.Log("Deck is empty!!!");
                return;
            }
            handDeck.Add(card);
            //SetCardToHand(card);
            if (again)
            {
                turnIsDone = true;
            }
        }
    }

    public bool getTurnIsDone()
    {
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

        GameObject newDrag = new GameObject("Card " + handDeck.Count.ToString());

        newDrag.AddComponent<drag>();
        newDrag.GetComponent<drag>().setItemIndex(prefabIndex);
        newDrag.GetComponent<drag>().setCard(card);
        newDrag.GetComponent<drag>().setOriginator(ref handDeck);
        newDrag.GetComponent<drag>().boardScript = board;

        //newDrag.transform.SetParent(handPlace.transform, false);
        int col, row;
        do
        {
            col = Random.Range(0, Globals.numCols);
            row = Random.Range(Globals.numRows - 2, Globals.numRows);
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
}