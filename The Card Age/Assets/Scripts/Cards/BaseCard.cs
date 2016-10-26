using UnityEngine;
using System.Collections.Generic;

public class BaseCard {

    string cardName;
    string affiliation;
    int rank;
    int cost;

    protected List<string> actions;
    protected bool discard;

    public BaseCard(string newName, string newAffiliation, int newRank, int newCost)
    {
        cardName = newName;
        affiliation = newAffiliation;
        rank = newRank;
        cost = newCost;
        discard = false;

        actions.Add("Discard");
    }

    public string GetName() { return cardName; }
    public string GetAffiliation() { return affiliation; }
    public int GetRank() { return rank; }
    public int GetCost() { return cost; }
    public bool GetIfDiscard() { return discard; }

    protected void DiscardCard() { discard = true; }
    
    public virtual void RestoreCard() { discard = false; }
}
