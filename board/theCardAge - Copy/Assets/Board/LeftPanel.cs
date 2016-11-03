using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeftPanel : MonoBehaviour {

    // Use this for initialization
    public GameObject itemPrefab;
    //private int itemCount = 1;
    public Board boardScript;
    [SerializeField]
    private float height;
    //private List<GameObject> itemPrefabList;
    void Start()
    {
        //itemPrefabList = new List<GameObject>();
        //itemCount = boardScript.activeCard.Count;
        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        height = rowRectTransform.rect.height;


    }

    public void CreateHPBar(Card cardPiece)
    {
        int boardCount = boardScript.activeCard.Count;
        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();
        Debug.Log("Creating HP Bar");

        // create hp bar game object
        GameObject newItem = Instantiate(itemPrefab) as GameObject;
        // change the name of the game object
        newItem.name = gameObject.name + " item at (" + boardCount + ")";
        // set the parent
        newItem.transform.SetParent(gameObject.transform);

        // check if scroll panel need to be resize to fit all items
        if ((boardCount * height) > containerRectTransform.sizeDelta.y)
        {
            Debug.Log("changing: " + containerRectTransform.sizeDelta);
            containerRectTransform.sizeDelta = new Vector2(containerRectTransform.sizeDelta.x, boardCount * height);

        }

        // set hp bar to card class
        cardPiece.SetHPBar(newItem);
        cardPiece.LinkBarToObject(newItem);
        
        //cardPiece.health.bar = newItem.GetComponent(typeof(Bar));
    }
}
