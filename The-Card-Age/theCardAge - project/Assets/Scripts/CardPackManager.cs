using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class CardPackManager : MonoBehaviour {

    public List<GameObject> cardPacks;
    public Vector3 spawnCoords;
    public GameObject cardPack;
    public Transform parent;
    public Text packData;
    public void Start()
    {
        GameObject.Find("Collect Button").GetComponent<Button>().interactable = false;
        loadCardPacks();
        spawnCardPacks();
    }

    public void spawnCardPacks()
    {
        loadCardPacks();
        Quaternion rotatePack = Quaternion.identity;
        Vector3 newCoords = spawnCoords;
        rotatePack.eulerAngles = new Vector3(0, 0, 90);
        int count = 0;
        /*foreach(Transform pack in transform)
        {
            GameObject.Destroy(pack.gameObject);
        }*/
        foreach (GameObject card in cardPacks)
        {
            if (count < 5)
            {
                GameObject newPack = Instantiate(card, newCoords, Quaternion.identity, parent) as GameObject;
                newPack.transform.localPosition = newCoords;
                newPack.transform.localScale = new Vector3(.5f, .5f, .5f);
                newPack.transform.eulerAngles = rotatePack.eulerAngles;
                newCoords.x -= .45f;
                count++;
            }
        }
    }
    public void destroyChildren()
    {
        cardPacks.Clear();
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void loadCardPacks()
    {
        int packAmt = packData.GetComponent<CardPackTracker>().totalPacks;
        for(int i=0; i<packAmt;i++)
        {
            cardPacks.Add(cardPack);
        } 
    }
}
