﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SellManager : MonoBehaviour {

    public List<GameObject> cardsSelected;
    public Text moneyManager;
    public GameObject shopCardCollection;
    public GameObject blacksmithCardCollection;
    public void sellCards()
    {
        foreach(GameObject card in cardsSelected)
        {
            if (card != null)
            {
                moneyManager.GetComponent<MoneyManager>().totalMoney += 10;
                moneyManager.GetComponent<MoneyManager>().changeText();
                removeFromCollections(card);
                shopCardCollection.GetComponent<CollectionManager>().cardCollection.Sort(compareListByName);
                shopCardCollection.GetComponent<CollectionManager>().spawnSet();
                blacksmithCardCollection.GetComponent<CollectionManager>().cardCollection.Sort(compareListByName);
                blacksmithCardCollection.GetComponent<CollectionManager>().spawnSet();
            }
        }
    }
    private static int compareListByName(GameObject obj1, GameObject obj2)
    {
        return obj1.name.CompareTo(obj2.name);
    }

    private void removeFromCollections(GameObject card)
    {
        foreach(var shopCard in shopCardCollection.GetComponent<CollectionManager>().cardCollection)
        {
            if (card.name == shopCard.name)
            {
                shopCardCollection.GetComponent<CollectionManager>().cardCollection.Remove(shopCard);
                break;
            }
        }
        foreach (var blacksmithCard in blacksmithCardCollection.GetComponent<CollectionManager>().cardCollection)
        {
            if (card.name == blacksmithCard.name)
            {
                blacksmithCardCollection.GetComponent<CollectionManager>().cardCollection.Remove(blacksmithCard);
                break;
            }
        }
    }
}
