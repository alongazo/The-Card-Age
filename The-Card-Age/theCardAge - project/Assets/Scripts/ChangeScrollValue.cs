using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeScrollValue : MonoBehaviour {

    public Scrollbar scrollerToChange;
	public void increaseScrollValue()
    {
        scrollerToChange.value += .5f;
    }
    public void decreaseScrollValue()
    {
        scrollerToChange.value -= .5f;
    }
}
