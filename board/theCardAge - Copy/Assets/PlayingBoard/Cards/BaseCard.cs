using UnityEngine;
using System.Collections;

public abstract class BaseCard
{
    protected bool discard;
    protected bool canMove;
    
    public void DiscardCard() { discard = true; }

    public Card linkedBoardCard;

    public void Link(Card toLink)
    {
        linkedBoardCard = toLink;
    }
    public void Unlink()
    {
        linkedBoardCard.Unlink();
        linkedBoardCard = null;
    }

    public virtual bool[,] PossibleMove()
    {
        canMove = false;
        return new bool[Globals.numCols, Globals.numRows];
    }

    public void NotMoved() { canMove = true; }



    // Functions we want to be able to use in PlayingCard
    public virtual string SaveCard() { return "Name"; }
    public virtual void EndTurn() { canMove = true; }
    public virtual void RestoreCard() { discard = false; canMove = true; }
    public virtual void AttackEnemy(BaseCard enemy, int attackToMultiply = 0) { }


    public virtual string GetName() { return "Name"; }
    public virtual string GetImage() { return "Image"; }
    public virtual string getDescription() { return "Description"; }
    public virtual int GetHealth() { return 0; }
    public virtual int GetDefense() { return 0; }
    public virtual int GetAttack() { return 0; }
    public virtual int GetMaxHealth() { return 0; }

    public virtual void SubHealth(int damage) { }
}

