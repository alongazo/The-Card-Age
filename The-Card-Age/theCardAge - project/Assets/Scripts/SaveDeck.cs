using UnityEngine;
using System.Collections;
using System.IO;

public class SaveDeck : MonoBehaviour {


	public void writeToFile()
    {
        var fileName = "Assets/Cards/playerdeck.txt";
        GameObject deck = GameObject.Find("PlayerDeck");
        if(File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        var of = File.CreateText(fileName);
        int deckSize = 0;
        of.Write("Knight:1\r\n");
        foreach(var card in deck.GetComponent<VillageDeck>().playableDeck)
        {
            of.Write(card.Key + ":" + card.Value.ToString());
            of.Write("\r\n");
        }
        
        of.Close();
    }
}
