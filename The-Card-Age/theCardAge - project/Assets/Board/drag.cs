using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform hand = null;
    public Transform placeholderParent = null;
    public enum slot{ Weapon, Head, Chest, Legs, Feet, Inventory};
    public slot typeOfItem = slot.Weapon;
    GameObject placeholder;
    //public int itemIndex;
    public Board boardScript;


    int itemIndex;
    string cardName;
    List<PlayingCard> originated;
    public void setItemIndex(int index) { itemIndex = index; }
    public void setCardName(string name) { cardName = name; }
    public void setOriginator(ref List<PlayingCard> originator) { originated = originator; }


    public void OnBeginDrag(PointerEventData eventData)
    {
        placeholder = new GameObject();
        placeholder.transform.SetParent(this.transform.parent);
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
        this.transform.position = eventData.position;

        if (placeholder.transform.parent != placeholderParent)
            placeholder.transform.SetParent(placeholderParent);

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
        this.transform.SetParent(hand);
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //EventSystem.current.RaycastAll(eventData) // get a list of things underneath
        Destroy(placeholder);
        RaycastHit hit;
        // find where the mouse is on the board when left click is released
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            // check if no card is on the tile
            if (boardScript.cards[boardScript.selectionX, boardScript.selectionY] == null)
            {
                boardScript.Spawn(itemIndex, cardName, boardScript.selectionX, boardScript.selectionY);
                
                for (int i=0; i<originated.Count; i++)
                {
                    if (originated[i].GetName() == cardName)
                    {
                        originated.RemoveAt(i);
                        break;
                    }
                }
                Destroy(this.gameObject);
            }
        }
    }
    

}
