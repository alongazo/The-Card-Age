using UnityEngine;
using System.Collections;

public class BossCard : PlayingCard
{
    int ap, baseAP;
    bool endOfTurn;

    // Not really important
    int level;
    int experience;

    public BossCard(string[] initInfo, CardType type, int[] stats)
    : base(initInfo, type)
    {
        base.LoadCard(stats);
        actions.Insert(2, "Defend");
        ap = baseAP = stats[1];
        endOfTurn = false;
    }

    public void Defend()
    {
        defense *= 2;
    }

    public override void RestoreCard()
    {
        base.RestoreCard();
        ap = baseAP;
    }

    // Not really important
    public void Equip(bool forAttack, int changeby)
    {
        if (forAttack) { ModifyAttack(changeby); }
        else { defense += changeby; }
    }
    public void RecieveEXP(int newExperience)
    {
        float factor = 0.89F + level*0.01F;
        experience += Mathf.CeilToInt(newExperience / level); 
    }
}