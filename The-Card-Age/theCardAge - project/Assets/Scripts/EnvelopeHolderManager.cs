﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnvelopeHolderManager : MonoBehaviour {

    public GameObject cardPack;
    public GameObject cardPackManager;
    public Vector3 newCardPackCoords;
    public Text packData;
    private bool moveCard = false;
    private bool open = false;
    public bool matchedLocation = false;
    public void Update()
    {
        if(moveCard&&cardPack!=null)
        {
            cardPack.transform.localPosition = Vector3.Lerp(cardPack.transform.localPosition, newCardPackCoords, .05f);
            if(cardPack.transform.localPosition == newCardPackCoords)
            {
                moveCard = false;
            }
        }
        if(cardPack!= null && cardPack.transform.localPosition == newCardPackCoords&&!matchedLocation)
        {
            open = true;
            matchedLocation = true;
        }
        if(open && cardPack!= null)
        {
            cardPack.GetComponent<OpenCardPack>().spawnCards();
            GameObject.Find("Collect Button").GetComponent<Button>().interactable = false;
            open = false;
        }
    }
    public void placeCardPack(GameObject pack)
    {
        cardPack = pack;
    }
    public void moveCardPack()
    {
        GameObject.Find("SettingTracker").GetComponent<TrackSetting>().blocked = true;
        packData.GetComponent<CardPackTracker>().totalPacks -= 1;
        packData.GetComponent<CardPackTracker>().changeText();
        //cardPackManager.GetComponent<CardPackManager>().cardPacks.RemoveAt(cardPackManager.GetComponent<CardPackManager>().cardPacks.Count-1);
        moveCard = true;
        //cardPack.transform.localPosition = Vector3.Lerp(cardPack.transform.localPosition, newCardPackCoords, .1f);
    }
}
