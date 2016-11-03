using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Board : MonoBehaviour
{

    [SerializeField]
    private LeftPanel leftPanelScriptPlayer;
    [SerializeField]
    private LeftPanel leftPanelScriptEnemy;

    [SerializeField]
    private RightPanel rightPanelScript;

    private static int _row = Globals.numRows;
    private static int _col = Globals.numCols;
    public static Board Instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    public Card[,] cards { set; get; }
    private Card selectedCard;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = TILE_SIZE / 2;
    public int selectionX = -1;
    public int selectionY = -1;

    private Quaternion orientation = Quaternion.Euler(-90, 180, 0);

    public List<GameObject> chessmanPrefabs;
    public List<GameObject> activeCard = new List<GameObject>();

    
    private bool isAttacking = false;
    private bool doneAttacking = false;
    private float startAttackTime;
    private Vector3 attackCardPosition;
    private Vector3 targetCardPosition;
    private float totalDistance;
    
    //[SerializeField]
    private bool isWhiteTurn = true;
    // Use this for initialization
    void Start()
    {

        Instance = this;
        activeCard = new List<GameObject>();
        cards = new Card[_col, _row];
        //SpawnDebug();
    }

    // Update is called once per frame
    void Update () {
        updateSelection();
        _DrawDebug();
        // card attack animation / card move toward target
        
        if (isAttacking == true && doneAttacking == false)
        {
            //Debug.Log("Hello we are attacking");
            float currentTime = (Time.time - startAttackTime);
            float journeyFraction = currentTime / totalDistance;
            //Debug.Log(currentTime + " " + totalDistance + " " + journeyFraction);
            int speed = 8;
            if (journeyFraction*speed > 0.6f)
            {
                
                startAttackTime = Time.time;
                targetCardPosition = attackCardPosition;
                attackCardPosition = selectedCard.transform.position;
                totalDistance =Vector3.Distance(attackCardPosition, targetCardPosition);
                doneAttacking = true;

            }
            else
            {
                selectedCard.transform.position = Vector3.Lerp(attackCardPosition, targetCardPosition, journeyFraction*speed);
            }

        }
        // card done attack animation / card return back to original position
        else if (doneAttacking == true)
        {
            float currentTime = (Time.time - startAttackTime);
            float journeyFraction = currentTime / totalDistance;
            selectedCard.transform.position = Vector3.Lerp(attackCardPosition, targetCardPosition, journeyFraction*2);
            if (selectedCard.transform.position == targetCardPosition)
            {
                doneAttacking = false;
                isAttacking = false;
            }
        }

        // left click
        else if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left click pressed");
            Debug.Log("Is my turn: " + isWhiteTurn);//" + cards[x, y].isWhite + " " + isWhiteTurn);
            //Debug.Log("hello");
            if (selectionX > -1 && selectionY > -1)
            {
                //Vector3 temp = GetTileCenter(selectionX, selectionY);
                //Debug.Log(selectionX + " " + selectionY);
                if (selectedCard == null)
                {
                    //Debug.Log("null");
                    //Debug.Log(selectionX + "  " + selectionY);
                    SelectCard(selectionX, selectionY);
                }
                else
                {
                    //Debug.Log("move");
                    MoveCard(selectionX, selectionY);
                    SelectCard(selectionX, selectionY);
                }
            }
        }

        // right click (show card information)
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Right click pressed");
            //Debug.Log("Is my turn: " + isWhiteTurn);//" + cards[x, y].isWhite + " " + isWhiteTurn);
            if (selectionX > -1 && selectionY > -1)
            {
                //Debug.Log("x, y: " + selectionX.ToString() + " " + selectionY.ToString());

                RightSelectCard(selectionX, selectionY);

            }
        }
    }
    private void SelectCard(int x, int y)
    {
        //Debug.Log("In Select Card " + selectionX + "  " + selectionY);
        //Debug.Log("Does card exist: " + (cards[x, y] != null).ToString());
        if (cards[x, y] == null)
        {
            return;
        }
        if (cards[x, y].isWhite != isWhiteTurn)
        {
            return;
        }
        allowedMoves = cards[x, y].PossibleMove();
        selectedCard = cards[x, y];
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }
    private void RightSelectCard(int x, int y)
        // opens left panel
    {
        Debug.Log("In RightSelectCard function");
        rightPanelScript.ViewCardStat(cards[x, y]);
    }

    private void MoveCard(int x, int y)
    {
        //Debug.Log(x + " " + y);
        if (allowedMoves[x,y])
        {
            Card c = cards[x, y];
            if (c != null && c.isWhite != isWhiteTurn)
            {
                Debug.Log("attack");
                // set variable for card attack animation
                attackCardPosition = selectedCard.transform.position;
                targetCardPosition = GetTileCenter(x,y);
                startAttackTime = Time.time;
                totalDistance = Vector3.Distance(attackCardPosition, targetCardPosition);
                //Debug.Log(totalDistance);
                isAttacking = true;

                // destroy card "not done yet" currently card get destroyed before attack. 
                // I will eventually destroy the card after attack when card class is completed
                c.Attack(selectedCard);
                if (c.GetHPVal() <= 0)
                // destroy object and the health bar at the left side if the card health goes down to 0
                {
                    activeCard.Remove(c.gameObject);
                    Destroy(c.GetHPBar());
                    Destroy(c.gameObject);
                }

            }
            else
            {
                Debug.Log("move is allowed");
                cards[selectedCard.CurrentX, selectedCard.CurrentY] = null;
                selectedCard.transform.position = GetTileCenter(x, y);
                selectedCard.SetPosition(x, y);
                cards[x, y] = selectedCard;
                isWhiteTurn = !isWhiteTurn;
            }
        }
        BoardHighlights.Instance.HideHighlights();
        if (isAttacking == false)
        {
            selectedCard = null;
        }
    }
    private void updateSelection()
    {
        if (!Camera.main)
        {
            return;
        }
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f,LayerMask.GetMask("ChessPlane")))
        {
            //Debug.Log(hit.point.x + " " + hit.point.z);
            selectionX = (int) hit.point.x;
            selectionY = (int) hit.point.z;
        }
        else
        {
            //Debug.Log(hit.point.x+ " " +hit.point.z);
            selectionX = -1;
            selectionY = -1;
        }
    }
    public void Spawn(int index, int x, int y)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x,y), orientation) as GameObject;
        go.transform.SetParent(transform);
        cards[x, y] = go.GetComponent<Card>();
        //Debug.Log("Setting card to position (" + x.ToString() + "," + y.ToString() + ")");
        cards[x, y].SetPosition(x, y);
        if (cards[x, y].isWhite)
            cards[x, y].Link(Globals.cardDatabase["Angel"]);
        else
            cards[x, y].Link(Globals.cardDatabase["Pixie"]);
        //Debug.Log(cards[x, y].IsLinked());
        activeCard.Add(go);
        
        if (cards[x, y].isWhite)
        {
            leftPanelScriptPlayer.CreateHPBar(cards[x, y]);
        }
        else if (!cards[x,y].isWhite)
        {
            leftPanelScriptEnemy.CreateHPBar(cards[x, y]);
        }
    }
    private void SpawnDebug()
    {
        Spawn(11, 3, 3);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }
    private void _DrawDebug()
    {
        Vector3 widthLine = Vector3.right * _col;
        Vector3 heightLine = Vector3.forward * _row;

        for (int i = 0; i <= _row; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);

            for (int j = 0; j <= _col; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }

        }

        // Draw selection on board
        if (selectionX > -1 && selectionY > -1)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }
}
