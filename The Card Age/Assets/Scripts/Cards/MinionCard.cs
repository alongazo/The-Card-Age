using UnityEngine;
using System.Collections;

public class MinionCard : BaseCard
{
    int attack, baseAttack;
    int health, baseHealth;
    int defense;
    int moveCost;

    public MinionCard(string name, string affiliation, int rank, int cost, int newAttack, int move, int newHealth)
    : base(name, affiliation, rank, cost)
    {
        attack = baseAttack = newAttack;
        health = baseHealth = newHealth;
        defense = 0;
        moveCost = move;

        actions.Insert(0, "Attack");
        actions.Insert(1, "Move");
    }

    int CalculateDamage(int enemyDefense)
    {
        int changeBy = Random.Range(-2, 3);
        if (attack+changeBy < enemyDefense)
        {
            return 0;
        }
        return attack + changeBy;
    }

    public void AttackEnemy(MinionCard enemy)
    {
        int damage = CalculateDamage(enemy.defense);
        if (enemy.health < damage)
        {
            DiscardCard();
        }
        else
        {
            enemy.health -= damage;
        }
    }
    protected void ModifyAttack(int changeby) { attack += changeby; }

    public override void RestoreCard()
    {
        base.RestoreCard();
        attack = baseAttack;
        health = baseHealth;
    }

    public int GetMoveCost() { return moveCost; }
}
