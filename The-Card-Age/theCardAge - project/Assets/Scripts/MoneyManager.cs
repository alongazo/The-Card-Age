using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyManager : MonoBehaviour {

    public int totalMoney = 0;
    public Text moneyUI;
    public void Start()
    {
        moneyUI.text = totalMoney.ToString();
    }
    public void changeText()
    {
        moneyUI.text = totalMoney.ToString();
    }
}
