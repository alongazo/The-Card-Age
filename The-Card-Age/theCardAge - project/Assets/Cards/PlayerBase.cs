//using UnityEngine;
//using UnityEditor;
//using UnityEngine.UI;
//using System.Collections.Generic;
//using UnityEngine.EventSystems;

//public class PlayerBase : MonoBehaviour, IEndDragHandler
//{
//    /// <summary>
//    //  Attempting to make a Base class for both Player and Enemy
//    /// </summary>
//    public dropzone handPlace;
//    public Board board;
//    public Text deckCount;
    
//    public TextAsset deckInfo;

//    GameObject wholeDeck;
//    static List<PlayingCard> handDeck;

//    static bool canSkill; public static void SetCanSkill(bool truth) { canSkill = truth; }
//    public static bool CanSkill() { return canSkill; }
//    public static bool doubleSummon;
//    public static bool surroundDamage;
//    int surroundTurnCount;
//    int surroundDamageAmount; public void SetSurroundDamage(int damage) { surroundDamage = true; surroundDamageAmount = damage; surroundTurnCount = 5; }



//    //Coordinate startCoordinate;

//    List<Coordinate> cardsOnBoard = new List<Coordinate>();

//    void Start()
//    {
//        wholeDeck = new GameObject();
//        string[] deck_text = deckInfo.text.Split('\n');
//        wholeDeck.AddComponent<Deck>();
//        wholeDeck.GetComponent<Deck>().Initialize(deck_text, true);
//        handDeck = new List<PlayingCard>();
//        //Initialize(startCoordinate); <- would call this in the actual Player, Enemy class!

//        canSkill = true;
//        doubleSummon = false;
//        surroundDamage = false;
//        surroundTurnCount = 0;
//        surroundDamageAmount = 0;
//    }

//    void Initialize(Coordinate startCoordinate)
//    {
//        FirstDraw();
//        PlaceFirstCard(startCoordinate);
//        board.EndTurn();
//    }

//    void Update()
//    {
//        if (doubleSummon)
//        {
//            RandomSummon("Card");
//            RandomSummon("Card");
//            doubleSummon = false;
//        }
//    }

//    void FirstDraw()
//    {
//        for (int i = 0; i < 5; i++)
//        {
//            PlayingCard card = wholeDeck.GetComponent<Deck>().DrawCard();
//            if (card == null) { return; }
//            handDeck.Add(card);
//            SetCardToHand(card);
//        }
//        deckCount.text = wholeDeck.GetComponent<Deck>().SizeOFDeck().ToString();
//    }

//    public void DrawCard(bool again)
//    {
//        if (handDeck.Count < 7)
//        {
//            PlayingCard card = wholeDeck.GetComponent<Deck>().DrawCard();
//            if (card == null) { return; }
//            handDeck.Add(card);
//            SetCardToHand(card);
//            if (again) { board.EndTurn(); }
//        }
//        deckCount.text = wholeDeck.GetComponent<Deck>().SizeOFDeck().ToString();
//    }

//    void PlaceFirstCard(Coordinate startCoordinate)
//    {
//        GameObject newDrag = handPlace.transform.GetChild(0).gameObject;
//        newDrag.GetComponent<drag>().OnEndDrag(startCoordinate.col, startCoordinate.row);
//        board.SetBossCardLocation(startCoordinate, true);
//        cardsOnBoard.Add(startCoordinate);
//    }

    
//    // Object name = "Card" for player, "Enemy Card" for enemy
//    // showBack -> true if using aback.png instead of the file name
//    // See if this is really necessary.... um... the Player and Enemy might have call this function...
//    void SetCardToHand(PlayingCard card, string objectName, bool showBack)
//    {
//        // Trying to add this to a drag object?
//        string prefabName = card.GetName().Replace(" ", "") + "Prefab";
//        int prefabIndex = board.FindPrefabIndex(prefabName);

//        GameObject newDrag = new GameObject(objectName + handDeck.Count.ToString());
//        newDrag.AddComponent<Image>();
//        newDrag.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Card_Images/" + ((showBack)? "aback.png" : card.GetImage()));
//        newDrag.GetComponent<Image>().preserveAspect = true;
//        newDrag.AddComponent<LayoutElement>();
//        newDrag.AddComponent<CanvasGroup>();
//        newDrag.GetComponent<CanvasGroup>().alpha = 1;
//        newDrag.GetComponent<CanvasGroup>().interactable = true;
//        newDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;

//        newDrag.AddComponent<drag>();
//        newDrag.GetComponent<drag>().setItemIndex(prefabIndex);
//        newDrag.GetComponent<drag>().setCard(card);
//        newDrag.GetComponent<drag>().setOriginator(ref handDeck);
//        newDrag.GetComponent<drag>().boardScript = board;

//        newDrag.transform.SetParent(handPlace.transform, false);
//    }

//    void RandomSummon(string objectName)
//    {
//        int prefabIndex = board.FindPrefabIndex("WyvernPrefab");
//        GameObject newDrag = new GameObject(objectName + handDeck.Count.ToString());
//        newDrag.AddComponent<drag>();
//        newDrag.GetComponent<drag>().setItemIndex(prefabIndex);
//        newDrag.GetComponent<drag>().setCard(Globals.cardDatabase["Wyvern"]);
//        newDrag.GetComponent<drag>().setOriginator(ref handDeck);
//        newDrag.GetComponent<drag>().boardScript = board;

//        FindWhereToPlaceCard(ref newDrag);
//    }

//    void FindWhereToPlaceCard(ref GameObject objectToPlace)
//    {
//        int col, row;
//        do
//        {
//            col = Random.Range(0, Globals.numCols);
//            row = Random.Range(0, 3);
//        } while (!objectToPlace.GetComponent<drag>().OnEndDrag(col, row));
//        cardsOnBoard.Add(new Coordinate(col, row));
//    }

//    int GetCardOfType(CardType cardtype)
//    {
//        List<int> indexes = new List<int>();
//        for (int i = 0; i < handDeck.Count; i++)
//        {
//            if (handDeck[i].GetCardType() == cardtype)
//            {
//                //Debug.Log("Card " + handDeck[i].GetName() + " is type " + handDeck[i].GetCardType().ToString());
//                indexes.Add(i);
//            }
//        }
//        if (indexes.Count > 0)
//        {
//            return indexes[Random.Range(0, indexes.Count)];
//        }
//        return -1;
//    }


//    public void SetCardAt(Coordinate place)
//    {
//        cardsOnBoard.Add(place);
//    }

//    public void ResetTurn()
//    {
//        foreach (Coordinate card in cardsOnBoard)
//        {
//            board.cards[card.col, card.row].ResetTurn();
//        }
//    }

//    public void OnEndDrag(PointerEventData eventData)
//    {
//        SetCardAt(new Coordinate(board.selectionX, board.selectionY));
//    }

//    public void RemoveCardAt(int x, int y)
//    {
//        cardsOnBoard.Remove(new Coordinate(x, y));
//    }

//    public void UpdateCardAt(Coordinate prev, Coordinate next)
//    {
//        cardsOnBoard.Remove(prev);
//        cardsOnBoard.Add(next);
//    }

//    static public int HandSize() { return handDeck.Count; }
//}