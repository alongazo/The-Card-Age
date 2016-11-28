using UnityEngine;
using System;
using System.Collections.Generic;

public class CardPackManager : MonoBehaviour {

    public List<GameObject> cardPacks;
    public Vector3 spawnCoords;
    public GameObject cardPack;
    public Transform parent;

    public void Start()
    {
        Quaternion rotatePack = Quaternion.identity;
        Vector3 newCoords = spawnCoords;
        rotatePack.eulerAngles = new Vector3 (0,0,90);
        foreach (GameObject card in cardPacks)
        {
            GameObject newPack = Instantiate(card, newCoords, Quaternion.identity,parent) as GameObject;
            newPack.transform.localPosition = newCoords;
            newPack.transform.localScale = new Vector3(.5f, .5f, .5f);
            newPack.transform.eulerAngles = rotatePack.eulerAngles;
            newCoords.x -= .45f;
        }
    }

    public void spawnCardPacks()
    {

    }
}
