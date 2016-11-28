using UnityEngine;
using System;
using System.Collections.Generic;

public class OpenCardPack : MonoBehaviour {

    public List<GameObject> cardCollection;
    public List<GameObject> earnedCards;
    public List<GameObject> spawnedCards;
    public List<Vector3> cardFinalCoords;
    public Vector3 cardSpawnCoords;
    public Vector3 cardPackCoords;
    public List<GameObject> currentCollection;
    private Vector3 newFlapCoords;
    private Vector3 cardBackCoords;
    public int earnedCardsAmt;
    private bool moveTop = false;
    private bool moved = false;
    private bool shootCards = false;
    private bool stopShooting = false;
    private int cardReachedSpot = 0;
    private Transform flap;
    public void Start()
    {
        foreach(GameObject card in GameObject.Find("ShopCardCollection").GetComponent<CollectionManager>().cardCollection)
        {
            currentCollection.Add(card);
        }
    }
    public void Update()
    {
        if(moveTop&&!moved)
        {
            Vector3 temp = newFlapCoords;
            temp.y = newFlapCoords.y - .3f; 
            flap.localPosition = Vector3.Lerp(flap.localPosition,newFlapCoords,.05f);
            if (flap.localPosition.y >= temp.y)
            {
                moved = true;
                selectCards();
            }
        }
        if(shootCards&&!stopShooting)
        {
            Vector3 temp = cardBackCoords;
            temp.y = cardBackCoords.y - .05f;
            if (cardReachedSpot < earnedCardsAmt)
            {
                if (spawnedCards[cardReachedSpot].transform.localPosition.y >= temp.y)
                {
                    spawnedCards[cardReachedSpot].transform.localEulerAngles = new Vector3(90, 180, 90);
                    spawnedCards[cardReachedSpot].transform.localScale = new Vector3(6, .001f, 6);
                    spawnedCards[cardReachedSpot].transform.localPosition = cardFinalCoords[cardReachedSpot];
                    spawnedCards[cardReachedSpot].transform.parent = GameObject.Find("TemporaryCardHolder").transform;
                    cardReachedSpot++;
                    if(cardReachedSpot==earnedCardsAmt)
                    {
                        //Destroy(gameObject);
                        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, new Vector3(-100, -100, -100), .5f);
                        GameObject.Find("EnvelopeHolder").GetComponent<EnvelopeHolderManager>().cardPack = null;
                        GameObject.Find("EnvelopeHolder").GetComponent<EnvelopeHolderManager>().matchedLocation = false;
                    }
                }
                if (cardReachedSpot < earnedCardsAmt)
                {
                    spawnedCards[cardReachedSpot].transform.localPosition = Vector3.Lerp(spawnedCards[cardReachedSpot].transform.localPosition, cardBackCoords, .1f);
                }
            }
            else
            {
                stopShooting = true;
            }
            
        }
    }
    public void selectCards()
    {
        int rand = (int)Mathf.Round(UnityEngine.Random.Range(0, cardCollection.Count));
        for (int i=0;i<earnedCardsAmt;i++)
        {
            earnedCards.Add(cardCollection[rand]);
            rand = (int)Mathf.Round(UnityEngine.Random.Range(0, cardCollection.Count));
        }
        foreach(GameObject card in earnedCards)
        {
            GameObject newCard = Instantiate(card,gameObject.transform.localPosition,Quaternion.identity,gameObject.transform) as GameObject;
            addCardToCollection(newCard);
            newCard.name = card.name;
            newCard.transform.localScale = new Vector3(4, .001f, 4);
            newCard.transform.localPosition = cardSpawnCoords;
            newCard.transform.localEulerAngles = new Vector3(0, 180, -90);
            spawnedCards.Add(newCard);
            cardBackCoords = newCard.transform.localPosition;
            cardBackCoords.y += 2.5f;
        }
        shootCards = true;
    }
	public void spawnCards()
    {
        flap = transform.Find("CardAgeEnvelopeBottom").transform.Find("EnvelopeTop");
        newFlapCoords = flap.localPosition;
        newFlapCoords.y += 1.5f;
        moveTop = true;
    }
    private void addCardToCollection(GameObject newCard)
    {
        foreach(GameObject card in currentCollection)
        {
           if(newCard.GetComponent<CardPrefabInfo>().cardName == card.name)
            {
                GameObject.Find("ShopCardCollection").GetComponent<CollectionManager>().cardCollection.Add(card);
                GameObject.Find("Collection").GetComponent<CollectionManager>().cardCollection.Add(card);
            }
        }
    }
}
