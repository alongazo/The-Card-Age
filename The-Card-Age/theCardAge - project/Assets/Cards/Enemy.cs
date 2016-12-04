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
    public static bool CanSkill() { return canSkill; }
    public static bool doubleSummon;
    public static bool surroundDamage;
    int surroundTurnCount;

    int numMoves;

    List<Coordinate> cardsOnBoard = new List<Coordinate>();
    List<Coordinate> myCardPositions;

    void Start()
    {
        //Debug.Log("Enemy start is first");
        string[] deck_text = deckInfo.text.Split('\n');
        wholeDeck = new GameObject();
        wholeDeck.AddComponent<Deck>();
        wholeDeck.GetComponent<Deck>().Initialize(deck_text, false);
        handDeck = new List<PlayingCard>();
        isMovingCard = false;
        selectedAction = "";
        Initialize();
        board.EndTurn(); // required because for some reason, board.IsWhiteTurn() returns false right after initiation...
        canSkill = true;
        doubleSummon = false;
        numMoves = 0;
    }

    void Initialize()
    {
        FirstDraw();
        PlaceFirstCard();
        isMovingCard = true;
    }

    void Update()
    {
        if (isMovingCard && !board.IsWhiteTurn())
        {
            //////Debug.Log("Not going to the actual placement stuff");
            // animation in here
            isMovingCard = board.CardIsAttacking(); // will change to not be immediate later on
            //////Debug.Log("isMovingCard = " + isMovingCard);
            if (!isMovingCard && numMoves == 1)
            {
                board.EndTurn();
            }
        }
        else if (!board.IsWhiteTurn())
        {
            //////Debug.Log("Not doing the right thing >o<");
            if (numMoves == 0)
            {
                // time for the enemy to make a move
                DrawCard(false);

                // decide how many actions to do
                numMoves = Random.Range(2, 6);
            }
            else
            {
                numMoves--;
                ////Debug.Log(numMoves);
                if (handDeck.Count > 0)
                {
                    // dumb thing: summons all the things it can and then moves them
                    int index = GetCardOfType(CardType.Minion);
                    if (index == -1) // if there's no more minion cards...
                    {
                        // if there's no minion card, then the enemy has two choices: move a card or play a skill card
                        if (Random.Range(0, 5) < 5) // has a 2/3rds chance to move over playing a skill card
                        {
                            //////Debug.Log("Going to move a card");
                            List<Coordinate> attackers = new List<Coordinate>();
                            List<Coordinate> targets = new List<Coordinate>();
                            FindPositionOfTargets(ref attackers, ref targets);
                            if (attackers.Count > 0)
                            {
                                int i = Random.Range(0, attackers.Count);
                                board.SelectCard(attackers[i]);
                                board.MoveCard(targets[i].col, targets[i].row);
                                ////Debug.Log("Attacker at (" + attackers[i].col + "," + attackers[i].row + ")");
                                ////Debug.Log("Target at (" + targets[i].col + "," + targets[i].row + ")");
                                isMovingCard = true;
                                attackers.RemoveAt(i);
                                targets.RemoveAt(i);
                            }
                        }
                        // commenting this out so it doesn't break your code!!!
                        //else
                        //{
                        //    index = GetCardOfType(CardType.Skill);
                        //    Debug.Log("Index of Skill card is " + index);
                        //    if (index > -1) // if there's a skill card, USE IT 
                        //    {
                        //        // Need to get a target
                        //        List<Coordinate> playerPositions = board.GetPlayerPositions();
                        //        if (playerPositions != null)
                        //        {
                        //            Coordinate targetCoordinate = playerPositions[Random.Range(0, playerPositions.Count)];
                        //            Debug.Log("Target at (" + targetCoordinate.col + "," + targetCoordinate.row + ") out of " + playerPositions.Count + "targets");



                        //            // Need to put the card onto the target (remember, it's not on the board...)
                        //            GameObject newDrag = handPlace.transform.GetChild(index).gameObject;
                        //            Debug.Log("Using skill " + handDeck[index].GetName());
                        //            newDrag.GetComponent<drag>().OnSkillDrag(targetCoordinate.col, targetCoordinate.row);
                        //        }
                        //    }
                        //}
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
                    Debug.Log("Enemy draws card");
                    DrawCard(true);
                }

                if (!isMovingCard && numMoves == 0) { board.EndTurn(); }
            }
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
        newDrag.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Card_Images/" + card.GetImage());//"Assets/Card_Images/aback.png");
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
        board.SetBossCardLocation(new Coordinate(2, 4), false);
        cardsOnBoard.Add(new Coordinate(2, 4));
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
        cardsOnBoard.Add(new Coordinate(col, row));
    }


    int GetCardOfType(CardType cardtype)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < handDeck.Count; i++)
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


    void FindPositionOfTargets(ref List<Coordinate> attackers, ref List<Coordinate> targets)
    {

        foreach (Coordinate point in cardsOnBoard)
        {
            if (!board.cards[point.col, point.row].HasTakenAction())
            {
                if (point.row + 1 < Globals.numRows && board.cards[point.col, point.row + 1] != null && board.cards[point.col, point.row + 1].isWhite)
                {
                    attackers.Add(point);
                    targets.Add(new Coordinate(point.col, point.row + 1));
                }
                if (point.row - 1 >= 0 && board.cards[point.col, point.row - 1] != null && board.cards[point.col, point.row - 1].isWhite)
                {
                    attackers.Add(point);
                    targets.Add(new Coordinate(point.col, point.row - 1));
                }
                if (point.col + 1 < Globals.numCols && board.cards[point.col + 1, point.row] != null && board.cards[point.col + 1, point.row].isWhite)
                {
                    attackers.Add(point);
                    targets.Add(new Coordinate(point.col + 1, point.row));
                }
                if (point.col - 1 >= 0 && board.cards[point.col - 1, point.row] != null && board.cards[point.col - 1, point.row].isWhite)
                {
                    attackers.Add(point);
                    targets.Add(new Coordinate(point.col - 1, point.row));
                }
            }
        }
    }


    public void ResetTurn()
    {
        foreach (Coordinate card in cardsOnBoard)
        {
            board.cards[card.col, card.row].ResetTurn();
        }
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


    void RandomSummon()
    {
        int prefabIndex = board.FindPrefabIndex("WyvernPrefab");
        GameObject newDrag = new GameObject("Enemy Card " + handDeck.Count.ToString());
        newDrag.AddComponent<drag>();
        newDrag.GetComponent<drag>().setItemIndex(prefabIndex);
        newDrag.GetComponent<drag>().setCard(Globals.cardDatabase["Wyvern"]);
        newDrag.GetComponent<drag>().setOriginator(ref handDeck);
        newDrag.GetComponent<drag>().boardScript = board;
        int col, row;
        do
        {
            col = Random.Range(0, Globals.numCols);
            row = Random.Range(Globals.numRows - 2, Globals.numRows);
        } while (!newDrag.GetComponent<drag>().OnEndDrag(col, row));
    }
}