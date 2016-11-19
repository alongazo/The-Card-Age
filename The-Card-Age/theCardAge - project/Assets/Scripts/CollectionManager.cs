using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectionManager : MonoBehaviour {
 
    public List<GameObject> cardCollection;
    public List<GameObject> spawnedCards;
    public Transform parent;
    public Vector3 rotateAngles;
    public Vector3 scaleVector;
    public Vector3 baseCollectionCoords;
    public float zSpacing;
    public float xSpacing;
    public int collectionIndex = 0;
    public int spawnedIndex = 0;
    public int cardsInRow;
    public int totalSpawnedCards;
    // Use this for initialization
    void Start () {
        int count = 1;
        Vector3 cardPosition = baseCollectionCoords;
        Quaternion rotateCard = Quaternion.identity;
        rotateCard.eulerAngles = rotateAngles;
        for (int i = collectionIndex; i < collectionIndex + totalSpawnedCards; i++)
        {
            if (i < cardCollection.Count)
            {
                GameObject newCard = Instantiate(cardCollection[i], cardPosition, rotateCard, parent) as GameObject;
                newCard.transform.localScale = scaleVector;
                newCard.name = cardCollection[i].name;
                spawnedCards[spawnedIndex] = newCard;
                spawnedIndex++;
                cardPosition.z -= zSpacing;
                if (count == cardsInRow)
                {

                    cardPosition.x -= xSpacing;
                    cardPosition.z = baseCollectionCoords.z;
                }
                count++;
                if (count == totalSpawnedCards)
                    count = 1;
            }
            //Debug.Log(count);
        }
        //collectionIndex += 6;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void spawnSet()
    {
        int count = 1;
        Vector3 cardPosition = baseCollectionCoords;
        //cardPosition.z -= 2;
        Vector3 moveCardPosition = new Vector3(cardPosition.x, cardPosition.y, cardPosition.z + 2);
        Quaternion rotateCard = Quaternion.identity;
        rotateCard.eulerAngles = rotateAngles;

        //foreach (GameObject card in spawnedCards)
        //card.transform.position = Vector3.Lerp(card.transform.position, moveCardPosition, 1);
        foreach (GameObject card in spawnedCards)
        {
            Destroy(card);
        }
        spawnedIndex = 0;
        for (int i = collectionIndex; i < collectionIndex + totalSpawnedCards; i++)
        {
            if (i < cardCollection.Count)
            {
                /*Sprite newCard =*/
                GameObject newCard = Instantiate(cardCollection[i], cardPosition, rotateCard, parent) as GameObject;// as Sprite;
                newCard.name = cardCollection[i].name;
                newCard.transform.localScale = scaleVector;
                spawnedCards[spawnedIndex] = newCard;
                spawnedIndex++;
                cardPosition.z -= zSpacing;
                if (count == cardsInRow)
                {

                    cardPosition.x -= xSpacing;
                    cardPosition.z = baseCollectionCoords.z;
                }
                count++;
                if (count == totalSpawnedCards)
                    count = 1;
            }
        }
        
    }
    public void nextButton()
    {
        if(collectionIndex+totalSpawnedCards<=cardCollection.Count)
            collectionIndex += totalSpawnedCards;
    }
    public void backButton()
    {
        if(collectionIndex>=totalSpawnedCards)
            collectionIndex -= totalSpawnedCards;
    }
    
}
