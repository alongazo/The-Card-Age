using UnityEngine;
using System.Collections;
using System.IO;

public class SaveDeck : MonoBehaviour {


	public void writeToFile()
    {
        var fileName = "deck.txt";
        GameObject deck = GameObject.Find("PlayerDeck");
        if(File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        var of = File.CreateText(fileName);
        int deckSize = 0;
        of.Write("Knight:");
        foreach(var card in deck.GetComponent<VillageDeck>().playableDeck)
        {
            for(int i=0;i<card.Value;i++)
            {
                of.Write(card.Key);
                deckSize++;
                if (deckSize < deck.GetComponent<VillageDeck>().currentSize)
                {
                    of.Write(",");
                }
            }
        }
        of.Write("\r\n");
        of.Close();
    }
}
