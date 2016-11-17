using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CardType { Boss, Minion, Skill };

public class PlayingCard : BaseCard
{
    // All member variables
    protected string cardName;
    protected string affiliation;
    protected string image;
    protected string description;

    CardType cardType;
    int rank;
    int cost;
    int move;

    protected int attack, baseAttack;
    public int attackMultiplier = 2;
    protected int health, baseHealth;
    public int healthMultiplier = 2;
    protected int defense, baseDefense;
    public int increaseDefense = 2;
    protected string status;
    public int sacrificeAmt = 5;

    protected List<string> actions;

 
    // Initialization
    public PlayingCard(string[] initInfo, CardType type)
    {
        cardName = initInfo[0];
        affiliation = initInfo[1];
        image = initInfo[2];
        //Debug.Log("Image for " + cardName + " is " + image);
        description = initInfo[3];

        cardType = type;
        discard = false;

        actions = new List<string>();
    }
    public PlayingCard(PlayingCard toCopy)
    {
        cardName = toCopy.cardName; affiliation = toCopy.affiliation;
        image = toCopy.image; description = toCopy.description;
        cardType = toCopy.cardType; rank = toCopy.rank; cost = toCopy.cost;
        move = toCopy.move;
        attack = toCopy.attack; baseAttack = toCopy.baseAttack;
        health = toCopy.health; baseHealth = toCopy.baseHealth;
        defense = toCopy.defense; baseDefense = toCopy.baseDefense;
        actions = toCopy.actions;
        discard = toCopy.discard;
        status = toCopy.status;
    }
    public void LoadCard(int[] info, string status = "None")
    {
        rank = info[0]; cost = info[1]; move = info[2];
        attack = baseAttack = info[3];
        health = baseHealth = info[4];
        defense = baseDefense = info[5];

        if (cardType == CardType.Minion || cardType == CardType.Boss)
        {
            actions.Add("Attack"); actions.Add("Move");
        }
        this.status = status;
        actions.Add("Discard");
    }


    // Helper functions 
    int CalculateDamage(int enemyDefense, int attackToMultiply)
    {
        int damage = 0;
        if (cardType == CardType.Skill)
        {
            damage = Mathf.CeilToInt(attackToMultiply * (attack / 10));
        }
        else {
            int changeBy = Random.Range(-2, 3);
            if (attack + changeBy > enemyDefense)
            {
                damage = attack + changeBy;
            }
        }
        return damage;
    }
    protected void ModifyAttack(int changeby) { attack += changeby; }

    // Overwrite the functions in BaseCard
    public override bool[,] PossibleMove()
    {
        //Debug.Log("Is calling the PossibleMove function of PlayingCard");
        bool[,] moves = new bool[Globals.numCols, Globals.numRows];
        int x = linkedBoardCard.CurrentX, y = linkedBoardCard.CurrentY;
        for (int spread = 1; spread-1 < move; spread++)
        {
            if (y+spread < Globals.numRows)
                moves[x, y + spread] = true;
            if (y-spread > -1)
                moves[x, y - spread] = true;
            if (x+spread < Globals.numCols)
                moves[x + spread, y] = true;
            if (x-spread > -1)
                moves[x - spread, y] = true;

            // For right now, we're going to assume that everyone moves 1 tile at a time,
            //   but later on, maybe I could implement something with a more Fire Emblem-y style movement thing
            // Check out: http://gamedev.stackexchange.com/questions/61928/finding-possible-moves-for-an-entity-in-a-2d-tiled-game
            //            http://gamedev.stackexchange.com/questions/46681/how-to-make-my-characters-turn-smoothy-while-walking-on-a-pathlist-of-coordinat
        }
        return moves;
    }


    public override string SaveCard()
    {
        string toSave = cardName;
        int[] info = { rank, cost, move, baseAttack, baseHealth, baseDefense };
        return toSave + ":" + info.ToString() + ":" + status;
    }
    public override void RestoreCard()
    {
        discard = false;
        attack = baseAttack;
        health = baseHealth;
        defense = baseDefense;
        if (cardType != CardType.Skill)
        {
            status = "None";
        }
    }
    public override void AttackEnemy(BaseCard enemy, int attackToMultiply = 0)
    {
        int damage = CalculateDamage(enemy.GetDefense(), attackToMultiply);
        if (enemy.GetHealth() < damage) { DiscardCard(); }
        else { enemy.SubHealth(damage); }
    }


    public override string GetName() { return cardName; }
    public override string GetImage() { return image; }
    public override string GetDescription() { return description; }
    public override int GetHealth() { return health; }
    public override int GetDefense() { return defense; }
    public override int GetAttack() { return attack; }
    public override int GetMaxHealth() { return baseHealth;  }
    public override int GetMovement() { return move; }
    public override string GetStatus() { return (status == "None") ? " " : status ; }

    public override void SubHealth(int damage) { health -= damage; }


    // Get functions - possibly put into BaseCard?
    public List<string> GetActions() { return actions; }

    // Skill card skills - template class T is either PlayingCard or BossCard
    void Heal(PlayingCard target) 
    {
        target.health *= healthMultiplier;
    }
    void increaseDamage(PlayingCard target)
    {
        target.attack *= attackMultiplier;
    }
    void Fortify(PlayingCard target)
    {
        target.defense += increaseDefense;
    }
    void Sacrifice(PlayingCard target)
    {
        health -= sacrificeAmt;
        target.health += sacrificeAmt;
    }
    void SurroundDamage(PlayingCard[] targets,int damage)
    {
        foreach(PlayingCard card in targets)
        {
            card.health -= damage;
        }
    }
    void Summon() { }
}

// Skill cards -> would have to be connected to the boss card somehow
// Effect: have a target, sometimes a status condition
//  D = damage multiplier -> take the hero's attack and multiply it by the attack value
//  H = healing -> take selected card and add a multiplier to health
//  Def = "permanently" add number of points to defense