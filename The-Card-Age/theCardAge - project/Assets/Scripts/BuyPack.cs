using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class BuyPack : MonoBehaviour {
    public int packAmt = 0;
    public int costFor5 = 450;
    public int costFor3 = 290;
    public int costFor2 = 200;
    public int costFor1 = 100;
    public Text packManager;
    public Text moneyManager;
    public void OnMouseDown()
    {
        if (!GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused&&GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "shop-buy")
        {
            addPacks();
        }
    }
    public void addPacks()
    {
        int money = moneyManager.GetComponent<MoneyManager>().totalMoney;
        if (packAmt == 5 && money >= costFor5)
        {
            packManager.GetComponent<CardPackTracker>().totalPacks += packAmt;
            packManager.GetComponent<CardPackTracker>().changeText();
            moneyManager.GetComponent<MoneyManager>().totalMoney -= costFor5;
            moneyManager.GetComponent<MoneyManager>().changeText();
        }
        else if (packAmt == 3 && money>=costFor3)
        {
            packManager.GetComponent<CardPackTracker>().totalPacks += packAmt;
            packManager.GetComponent<CardPackTracker>().changeText();
            moneyManager.GetComponent<MoneyManager>().totalMoney -= costFor3;
            moneyManager.GetComponent<MoneyManager>().changeText();
        }
        else if(packAmt == 2 && money >=costFor2)
        {
            packManager.GetComponent<CardPackTracker>().totalPacks += packAmt;
            packManager.GetComponent<CardPackTracker>().changeText();
            moneyManager.GetComponent<MoneyManager>().totalMoney -= costFor2;
            moneyManager.GetComponent<MoneyManager>().changeText();
        }
        else if(packAmt == 1 && money >=costFor1)
        {
            packManager.GetComponent<CardPackTracker>().totalPacks += packAmt;
            packManager.GetComponent<CardPackTracker>().changeText();
            moneyManager.GetComponent<MoneyManager>().totalMoney -= costFor1;
            moneyManager.GetComponent<MoneyManager>().changeText();
        }
        else
        {
            Debug.Log("Not enough money.");
        }
    }
}
