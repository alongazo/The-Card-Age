using UnityEngine;
using System.Collections;

public abstract class BaseCard
{
    public bool discard;
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
        Debug.Log("Is calling the PossibleMove function of BaseCard");
        return new bool[Globals.numCols, Globals.numRows];
    }




    // Functions we want to be able to use in PlayingCard
    public virtual string SaveCard() { return "Name"; }
    public virtual void RestoreCard() { discard = false; }
    public virtual void AttackEnemy(BaseCard enemy, int attackToMultiply = 0) { }


    public virtual string GetName() { return "Name"; }
    public virtual string GetImage() { return "Image"; }
    public virtual string GetDescription() { return "Description"; }
    public virtual CardType GetCardType() { return CardType.Fake; }
    public virtual int GetHealth() { return 0; }
    public virtual int GetDefense() { return 0; }
    public virtual int GetAttack() { return 0; }
    public virtual int GetMaxHealth() { return 0; }
    public virtual int GetMovement() { return 0; }
    public virtual string GetStatus() { return ""; }
    public virtual bool IsPlayer() { return false; }

    public virtual void SubHealth(int damage) { }
}

