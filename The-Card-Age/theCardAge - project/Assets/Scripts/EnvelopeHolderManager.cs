using UnityEngine;
using System.Collections;

public class EnvelopeHolderManager : MonoBehaviour {

    public GameObject cardPack;
    public Vector3 newCardPackCoords;
    private bool moveCard = false;
    private bool open = false;
    public bool matchedLocation = false;
    public void Update()
    {
        if(moveCard&&cardPack!=null)
        {
            cardPack.transform.localPosition = Vector3.Lerp(cardPack.transform.localPosition, newCardPackCoords, .05f);
            if(cardPack.transform.localPosition == newCardPackCoords)
            {
                moveCard = false;
            }
        }
        if(cardPack!= null && cardPack.transform.localPosition == newCardPackCoords&&!matchedLocation)
        {
            open = true;
            matchedLocation = true;
        }
        if(open && cardPack!= null)
        {
            cardPack.GetComponent<OpenCardPack>().spawnCards();
            open = false;
        }
    }
    public void placeCardPack(GameObject pack)
    {
        cardPack = pack;
    }
    public void moveCardPack()
    {
        moveCard = true;
        //cardPack.transform.localPosition = Vector3.Lerp(cardPack.transform.localPosition, newCardPackCoords, .1f);
    }
}
