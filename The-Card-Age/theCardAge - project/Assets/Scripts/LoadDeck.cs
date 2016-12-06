using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class LoadDeck : MonoBehaviour {
    public Dictionary<string,int> cardsInDeck;
    void Start()
    {
        cardsInDeck = new Dictionary<string, int>();
    }
	public void load()
    {
        var fileName = "Assets/Cards/playerdeck.txt";
        string card;
        try
        {
            StreamReader reader = new StreamReader(fileName, Encoding.Default);
            do
            {

                card = reader.ReadLine();
                if (card != null)
                {
                    string[] data = card.Split(':');
                    data[0]=data[0].Replace(' ', '_');
                    int amt = Convert.ToInt32(data[1]);
                    cardsInDeck.Add(data[0], amt);
                }
            }
            while (card != null);
            reader.Close();
        }
        catch (Exception e)
        {
            Debug.Log("File Not Found");
        }
        addToDeck();
    }
    public void addToDeck()
    {
        List <GameObject> currentCollection = GameObject.Find("Collection").GetComponent<CollectionManager>().cardCollection;
        foreach(var card in cardsInDeck)
        {
            foreach(GameObject cardInCollection in currentCollection)
            {
                if (card.Key.ToLower() == cardInCollection.name)
                {
                    Vector3 imagePos;
                    Transform newParent = GameObject.Find("CardList").transform;
                    GameObject.Find("PlayerDeck").GetComponent<VillageDeck>().playableDeck.Add(card.Key.ToLower(),card.Value);
                    GameObject.Find("PlayerDeck").GetComponent<VillageDeck>().currentSize += card.Value;
                    for (int i = 0; i < card.Value; i++)
                    {
                        RawImage deckImage = Instantiate(cardInCollection.GetComponent<VillageDrag>().imageInDeck) as RawImage;
                        imagePos = deckImage.rectTransform.localPosition;
                        deckImage.transform.parent = newParent;
                        deckImage.transform.localScale = new Vector3(1, 1, 1);
                        deckImage.transform.localEulerAngles = new Vector3(0, 0, 0);
                        imagePos.z = 0;
                        deckImage.rectTransform.localPosition = imagePos;
                    }
                }
            }
        }
    }
}
