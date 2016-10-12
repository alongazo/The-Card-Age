using UnityEngine;
using System.Collections;

public class Pawn : Card
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        Card c, c2;
        // White team move
        if (isWhite)
        {
            //diagonal left
            if (CurrentX != 0 && CurrentY != 7)
            {
                c = board.Instance.cards[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX - 1, CurrentY + 1] = true;
            }
            //diagonal right
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = board.Instance.cards[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite)
                    r[CurrentX + 1, CurrentY + 1] = true;
            }
           
            //middle
            if (CurrentY != 7)
            {
                c = board.Instance.cards[CurrentX, CurrentY + 1];
                if (c==null)
                
                    r[CurrentX, CurrentY + 1] = true;
                
            }
            //middle on first move
            if (CurrentY == 1)
            {
                c = board.Instance.cards[CurrentX, CurrentY + 1];
                c2 = board.Instance.cards[CurrentX, CurrentY + 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY + 2] = true;
                }
            }
        }
        //Black
        else
        {
            //diagonal left
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = board.Instance.cards[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;
            }
            //diagonal right
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = board.Instance.cards[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }

            //middle
            if (CurrentY != 0)
            {
                c = board.Instance.cards[CurrentX, CurrentY - 1];
                if (c == null)

                    r[CurrentX, CurrentY - 1] = true;

            }
            //middle on first move
            if (CurrentY == 6)
            {
                c = board.Instance.cards[CurrentX, CurrentY - 1];
                c2 = board.Instance.cards[CurrentX, CurrentY - 2];
                if (c == null && c2 == null)
                {
                    r[CurrentX, CurrentY - 2] = true;
                }
            }
        }
        return r;
    }

}
