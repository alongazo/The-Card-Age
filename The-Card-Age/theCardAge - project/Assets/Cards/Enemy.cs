using UnityEngine;
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

    bool isMovingCard;
    static bool canSkill; public static void SetCanSkill(bool truth) { canSkill = truth; }
    public static bool CanSkill() { return canSkill; }
    public static bool doubleSummon;
    public static bool surroundDamage;
    int surroundTurnCount;

    int numMoves;

    List<Coordinate> cardsOnBoard = new List<Coordinate>();

    void Start()
    {
        //Debug.Log("Enemy start is first");
        string[] deck_text = deckInfo.text.Split('\n');
        wholeDeck = new GameObject();
        wholeDeck.AddComponent<Deck>();
        wholeDeck.GetComponent<Deck>().Initialize(deck_text, false);
        handDeck = new List<PlayingCard>();
        isMovingCard = false;
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
            // animation in here
            isMovingCard = board.CardIsAttacking(); // will change to not be immediate later on
            if (!isMovingCard && numMoves == 1)
            {
                board.EndTurn();
                return;
            }
            bool noMoreMoves = true;
            foreach (Coordinate activeCard in cardsOnBoard)
            {
                if (!board.cards[activeCard.col,activeCard.row].HasTakenAction())
                {
                    noMoreMoves = false;
                }
            }
            if (noMoreMoves)
            {
                board.EndTurn();
            }
        }
        else if (!board.IsWhiteTurn())
        {
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
                        //if there's no minion card, then the enemy has two choices: move a card or play a skill card
                        if (Random.Range(0, 5) < 5) // has a 1/5rds chance to move over playing a skill card
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
                                isMovingCard = true;
                                attackers.RemoveAt(i);
                                targets.RemoveAt(i);
                            }
                            else
                            {
                                Coordinate randCard = cardsOnBoard[Random.Range(0, cardsOnBoard.Count)];
                                board.SelectCard(randCard);
                                if (randCard.row > 0 && board.cards[randCard.col, randCard.row - 1] == null)
                                {
                                    board.MoveCard(randCard.col, randCard.row - 1);
                                } else if (randCard.col > 0 && board.cards[randCard.col - 1,randCard.row] == null) {
                                    board.MoveCard(randCard.col - 1, randCard.row);
                                } else if (randCard.col < Globals.numCols-1 && board.cards[randCard.col+1,randCard.row] == null)
                                {
                                    board.MoveCard(randCard.col + 1, randCard.row);
                                }
                                isMovingCard = true;
                            }
                        }
                        else
                        {
                            index = GetCardOfType(CardType.Skill);
                            if (index > -1) // if there's a skill card, USE IT 
                            {
                                // Need to get a target
                                List<Coordinate> playerPositions = board.GetPlayerPositions();
                                if (playerPositions != null)
                                {
                                    Coordinate targetCoordinate;
                                    if (handDeck[index].IsForPlayer())
                                    {
                                        targetCoordinate = playerPositions[Random.Range(0, playerPositions.Count)];
                                    }
                                    else
                                    {
                                        targetCoordinate = cardsOnBoard[Random.Range(0, cardsOnBoard.Count)];
                                    }

                                    // Need to put the card onto the target (remember, it's not on the board...)
                                    GameObject newDrag = handPlace.transform.GetChild(index).gameObject;
                                    newDrag.GetComponent<drag>().OnSkillDrag(targetCoordinate.col, targetCoordinate.row);
                                    numMoves--;
                                }
                            }
                        }
                    }
                    else // if there's a minion card, PUT IT DOWN
                    {
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

    string ToTitleCase(string input)
    {
        return input[0].ToString().ToUpper() + input.Substring(1);
    }

    void SetCardToHand(PlayingCard card)
    {
        // Trying to add this to a drag object?
        string prefabName = ToTitleCase(card.GetName().Replace(" ", "")) + "Prefab";
        int prefabIndex = board.FindPrefabIndex(prefabName);
        // want to add the required components to here:
        //  Drag (script) with TypeOfItem = Weapon and ItemIndex = prefabIndex

        GameObject newDrag = new GameObject("Enemy Card " + handDeck.Count.ToString());
        newDrag.AddComponent<Image>();
        newDrag.GetComponent<Image>().sprite = Resources.Load("Card_Images/" + card.GetName(), typeof(Sprite)) as Sprite;//"Assets/Card_Images/aback.png");
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
        newDrag.GetComponent<drag>().setCard(Globals.cardDatabase["wyvern"]);
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