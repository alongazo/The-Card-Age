using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class drag : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform hand = null;
    public Transform placeholderParent = null;
    public enum slot { Weapon, Head, Chest, Legs, Feet, Inventory };
    public slot typeOfItem = slot.Weapon;
    GameObject placeholder;
    //public int itemIndex;
    public Board boardScript;

	string cardName;
    int itemIndex;
    PlayingCard card;
    List<PlayingCard> originated;
    public void setItemIndex(int index) { itemIndex = index; }
    public void setCard(PlayingCard card) { this.card = card; }
    public void setOriginator(ref List<PlayingCard> originator) { originated = originator; }
    Vector3 dist;
    float posX;
    float posY;

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right)
		{
			//Debug.Log ("Right Click Hand");
			//Debug.Log (this.card.GetName());
			//Debug.Log("card on had was right clicked");
			GameObject go = Instantiate(boardScript.chessmanPrefabs[itemIndex]) as GameObject;
			//Debug.Log(go.GetComponent<Card>().name);
			go.GetComponent<MeshRenderer>().enabled = false;
			go.GetComponent<Card>().Link(Globals.cardDatabase[this.card.GetName()]);
			boardScript.ViewHandCard(go.GetComponent<Card>(), gameObject.name);
			Destroy(go);
			//Debug.Log(this.name);

		}
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;

        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent, false);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleWidth = 0;
        le.flexibleHeight = 0;
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());


        hand = this.transform.parent;
        placeholderParent = hand;
        this.transform.SetParent(this.transform.parent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;

        //dropzone[] zones = GameObject.FindObjectsOfType<dropzone>();
        //find all possible drop zone
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos - new Vector3(0,0,(float)0.1);

        if (placeholder.transform.parent != placeholderParent)
            placeholder.transform.SetParent(placeholderParent, false);

        int newSiblingIndex = placeholderParent.childCount;

        for (int i = 0; i < placeholderParent.childCount; i++)
        {
            if (this.transform.position.x < placeholderParent.GetChild(i).position.x)
            {
                newSiblingIndex = i;
                if (placeholder.transform.GetSiblingIndex() < newSiblingIndex)
                    newSiblingIndex--;

                break;
            }
        }
        placeholder.transform.SetSiblingIndex(newSiblingIndex);

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(hand, false);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //EventSystem.current.RaycastAll(eventData) // get a list of things underneath
        Destroy(placeholder);
        RaycastHit hit;
        // find where the mouse is on the board when left click is released
		if (boardScript.GetCurrentAP () > 0) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("ChessPlane"))) {
				// check if no card is on the tile
				if (card.GetCardType () == CardType.Skill && boardScript.cards [boardScript.selectionX, boardScript.selectionY] != null) { //TRYING TO IMPLEMENT SKILL CARDS!
					if (card.IsPlayer () && Player.CanSkill () || !card.IsPlayer () && Enemy.CanSkill ()) {
						Debug.Log ("Using skill on a card");
						boardScript.MinusAP (card.GetCost ());
						int bossAttack = boardScript.GetAttackOfBoss (card.IsPlayer ());
						//List<Card> targets = new List<Card>();
						//if (boardScript.selectionX + 1 < Globals.numCols) { targets.Add(boardScript.cards[boardScript.selectionX+1, boardScript.selectionY]); }
						//if (boardScript.selectionX - 1 > 0) { targets.Add(boardScript.cards[boardScript.selectionX-1, boardScript.selectionY]); }
						//if (boardScript.selectionY + 1 < Globals.numRows) { targets.Add(boardScript.cards[boardScript.selectionX, boardScript.selectionY+1]); }
						//if (boardScript.selectionY - 1 > 0) { targets.Add(boardScript.cards[boardScript.selectionX, boardScript.selectionY-1]); }
						card.DetermineSkill (bossAttack, ref boardScript.cards [boardScript.selectionX, boardScript.selectionY]);//, targets.ToArray());
						originated.Remove (card);
						Destroy (this.gameObject);

					} else {
						Debug.Log ("Can't use skill!");
						if (card.IsPlayer ()) {
							Player.SetCanSkill (true);
						} else {
							Enemy.SetCanSkill (true);
						}
					}
				} else if (boardScript.cards [boardScript.selectionX, boardScript.selectionY] == null) {
					boardScript.MinusAP (card.GetCost ());
					boardScript.Spawn (itemIndex, card, boardScript.selectionX, boardScript.selectionY);

					originated.Remove (card);
					Destroy (this.gameObject);
				}
			} else {

				this.transform.position += new Vector3 (0, 0, (float)0.1);
			}
		}
    }


    public bool OnEndDrag(int x, int y)
    {
        this.transform.SetParent(hand, false);
        //this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        //EventSystem.current.RaycastAll(eventData) // get a list of things underneath
        Destroy(placeholder);

        // check if no card is on the tile
        if (boardScript.cards[x, y] == null)
        {
            // boardScript.Spawn(itemIndex, cardName, boardScript.selectionX, boardScript.selectionY);
            boardScript.Spawn(itemIndex, card, x, y);

            originated.Remove(card);
            
            //Debug.Log("Hand is " + originated.Count.ToString() + " cards big");
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }
}
