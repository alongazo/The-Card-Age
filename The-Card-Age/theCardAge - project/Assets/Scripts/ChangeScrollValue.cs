﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeScrollValue : MonoBehaviour {

    public Scrollbar scrollerToChange;
	public void increaseScrollValue()
    {
        scrollerToChange.value += .1f;
        if(scrollerToChange.value>=1)
        {
            scrollerToChange.value = 1;
        }
    }
    public void decreaseScrollValue()
    {
        scrollerToChange.value -= .1f;
        if(scrollerToChange.value<=0)
        {
            scrollerToChange.value = 0;
        }
    }
}
