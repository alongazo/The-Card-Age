﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform hand = null;
    public Transform placeholderParent = null;
    public enum slot { Weapon, Head, Chest, Legs, Feet, Inventory };
    public slot typeOfItem = slot.Weapon;
    GameObject placeholder;
    //public int itemIndex;
    public Board boardScript;


    int itemIndex;
    PlayingCard card;
    List<PlayingCard> originated;
    public void setItemIndex(int index) { itemIndex = index; }
    public void setCard(PlayingCard card) { this.card = card; }
    public void setOriginator(ref List<PlayingCard> originator) { originated = originator; }
    Vector3 dist;
    float posX;
    float posY;

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
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            // check if no card is on the tile
            if(card.GetCardType() == CardType.Skill && boardScript.cards[boardScript.selectionX, boardScript.selectionY] != null)
            { //TRYING TO IMPLEMENT SKILL CARDS!
                Debug.Log("Using skill on a card");
                int bossAttack = boardScript.GetAttackOfBoss(card.IsPlayer());
                List<Card> targets = new List<Card>();
                if (boardScript.selectionX + 1 < Globals.numCols) { targets.Add(boardScript.cards[boardScript.selectionX+1, boardScript.selectionY]); }
                if (boardScript.selectionX - 1 > 0) { targets.Add(boardScript.cards[boardScript.selectionX-1, boardScript.selectionY]); }
                if (boardScript.selectionY + 1 < Globals.numRows) { targets.Add(boardScript.cards[boardScript.selectionX, boardScript.selectionY+1]); }
                if (boardScript.selectionY - 1 > 0) { targets.Add(boardScript.cards[boardScript.selectionX, boardScript.selectionY-1]); }
                card.DetermineSkill(bossAttack, ref boardScript.cards[boardScript.selectionX, boardScript.selectionY], targets.ToArray());

                originated.Remove(card);
                Destroy(this.gameObject);
            }
            else if (boardScript.cards[boardScript.selectionX, boardScript.selectionY] == null)
            {

                boardScript.Spawn(itemIndex, card, boardScript.selectionX, boardScript.selectionY);

                originated.Remove(card);
                Destroy(this.gameObject);
            }
        }
        else
        {

            this.transform.position += new Vector3(0, 0, (float)0.1);
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
