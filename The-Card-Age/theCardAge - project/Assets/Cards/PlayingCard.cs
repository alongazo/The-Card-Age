using UnityEngine;
using System;
using System.Collections.Generic;

public enum CardType { Fake = -1, Boss = 0, Minion = 1, Skill = 2 };

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
    bool isPlayer;

    protected int attack, baseAttack;
    public int attackMultiplier = 2;
    protected int health, baseHealth;
    public int healthMultiplier = 2;
    protected int defense, baseDefense;
    public int increaseDefense = 2;
    protected string skill;
    protected string status;
    public int sacrificeAmt = 5;

    protected List<string> actions;

    public int idNumber;

    // Initialization
    public PlayingCard(string[] initInfo, CardType type)
    {
        cardName = initInfo[0];
        affiliation = initInfo[1];
        image = initInfo[2];
        ////Debug.Log("Image for " + cardName + " is " + image);
        description = initInfo[3];

        cardType = type;
        discard = false;

        actions = new List<string>();
    }
    public PlayingCard(PlayingCard toCopy)
    {
        //Debug.Log("Called copy constructor");
        cardName = toCopy.cardName; affiliation = toCopy.affiliation;
        image = toCopy.image; description = toCopy.description;
        cardType = toCopy.cardType; rank = toCopy.rank; cost = toCopy.cost;
        move = toCopy.move;
        attack = toCopy.attack; baseAttack = toCopy.baseAttack;
        health = toCopy.health; baseHealth = toCopy.baseHealth;
        defense = toCopy.defense; baseDefense = toCopy.baseDefense;
        actions = toCopy.actions;
        discard = toCopy.discard;
        skill = toCopy.skill;
        status = toCopy.status;
        idNumber = toCopy.idNumber;
        isPlayer = toCopy.isPlayer;
    }
    public PlayingCard()
    {
        cardName = "!";
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
        this.skill = status.Split(' ')[0].Replace("\r", ""); // need tp figure out how to separate the status -> how does the status store things?

        this.status = status;
        actions.Add("Discard");
    }


    // Overloading functions relating to equality
    public static bool operator ==(PlayingCard right, PlayingCard left)
    {
        // above should be replaceable with this:
        if (object.ReferenceEquals(right, null))
        {
            return object.ReferenceEquals(left, null);
        }

        if (object.ReferenceEquals(left, null))
        {
            return object.ReferenceEquals(right, null);
        }

        return (right.idNumber == left.idNumber) && (right.cardName == left.cardName);
    }
    public static bool operator !=(PlayingCard right, PlayingCard left)
    {
        if (object.ReferenceEquals(right, null))
        {
            return !object.ReferenceEquals(left, null);
        }
        if (object.ReferenceEquals(left, null))
        {
            return !object.ReferenceEquals(right, null);
        }
        return !(right.idNumber == left.idNumber) || !(right.cardName == left.cardName);
    }
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }



    // Helper functions 
    int CalculateDamage(int enemyDefense, int attackToMultiply)
    {
        int damage = 0;
        if (cardType == CardType.Skill)
        {
            Debug.Log("Base Attack: " + attackToMultiply.ToString() + ", Multiplier: " + (((float)attack / (float)10.0)).ToString());
            damage = Mathf.CeilToInt(attackToMultiply * ((float)attack / (float)10.0));
        }
        else {
            damage = Mathf.Max(0, attack - enemyDefense);
            Debug.Log("Attack = " + attack.ToString() + ", Enemy Defense = " + enemyDefense.ToString());
        }
        return damage;
    }

    // Overwrite the functions in BaseCard
    public override bool[,] PossibleMove()
    {
        ////Debug.Log("Is calling the PossibleMove function of PlayingCard");
        bool[,] moves = new bool[Globals.numCols, Globals.numRows];
        int x = linkedBoardCard.CurrentX, y = linkedBoardCard.CurrentY;
        for (int spread = 1; spread - 1 < move; spread++)
        {
            if (y + spread < Globals.numRows)
                moves[x, y + spread] = true;
            if (y - spread > -1)
                moves[x, y - spread] = true;
            if (x + spread < Globals.numCols)
                moves[x + spread, y] = true;
            if (x - spread > -1)
                moves[x - spread, y] = true;

            // For right now, we're going to assume that everyone moves 1 tile at a time,
            //   but later on, maybe I could implement something with a more Fire Emblem-y style movement thing
            // Check out: http://gamedev.stackexchange.com/questions/61928/finding-possible-moves-for-an-entity-in-a-2d-tiled-game
            //            http://gamedev.stackexchange.com/questions/46681/how-to-make-my-characters-turn-smoothy-while-walking-on-a-pathlist-of-coordinat
        }
        return moves;
    }

    public void SetIsPlayer(bool truth) { isPlayer = truth; }

    public override void RestoreCard()
    {
        discard = false;
        attack = baseAttack;
        health = baseHealth;
        defense = baseDefense;
        if (cardType != CardType.Skill)
        {
            status = "";
        }
        linkedBoardCard.UpdateHealth();
    }
    public override void AttackEnemy(BaseCard enemy, int attackToMultiply = 0)
    {
        int damage = CalculateDamage(enemy.GetDefense(), attackToMultiply);
        if (enemy.GetHealth() < damage) { enemy.SubHealth(enemy.GetHealth()); }
        else { enemy.SubHealth(damage); }
        enemy.linkedBoardCard.UpdateHealth();
    }
    protected void ModifyAttack(int changeby) { }

    public override string GetName() { return cardName; }
    public override string GetImage() { return image; }
    public override string GetDescription() { return description; }
    public override CardType GetCardType() { return cardType; }
    public override int GetHealth() { return health; }
    public override int GetDefense() { return defense; }
    public override int GetAttack() { return attack; }
    public override int GetMaxHealth() { return baseHealth; }
    public override int GetMovement() { return move; }
    public override string GetStatus() { return (status == "") ? "" : status; }
    public override bool IsPlayer() { return isPlayer; }
	public override int GetCost() { return cost; }

    public override void SubHealth(int damage) { health -= damage; }

    // Get functions - possibly put into BaseCard?
    public List<string> GetActions() { return actions; }

    // Skill card skills - template class T is either PlayingCard or BossCard
    void Heal(PlayingCard target)
    {
        Debug.Log("health = " + health + ", target.baseHealth = " + target.baseHealth);
        int restorePoints = Mathf.CeilToInt(((float)target.baseHealth*health)/100f);
        Debug.Log("Healing " + restorePoints + " points");
        target.health = Mathf.Min(target.baseHealth, target.health + restorePoints);
        target.linkedBoardCard.UpdateHealth();
    }
    void increaseDamage(PlayingCard target)
    {
        target.attack *= attackMultiplier;
    }
    void Fortify(PlayingCard target)
    {
        target.defense += defense;
    }
    void Sacrifice(PlayingCard target)
    {
        health -= sacrificeAmt;
        target.health += sacrificeAmt;
    }
    void SurroundDamage(PlayingCard[] targets, int damage)
    {
        foreach (PlayingCard card in targets)
        {
            card.health -= damage;
        }
    }
    void MultiAttack(PlayingCard target, int bossAttack)
    {
        int numAttacks, damage = 0;
        int minMultiplier, maxMultiplier;

        string[] attackinfo = status.Split(' ')[1].Split(',');
        numAttacks = Convert.ToInt32(attackinfo[0]);
        minMultiplier = Convert.ToInt32(attackinfo[1]);
        maxMultiplier = Convert.ToInt32(attackinfo[2]);

        for (int i = 0; i < numAttacks; i++)
        {
            damage += Mathf.CeilToInt((float)bossAttack * (float)(UnityEngine.Random.Range(minMultiplier, maxMultiplier)) / (float)10);
        }
        damage = Mathf.Max(0, damage - target.defense);
        damage = Mathf.Max(0, target.health - damage);
        target.SubHealth(damage);
        target.linkedBoardCard.UpdateHealth();
    }

    void Card_Damage(PlayingCard target, int bossAttack)
    {
        int numAttacks = Player.HandSize();
        int damage = Mathf.CeilToInt((float)bossAttack*.20f*numAttacks);
        damage = Mathf.Max(0, damage - target.defense);
        damage = Mathf.Max(0, target.health - damage);
        Debug.Log("Heart of the Cards is dealing " + damage + " damage");
        target.SubHealth(damage);
        target.linkedBoardCard.UpdateHealth();
    }


    void Surround(Card target)
    {
        // for now, just going to hard code in the thing... cause we've only got one card like this
        Board temp = GameObject.Find("BoardManager").GetComponent<Board>();
        if (target.isWhite)
        {
            temp.enemyDamage = 5;
            temp.enemyDamagePoint = new Coordinate(target.CurrentX, target.CurrentY);
        }
        else
        {
            temp.playerDamage = 5;
            temp.playerDamagePoint = new Coordinate(target.CurrentX, target.CurrentY);
        }
    }

    public void Damage(int damage)
    {
        SubHealth(Mathf.Min(health, damage));
        linkedBoardCard.UpdateHealth();
    }

    public void DetermineSkill(int bossAttack, ref Card target)//, Card[] targets)
    {
        Debug.Log("Skill = " + status.Split(' ')[0] + " (" + (skill == "Card_Damage") + ")");

        switch (skill)
        {
            case "Attack":
                Debug.Log("Attacking skill: bossAttack = " + bossAttack.ToString());
                target.Attack(this, bossAttack);
                break;
            case "Cancel":
                Player.SetCanSkill(isPlayer);
                Enemy.SetCanSkill(!isPlayer);
                break;
            case "Fortify":
                Fortify(target.linkedPlayingCard);
                break;
            case "Summon":
                Player.doubleSummon = isPlayer;
                Enemy.doubleSummon = !isPlayer;
                break;
            case "Esuna":
                target.linkedPlayingCard.status = "None";
                break;
            case "Paralysis":
                target.linkedPlayingCard.status = "Prlyze";
                break;
            case "Heal":
                Heal(target.linkedPlayingCard);
                break;
            case "Card_Damage":
                Card_Damage(target.linkedPlayingCard, bossAttack);
                break;
            case "Surround":
                Surround(target);
                break;
            case "Multiple":
                MultiAttack(target.linkedPlayingCard, bossAttack);
                break;
        }
    }
}

// Skill cards -> would have to be connected to the boss card somehow
// Effect: have a target, sometimes a status condition
//  D = damage multiplier -> take the hero's attack and multiply it by the attack value
//  H = healing -> take selected card and add a multiplier to health
//  Def = "permanently" add number of points to defense