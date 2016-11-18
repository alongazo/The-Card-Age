using UnityEngine;
using System;
using System.Collections.Generic;

public class CollectionManager : MonoBehaviour {
 
    public List<GameObject> cardCollection;
    public List<GameObject> spawnedCards;
    public Transform blacksmith;
    public Vector3 baseCollectionCoords;
    public int collectionIndex = 0;
    public int spawnedIndex = 0;
    // Use this for initialization
    void Start () {
        int count = 1;
        Vector3 cardPosition = baseCollectionCoords;
        Quaternion rotateCard = Quaternion.identity;
        rotateCard.eulerAngles = new Vector3(90, 90, 0);
        for (int i = collectionIndex; i < collectionIndex + 6; i++)
        {
            if (i < cardCollection.Count)
            {
                GameObject newCard = Instantiate(cardCollection[i], cardPosition, rotateCard, blacksmith) as GameObject;
                /*Sprite newCard =*/
                //Sprite newCard = Instantiate(cardCollection[i], cardPosition, rotateCard, blacksmith) as Sprite;
                newCard.transform.localScale = new Vector3(.06f, .06f, 2f);
                spawnedCards[spawnedIndex] = newCard;
                spawnedIndex++;
                cardPosition.z -= .45f;
                if (count == 3)
                {

                    cardPosition.x -= .7f;
                    cardPosition.z = baseCollectionCoords.z;
                }
                count++;
                if (count == 6)
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
        rotateCard.eulerAngles = new Vector3(90, 90, 0);

        //foreach (GameObject card in spawnedCards)
        //card.transform.position = Vector3.Lerp(card.transform.position, moveCardPosition, 1);
        foreach (GameObject card in spawnedCards)
        {
            Destroy(card);
        }
        spawnedIndex = 0;
        for (int i = collectionIndex; i < collectionIndex + 6; i++)
        {
            if (i < cardCollection.Count)
            {
                /*Sprite newCard =*/
                GameObject newCard = Instantiate(cardCollection[i], cardPosition, rotateCard, blacksmith) as GameObject;// as Sprite;
                newCard.transform.localScale = new Vector3(.06f, .06f, 2f);
                spawnedCards[spawnedIndex] = newCard;
                spawnedIndex++;
                cardPosition.z -= .45f;
                if (count == 3)
                {

                    cardPosition.x -= .7f;
                    cardPosition.z = baseCollectionCoords.z;
                }
                count++;
                if (count == 6)
                    count = 1;
            }
        }
        
    }
    public void nextButton()
    {
        if(collectionIndex+6<=cardCollection.Count)
            collectionIndex += 6;
    }
    public void backButton()
    {
        if(collectionIndex>=6)
            collectionIndex -= 6;
    }
    
}
