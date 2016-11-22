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
        foreach(var card in deck.GetComponent<VillageDeck>().playableDeck)
        {
            of.Write(card.Key);
            of.Write(":");
            of.Write(card.Value);
            of.Write("\r\n");
        }
        of.Close();
    }
}
