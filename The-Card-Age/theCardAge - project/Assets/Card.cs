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
    protected internal PlayingCard linkedPlayingCard;

    public bool isWhite;

    bool actionTaken;

    public void ResetTurn() {
        actionTaken = false;

        GetComponent<Renderer>().material.SetFloat("_Metallic", 0);
        GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }

    public bool HasTakenAction() { return actionTaken; }

    public void TookAction() {
        GetComponent<Renderer>().material.SetFloat("_Metallic", 1);
        GetComponent<Renderer>().material.SetColor("_Color", Color.gray);
        actionTaken = true;
    }
    
    void Update()
    {
        // debugging
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    linkedPlayingCard.AttackEnemy(linkedPlayingCard);
        //    health.CurrentVal = linkedPlayingCard.GetHealth();
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    linkedPlayingCard.RestoreCard();
        //    health.CurrentVal = linkedPlayingCard.GetHealth();
        //}
        //if (linkedPlayingCard.GetHealth() == 0)
        //{
        //    // probably need to be destroyed?
        //}
    }
    public void LinkBarToObject(GameObject card)
    {
        health.LinkBarToObject(card);
        health.Initialize(linkedPlayingCard.GetHealth());
    }
    public void Attack(Card attackingCard, int multiplier = 0)
    {
        //Debug.Log(attackingCard.linkedPlayingCard.GetName() + " is attacking");
        attackingCard.linkedPlayingCard.AttackEnemy(linkedPlayingCard, multiplier);
    }
    public void Attack(PlayingCard attackingCard, int multiplier = 0)
    {
        attackingCard.AttackEnemy(linkedPlayingCard, multiplier);
    }

    public float GetHPVal()
    {
        return health.CurrentVal;
    }
    public void SetHPBar(GameObject HPBar)
    {
        rightHPBar = HPBar;
        
        // add the card's name
        foreach (UnityEngine.UI.Text textItem in rightHPBar.GetComponentsInChildren<UnityEngine.UI.Text>())
        {
            //Debug.Log(textItem.name);
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
    public void UpdateHealth()
    {
        health.CurrentVal = linkedPlayingCard.GetHealth();
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
        //Debug.Log("In Possible Move, is linked? " + IsLinked().ToString());
        return linkedPlayingCard.PossibleMove();
    }



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


    // Getting information from attached card
    public int Defense()
    {
        return linkedPlayingCard.GetDefense();
    }
    public int Strength()
    {
        return linkedPlayingCard.GetAttack();
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
    public string Movement()
    {
        return linkedPlayingCard.GetMovement().ToString();
    }
    public string Status()
    {
        return linkedPlayingCard.GetStatus();
    }
    public CardType CardType()
    {
        return linkedPlayingCard.GetCardType();
    }

	public int Cost()
	{
		return linkedPlayingCard.GetCost();
	}

    public Texture Image()
    {
        string texture = "Assets/Card_Images/" + linkedPlayingCard.GetImage();
        ////Debug.Log("image is found at " + texture);
        return (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath(texture, typeof(Texture2D));
    }

    public Sprite Icon()
    {
        //Debug.Log("This is my name " + this.Name());
        string icon = "Assets/Playing_Cards/MonsterIcon/" + this.Name() + "Icon.psd";
        //Debug.Log("icon is found at " + icon);
        return (Sprite)UnityEditor.AssetDatabase.LoadAssetAtPath(icon, typeof(Sprite));
    }
    public void Damage(int damage) { linkedPlayingCard.Damage(damage); }
}