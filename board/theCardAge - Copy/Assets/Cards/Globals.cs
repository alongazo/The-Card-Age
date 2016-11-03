﻿using System;
using UnityEngine;
using System.Collections.Generic;

public class Globals : MonoBehaviour
{
    static public Dictionary<string, BossCard> bossDatabase;
    static public Dictionary<string, PlayingCard> cardDatabase;

    static public int numRows = 6;
    static public int numCols = 6;

    public TextAsset textfile;


    // Going to try to connect this to a Card class
    static public PlayingCard testCard;

    void Awake()
    {
        bossDatabase = new Dictionary<string, BossCard>();
        cardDatabase = new Dictionary<string, PlayingCard>();

        PlayingCard newCard;
        BossCard newBossCard;
        foreach (string cardInfo in textfile.text.Split('\n'))
        {
            string[] splitInfo = System.Text.RegularExpressions.Regex.Split(cardInfo, ",:,");
            CardType newType = (CardType)Enum.Parse(typeof(CardType), splitInfo[1]);
            string[] initInfo = splitInfo[0].Split(',');
            int[] stats = Array.ConvertAll<string, int>(splitInfo[2].Split(','), int.Parse);

            if (newType == CardType.Boss)
            {
                newBossCard = new BossCard(initInfo, newType, stats);
                bossDatabase[initInfo[0]] = newBossCard;
            }
            else
            {
                newCard = new PlayingCard(initInfo, newType);
                newCard.LoadCard(stats, splitInfo[3]);
                cardDatabase[initInfo[0]] = newCard;
            }
        }
        testCard = cardDatabase["Angel"];
        Debug.Log(testCard.GetName() + " is initialized");
    }
}