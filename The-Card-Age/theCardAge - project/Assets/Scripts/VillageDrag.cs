using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class VillageDrag : MonoBehaviour
{
    public RawImage imageInDeck;
    public GameObject cardCopy;
    public string currentSetting;
    Vector3 origPos;
    Vector3 dist;
    float posX;
    float posY;
    public SpriteRenderer sprite;
   
    public void OnMouseDown()
    {
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith")
        {
            Quaternion rotateCard = Quaternion.identity;
            GameObject parent = GameObject.Find("Blacksmith");
            rotateCard.eulerAngles = new Vector3(90, 90, 0);
            GameObject newCard = Instantiate(cardCopy, transform.position, rotateCard, parent.transform) as GameObject;
            newCard.name = gameObject.name;
            origPos = transform.position;
            dist = Camera.main.WorldToScreenPoint(transform.position);
            posX = Input.mousePosition.x - dist.x;
            posY = Input.mousePosition.y - dist.y;
            sprite.sortingOrder = 1;
        }
    
    }
    public void OnMouseDrag()
    {
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith")
        {
            Vector3 curPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
            //origPos = worldPos;
            //origPos.y += .1f;
            transform.position = worldPos;
            //origPos.y -= .1f;
        }
    }
    public void OnMouseUp()
    {
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith")
        {
            if (transform.position.z < GameObject.Find("Deck Window").transform.position.z + .1)
            {
                Vector3 newPos = transform.position;
                Vector3 imagePos;
                Transform newParent = GameObject.Find("CardList").transform;
                GameObject deck = GameObject.Find("PlayerDeck");
                int deckIndex = deck.GetComponent<VillageDeck>().currentIndex;
                if (deckIndex < deck.GetComponent<VillageDeck>().playableDeck.Count)
                {
                    deck.GetComponent<VillageDeck>().playableDeck[deckIndex] = gameObject;
                    deckIndex++;
                    deck.GetComponent<VillageDeck>().currentIndex = deckIndex;
                    newPos.y += 500;
                    transform.position = newPos;
                    RawImage deckImage = Instantiate(imageInDeck) as RawImage;
                    imagePos = deckImage.rectTransform.localPosition;
                    deckImage.transform.parent = newParent;
                    deckImage.transform.localScale = new Vector3(1, 1, 1);
                    deckImage.transform.localEulerAngles = new Vector3(0, 0, 0);
                    imagePos.z = 0;
                    deckImage.rectTransform.localPosition = imagePos;
                    //Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }
            sprite.sortingOrder = 0;
        }
    }
}