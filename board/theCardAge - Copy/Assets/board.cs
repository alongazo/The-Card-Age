using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class board : MonoBehaviour
{
    private static int _row = 5;
    private static int _col = 10;
    public static board Instance { set; get; }
    private bool[,] allowedMoves { set; get; }
    public Card[,] cards { set; get; }
    private Card selectedCard;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = TILE_SIZE / 2;
    public int selectionX = -1;
    public int selectionY = -1;

    private Quaternion orientation = Quaternion.Euler(-90, 180, 0);

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeCard = new List<GameObject>();


    public bool isWhiteTurn = true;
	// Use this for initialization
	void Start ()
    {
        Instance = this;
        //SpawnAllCards();
        activeCard = new List<GameObject>();
        cards = new Card[_row, _col];
    }

    // Update is called once per frame
    void Update () {
        updateSelection();
        _DrawDebug();


        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("hello");
            if (selectionX > -1 && selectionY > -1)
            {
                Vector3 temp = GetTileCenter(selectionX, selectionY);
                if (selectedCard == null)
                {
                    //Debug.Log("select");
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
	}
    private void SelectCard(int x, int y)
    {
        if (cards[x,y] == null)
        {
            return;
        }
        if (cards[x,y].isWhite != isWhiteTurn)
        {
            return;
        }
        allowedMoves = cards[x, y].PossibleMove();
        selectedCard = cards[x, y];
        Debug.Log(selectedCard.name);
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
    }
    private void MoveCard(int x, int y)
    {
        if (allowedMoves[x,y])
        {
            Card c = cards[x, y];
            if (c != null && c.isWhite != isWhiteTurn)
            {
                if (c.GetType() == typeof(King))
                {
                    return;
                }
                activeCard.Remove(c.gameObject);
                Destroy(c.gameObject);
            }
            cards[selectedCard.CurrentX, selectedCard.CurrentY] = null;
            selectedCard.transform.position = GetTileCenter(x, y);
            selectedCard.SetPosition(x, y);
            cards[x, y] = selectedCard;
            isWhiteTurn = !isWhiteTurn;
        }
        BoardHighlights.Instance.HideHighlights();
        selectedCard = null;
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
        cards[x, y].SetPosition(x, y);
        activeCard.Add(go);
    }
    //private void SpawnAllCards()
    //{
    //    activeCard = new List<GameObject>();
    //    Cards = new Card[_row, _col];

    //    //king
    //    SpawnCard(0, 3, 0);
    //    //queen
    //    SpawnCard(1, 4, 0);

    //    //rook
    //    SpawnCard(2, 0, 0);
    //    SpawnCard(2, 7, 0);
    //    //bishop
    //    SpawnCard(3, 2, 0);
    //    SpawnCard(3, 5, 0);

    //    //knight
    //    SpawnCard(4, 1, 0);
    //    SpawnCard(4, 6, 0);

    //    //pawns
    //    for (int i = 0; i < 8; i++)
    //    {
    //        SpawnCard(5, i, 1);
    //    }
    //    //Black Team

    //    //king
    //    SpawnCard(6, 4, 7);
    //    //queen             
    //    SpawnCard(7, 3, 7);
                            
    //    //rook              
    //    SpawnCard(8, 0, 7);
    //    SpawnCard(8, 7, 7);
    //    //bishop            
    //    SpawnCard(9, 2, 7);
    //    SpawnCard(9, 5, 7);
                            
    //    //knight            
    //    SpawnCard(10, 1, 7);
    //    SpawnCard(10, 6, 7);

    //    //pawns
    //    for (int i = 0; i < 8; i++)
    //    {
    //        SpawnCard(11, i, 6);
    //    }


    //}
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
