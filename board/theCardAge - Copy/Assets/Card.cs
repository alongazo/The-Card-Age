using UnityEngine;
using System.Collections;

public abstract class Card : MonoBehaviour {
    private int row = 8;
    private int col = 8;
    public int CurrentX { set; get; }
    public int CurrentY { set; get; }

    public bool isWhite;

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }
    
    public virtual bool[,] PossibleMove()
    {
        return new bool[8,8];
    }
}
