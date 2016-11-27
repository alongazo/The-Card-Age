using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class SellManager : MonoBehaviour {

    public List<GameObject> cardsSelected;
    public Text moneyManager;
    public void sellCards()
    {
        foreach(var card in cardsSelected)
        {
            moneyManager.GetComponent<MoneyManager>().totalMoney += 10;
            moneyManager.GetComponent<MoneyManager>().changeText();
        }
    }
}
