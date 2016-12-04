using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Stat
{
    [SerializeField]
    private Bar bar;
    [SerializeField]
    private int maxVal;
    [SerializeField]
    private int currentVal;

    public void LinkBarToObject(GameObject card)
    {
        bar = card.GetComponent<Bar>();
    }

    public int CurrentVal
    {
        get
        {
            return currentVal;
        }
        set
        {
            //this.currentVal = value;
			this.currentVal = Mathf.Clamp(value, 0, MaxVal);
            bar.Value = currentVal;
        }
    }
    public int MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            this.maxVal = value;
            bar.MaxValue = maxVal;
        }
    }

    public void Initialize(int health)
    {
        this.MaxVal = health;
        this.CurrentVal = health;
    }
}
