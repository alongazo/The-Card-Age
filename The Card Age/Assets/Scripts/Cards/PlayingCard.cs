using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CardType { Boss, Minion, Skill };

public class PlayingCard
{
    protected string cardName;
    protected string affiliation;

    CardType cardType;
    int rank;
    int cost;
    int move;

    protected int attack, baseAttack;
    protected int health, baseHealth;
    protected int defense, baseDefense;

    protected string status;

    protected List<string> actions;
    protected bool discard;

    public PlayingCard(string[] initInfo, CardType type)
    {
        cardName = initInfo[0];
        affiliation = initInfo[0];
        cardType = type;
        discard = false;
    }

    public PlayingCard(PlayingCard toCopy)
    {
        cardName = toCopy.cardName; affiliation = toCopy.affiliation;
        cardType = toCopy.cardType; rank = toCopy.rank; cost = toCopy.cost;
        move = toCopy.move;
        attack = toCopy.attack; baseAttack = toCopy.baseAttack;
        health = toCopy.health; baseHealth = toCopy.baseHealth;
        defense = toCopy.defense; baseDefense = toCopy.baseDefense;
        actions = toCopy.actions;
        discard = toCopy.discard;
        status = toCopy.status;
    }

    public void LoadCard(int[] info, string status="none")
    {
        rank = info[0]; cost = info[1]; move = info[3]; 
        attack = baseAttack = info[4];
        health = baseHealth = info[5];
        defense = baseDefense = info[6];

        if (cardType == CardType.Minion || cardType == CardType.Boss)
        {
            actions.Add("Attack"); actions.Add("Move");
        }
        this.status = status;
        actions.Add("Discard");
    }

    public string SaveCard()
    {
        string toSave = cardName;
        int[] info = { rank, cost, move, baseAttack, baseHealth, baseDefense};
        return toSave + ":" +info.ToString() + ":" + status;
    }

    public void DiscardCard()
    {
        discard = true;
    }

    public virtual void RestoreCard()
    {
        discard = false;
        attack = baseAttack;
        health = baseHealth;
        defense = baseDefense;
        if (cardType != CardType.Skill)
        {
            status = "none";
        }
    }

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

    public void AttackEnemy(PlayingCard enemy, int attackToMultiply=0)
    {
        int damage = CalculateDamage(enemy.defense, attackToMultiply);
        if (enemy.health < damage) { DiscardCard(); }
        else {  enemy.health -= damage; }
    }

    protected void ModifyAttack(int changeby) { attack += changeby; }
    

    

    // Skill card skills - template class T is either PlayingCard or BossCard
    void Heal<T>(T target) { }
    void Fortify<T>(T target) { }
    void Sacrifice<T>(T target) { }
    void SurroundDamage<T> (T[] targets) { }
    void Summon() { }
}

// Skill cards -> would have to be connected to the boss card somehow
// Effect: have a target, sometimes a status condition
//  D = damange multiplier -> take the hero's attack and multiply it by the attack value
//  H = healing -> take selected card and add a multiplier to health
//  Def = "permanently" add number of points to defense