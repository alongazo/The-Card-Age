using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardHighlights : MonoBehaviour
{

    public static BoardHighlights Instance { set; get; }

    public Board grid;

    public GameObject highlightPrefab;
    private List<GameObject> highlights;

    private Quaternion orientation = Quaternion.Euler(90, 0, 0);

    private Color32 gold;
    private Color32 ruby;

    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();

        gold = new Color32(255, 215, 0, 100);
        ruby = new Color32(224, 17, 65, 100);
    }

    private GameObject GetHighlightObject()
    {
        GameObject go = highlights.Find(g => !g.activeSelf);


        if (go == null)
        {
            go = Instantiate(highlightPrefab);
            highlights.Add(go);
        }
        return go;
    }

    public void HighlightAllowedMoves(bool[,] moves, bool isPlayerTurn)
    {
        for (int i = 0; i < Globals.numCols; i++)
        {
            for (int j = 0; j < Globals.numRows; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetHighlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + 0.5f, j + 0.5f, 0);
                    if (grid.cards[i,j] != null && isPlayerTurn != grid.cards[i,j].isWhite)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", ruby);
                    } 
                    else if (grid.cards[i, j] == null)
                    {
                        go.GetComponent<Renderer>().material.SetColor("_Color", gold);
                    }
                    else
                    {
                        go.SetActive(false);
                    }
                }
            }
        }
    }

    public void HideHighlights()
    {
        foreach (GameObject go in highlights)
        {
            go.SetActive(false);
        }
    }
}
