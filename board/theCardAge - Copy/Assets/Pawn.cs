//using UnityEngine;
//using System.Collections;

//public class Pawn : Card
//{
//    public Pawn()
//    {
//        strength = 20;
//        defense = 12345;
//    }
//    public override string Description()
//    {
//        string text = "Hello I am a 9. I am greater than 8 and less than 10.";
//        return text;
//    }
//    public override bool[,] PossibleMove()
//    {
//        bool[,] r = new bool[6, 6];
//        Card c, c2;
//        // White team move
//        if (isWhite)
//        {
//            //diagonal left
//            if (CurrentX != 0 && CurrentY != 7)
//            {
//                c = Board.Instance.cards[CurrentX - 1, CurrentY + 1];
//                if (c != null && !c.isWhite)
//                    r[CurrentX - 1, CurrentY + 1] = true;
//            }
//            //diagonal right
//            if (CurrentX != 7 && CurrentY != 7)
//            {
//                c = Board.Instance.cards[CurrentX + 1, CurrentY + 1];
//                if (c != null && !c.isWhite)
//                    r[CurrentX + 1, CurrentY + 1] = true;
//            }
           
//            //middle
//            if (CurrentY != 7)
//            {
//                c = Board.Instance.cards[CurrentX, CurrentY + 1];
//                if (c==null)
                
//                    r[CurrentX, CurrentY + 1] = true;
                
//            }
//            //middle on first move
//            if (CurrentY == 1)
//            {
//                c = Board.Instance.cards[CurrentX, CurrentY + 1];
//                c2 = Board.Instance.cards[CurrentX, CurrentY + 2];
//                if (c == null && c2 == null)
//                {
//                    r[CurrentX, CurrentY + 2] = true;
//                }
//            }
//        }
//        //Black
//        else
//        {
//            //diagonal left
//            if (CurrentX != 0 && CurrentY != 0)
//            {
//                c = Board.Instance.cards[CurrentX - 1, CurrentY - 1];
//                if (c != null && c.isWhite)
//                    r[CurrentX - 1, CurrentY - 1] = true;
//            }
//            //diagonal right
//            if (CurrentX != 7 && CurrentY != 0)
//            {
//                c = Board.Instance.cards[CurrentX + 1, CurrentY - 1];
//                if (c != null && c.isWhite)
//                    r[CurrentX + 1, CurrentY - 1] = true;
//            }

//            //middle
//            if (CurrentY != 0)
//            {
//                c = Board.Instance.cards[CurrentX, CurrentY - 1];
//                if (c == null)

//                    r[CurrentX, CurrentY - 1] = true;

//            }
//            //middle on first move
//            if (CurrentY == 6)
//            {
//                c = Board.Instance.cards[CurrentX, CurrentY - 1];
//                c2 = Board.Instance.cards[CurrentX, CurrentY - 2];
//                if (c == null && c2 == null)
//                {
//                    r[CurrentX, CurrentY - 2] = true;
//                }
//            }
//        }
//        return r;
//    }

//}
