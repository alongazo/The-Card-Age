using System.Collections.Generic;

public class Player
{
    Deck wholeDeck;
    List<PlayingCard> hand;
   

    bool turnIsDone;
    string selectedAction;

    public Player(string bossName, string deckInfo)
    {
        string[] cardNames = deckInfo.Split(',');
        wholeDeck = new Deck(bossName, cardNames);
        hand = new List<PlayingCard>();
        turnIsDone = false;
        selectedAction = "";
    }
    
    public void DrawCard(bool again)
    {
        if (hand.Count < 7)
        {
            if (hand.Count == 0)
            {
                for (int i=0; i<4; i++)
                {
                    hand.Add(wholeDeck.DrawCard());
                }
            }
            else {
                hand.Add(wholeDeck.DrawCard());
                if (again)
                {
                    turnIsDone = true;
                }
            }
        }
    }

    public bool getTurnIsDone() { return turnIsDone; }
    public void selectACard(PlayingCard card)
    {
        selectedAction = card.GetActions()[0];
    }
}