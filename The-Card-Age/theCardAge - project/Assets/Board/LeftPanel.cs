using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeftPanel : MonoBehaviour {

    // Use this for initialization
    public GameObject itemPrefab;
    public Scrollbar scrollBar;
    //private int itemCount = 1;
    public Board boardScript;
    //[SerializeField]
    private float height;
    private float panelOriginalHeight;
    //private List<GameObject> itemPrefabList;
    void Start()
    {
        //itemPrefabList = new List<GameObject>();
        //itemCount = boardScript.activeCard.Count;
        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        height = rowRectTransform.rect.height;
        Debug.Log("height: " + height);
        panelOriginalHeight = gameObject.GetComponent<RectTransform>().sizeDelta.y;
        Debug.Log("panel: " + panelOriginalHeight);
    }

    public void CreateHPBar(Card cardPiece)
    {
        int boardCount = 0;
        if (boardScript.IsWhiteTurn())
        {
            boardCount = boardScript.GetPlayerCardOnField();
        }

        else if (!boardScript.IsWhiteTurn())
        {
            boardCount = boardScript.GetEnemyCardOnField();
        }
        
        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();
        Debug.Log("Creating HP Bar");

        // create hp bar game object
        GameObject newItem = Instantiate(itemPrefab) as GameObject;
        // change the name of the game object
        newItem.name = gameObject.name + " item at (" + boardCount + ")";
        // set the parent
        newItem.transform.SetParent(gameObject.transform, false);

        // check if scroll panel need to be resize to fit all items
        //if ((boardCount * height) > containerRectTransform.sizeDelta.y)
        //{
        //    Debug.Log("changing: " + containerRectTransform.sizeDelta);
        //    containerRectTransform.sizeDelta = new Vector2(containerRectTransform.sizeDelta.x, boardCount * height);

        //}

        //// set hp bar to card class
        //cardPiece.SetHPBar(newItem);
        //cardPiece.LinkBarToObject(newItem);
        Debug.Log((boardCount * height) + " > " + containerRectTransform.sizeDelta.y);
        if ((boardCount * height) > containerRectTransform.sizeDelta.y)
        {
            Debug.Log("resize");
            //float scrollBarSizeRatio = panelOriginalHeight / (boardCount * height);
            scrollBar.GetComponent<Scrollbar>().value = 0.5f;
            //Debug.Log("Size: " + scrollBarSizeRatio);
            //Debug.Log("changing: " + containerRectTransform.sizeDelta.y +" to "+ (boardCount * height));
            containerRectTransform.position = new Vector3(containerRectTransform.position.x, -(boardCount * height) / 2, containerRectTransform.position.z);
            containerRectTransform.sizeDelta = new Vector2(containerRectTransform.sizeDelta.x, boardCount * height);
            //scrollBar.GetComponent<Scrollbar>().value = 0.5f;

        }
        scrollBar.GetComponent<Scrollbar>().value = 1.0f;
        //scrollBar.GetComponent<Scrollbar>().size = scrollBarSizeRatio;
        // set hp bar to card class
        cardPiece.SetHPBar(newItem);
        cardPiece.LinkBarToObject(newItem);

        //cardPiece.health.bar = newItem.GetComponent(typeof(Bar));
    }
}
