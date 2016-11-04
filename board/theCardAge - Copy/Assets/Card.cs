using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour {
    [SerializeField]
    private GameObject rightHPBar;

    [SerializeField]
    public Stat health;
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }

    // Attaching BaseCard to this card
    BaseCard linkedPlayingCard;

    public bool isWhite;

    //code for AP bar (delete when finish)
    //private void Awake()
    //{
    //    //health.Initialize(linkedPlayingCard.GetHealth());
    //}
    void Update()
    {
        // debugging
        if (Input.GetKeyDown(KeyCode.Q))
        {
            linkedPlayingCard.AttackEnemy(linkedPlayingCard);
            health.CurrentVal = linkedPlayingCard.GetHealth();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            linkedPlayingCard.RestoreCard();
            health.CurrentVal = linkedPlayingCard.GetHealth();
        }
    }
    public void LinkBarToObject(GameObject card)
    {
        health.LinkBarToObject(card);
        health.Initialize(linkedPlayingCard.GetHealth());
    }
    public void Attack(Card attackingCard)
    {
        Debug.Log(attackingCard.linkedPlayingCard.GetName() + " is attacking");
        attackingCard.linkedPlayingCard.AttackEnemy(linkedPlayingCard);
        health.CurrentVal = linkedPlayingCard.GetHealth();
    }
    public float GetHPVal()
    {
        return health.CurrentVal;
    }
    public void SetHPBar(GameObject HPBar)
    {
        rightHPBar = HPBar;



        // add the card's name
        foreach (UnityEngine.UI.Text textItem in rightHPBar.GetComponents<UnityEngine.UI.Text>())
        {
            Debug.Log(textItem.name);
            if (textItem.name == "Name")
            {
                textItem.text = linkedPlayingCard.GetName();
            }
        }
    }
    public GameObject GetHPBar()
    {
        return rightHPBar;
    }

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }
    public string Description()
    {
        return linkedPlayingCard.GetDescription();
    }
    public bool[,] PossibleMove()
    {
        Debug.Log("In Possible Move, is linked? " + IsLinked().ToString());
        return linkedPlayingCard.PossibleMove();
    }

    public int Defense() { return linkedPlayingCard.GetDefense(); }
    public int Strength() { return linkedPlayingCard.GetAttack(); }


    // Attaching BaseCard to this card
    public void Link(PlayingCard toLink)
    {
        linkedPlayingCard = new PlayingCard(toLink);
        linkedPlayingCard.Link(this);
    }
    public void Unlink()
    {
        linkedPlayingCard.Unlink();
        linkedPlayingCard = null;
    }
    public void CheckLink()
    {
        Debug.Log(linkedPlayingCard.GetName());
    }
    public bool IsLinked()
    {
        return linkedPlayingCard != null;
    }

    public string Name()
    {
        return linkedPlayingCard.GetName();
    }

    public string Health()
    {
        return linkedPlayingCard.GetHealth().ToString();
    }
    public string MaxHealth()
    {
        return linkedPlayingCard.GetMaxHealth().ToString();
    }

    public Texture Image()
    {
        string texture = "Assets/Card_Images/" + linkedPlayingCard.GetImage();
        Debug.Log("image is found at " + texture);
        return (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(texture, typeof(Texture2D));
    }
}