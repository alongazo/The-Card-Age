using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public dropzone handPlace;
    public Board board;
    public Text deckCount;

    public string bossName;
    public TextAsset deckInfo;

    private bool firstTurn;

    GameObject wholeDeck;
    List<PlayingCard> handDeck;
    string selectedAction;

    bool isMovingCard;
    static bool canSkill; public static void SetCanSkill(bool truth) { canSkill = truth; }
    public static bool doubleSummon;

    Dictionary<Coordinate, int> cardsOnBoard; // 0 means player, 1 means enemy

    void Start()
    {
        string[] deck_text = deckInfo.text.Split('\n');
        wholeDeck = new GameObject();
        wholeDeck.AddComponent<Deck>();
        wholeDeck.GetComponent<Deck>().Initialize(deck_text, false);
        handDeck = new List<PlayingCard>();
        isMovingCard = false;
        cardsOnBoard = new Dictionary<Coordinate, int>();
        selectedAction = "";
        Initialize();
        board.EndTurn(); // required because for some reason, board.IsWhiteTurn() returns false right after initiation...
        canSkill = true;
        doubleSummon = false;
    }

    void Initialize()
    {
        FirstDraw();
        PlaceFirstCard();
        isMovingCard = false;
    }

    void Update()
    {
        if (isMovingCard && !board.IsWhiteTurn())
        {
            // animation in here
            isMovingCard = false; // will change to not be immediate later on
            board.EndTurn();
        }
        else if (!board.IsWhiteTurn())
        {

            // time for the enemy to make a move
            DrawCard(false);

            // decide how many actions to do
            int numMoves = Random.Range(2, 6);

            Debug.Log(numMoves);
            while (numMoves > 0)
            {
                numMoves--;
                Debug.Log(numMoves);
                if (handDeck.Count > 0)
                {
                    // dumb thing: summons all the things it can and then moves them
                    int index = GetCardOfType(CardType.Minion);
                    if (index == -1) // if there's no more minion cards...
                    {
                        // if there's no minion card, then the enemy has two choices: move a card or play a skill card
                        if (Random.Range(0, 5) < 5) // has a 2/3rds chance to move over playing a skill card
                        {
                            //Debug.Log("Going to move a card");
                        }
                        else
                        {
                            index = GetCardOfType(CardType.Skill);
                            if (index > -1) // if there's a skill card, USE IT 
                            {
                                // (except I haven't implemented anything for skill cards, so don't do anything yet)
                                //Debug.Log("Going to use a skill");
                            }
                        }
                    }
                    else // if there's a minion card, PUT IT DOWN
                    {
                        //Debug.Log("Going to put down a minion");
                        PlaceCard(index);
                        numMoves--;
                        isMovingCard = true;
                    }
                }
                else
                {
                    DrawCard(true);
                    break;
                }
            }
            isMovingCard = true;
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
            if (again)
            {
                board.EndTurn();
            }
            deckCount.text = wholeDeck.GetComponent<Deck>().SizeOFDeck().ToString();
        }
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

        GameObject newDrag = new GameObject("Enemy Card " + handDeck.Count.ToString());
        newDrag.AddComponent<Image>();
        newDrag.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Card_Images/aback.png");
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


    void PlaceFirstCard()
    {
        GameObject newDrag = handPlace.transform.GetChild(0).gameObject;
        newDrag.GetComponent<drag>().OnEndDrag(2, 4);
        board.enemyBoss = new Coordinate(2, 4);
    }

    void PlaceCard(int index)
    {
        GameObject newDrag = handPlace.transform.GetChild(index).gameObject;
        int col, row;
        do
        {
            col = Random.Range(0, Globals.numCols);
            row = Random.Range(Globals.numRows - 2, Globals.numRows);
        } while (!newDrag.GetComponent<drag>().OnEndDrag(col, row));
        cardsOnBoard.Add(new Coordinate(col,row),1);
    }


    int GetCardOfType(CardType cardtype)
    {
        List<int> indexes = new List<int>();
        for (int i=0; i<handDeck.Count; i++)
        {
            if (handDeck[i].GetCardType() == cardtype)
            {
                //Debug.Log("Card " + handDeck[i].GetName() + " is type " + handDeck[i].GetCardType().ToString());
                indexes.Add(i);
            }
        }
        if (indexes.Count > 0)
        {
            return indexes[Random.Range(0, indexes.Count)];
        }
        return -1;
    }
}