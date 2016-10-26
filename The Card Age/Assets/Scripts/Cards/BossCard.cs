using UnityEngine;
using System.Collections;

public class BossCard : PlayingCard
{
    int ap, baseAP;
    //bool endOfTurn;

    // Not really important
    int level;
    int experience;

    public BossCard(string[] initInfo, CardType type, int[] stats)
    : base(initInfo, type)
    {
        base.LoadCard(stats);
        actions.Insert(2, "Defend");
        ap = baseAP = stats[1];
        //endOfTurn = false;

        if (stats.Length > 7)
        {
            level = stats[7];
            experience = stats[8];
        }
        else {
            level = 1;
            experience = 0;
        }
    }

    public bool UseSkill(int cost)
    {
        if (ap > 0)
        {
            ap = Mathf.Max(0, ap - cost);
        }
        return ap == 0;
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
        experience += Mathf.CeilToInt(newExperience / (0.89F + level * 0.01F)); 
    }
}