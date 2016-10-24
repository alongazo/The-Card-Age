using System.Collections.Generic;

public class Player
{
    Deck wholeDeck;
    List<PlayingCard> hand;
   

    bool turnIsDone;
    string selectedAction;

    public Player(BossCard heroCard, string deckInfo)
    {
        string[] cardNames = deckInfo; 
        List<PlayingCard> newCards = new List<PlayingCard>();
        BossCard newHero = heroCard;
        for (int i=0; i<cardNames.Length; i++)
        {
            newCards.Add(cardDictionary[cardNames[i]]);
        }

        turnIsDone = false;
        selectedAction = "";
    }
    
    public void DrawCard(bool again)
    {
        if (numCardsInHand < 7)
        {
            hand.Add(wholeDeck.DrawCard());
            if (again)
            {
                turnIsDone = true;
            }
        }
    }
}