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
    
    
    public void OnMouseEnter()
    {
        origPos = transform.position;
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith"&&!GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused)
        {
            origPos.y += .3f;
            transform.position = origPos;
            origPos.y -= .3f;
        }
        else
        {
            transform.position = origPos;
        }
    }
    public void OnMouseExit()
    {
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith")
        {
            transform.position = origPos;
        }
    }   
    public void OnMouseDown()
    {
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith"&& !GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused)
        {
            Quaternion rotateCard = Quaternion.identity;
            GameObject parent = GameObject.Find("Blacksmith");
            rotateCard.eulerAngles = new Vector3(90, 90, 0);
            GameObject newCard = Instantiate(cardCopy, origPos, rotateCard, parent.transform) as GameObject;
            newCard.name = gameObject.name;
            newCard.transform.parent = GameObject.Find("TemporaryCardHolder").transform;
            //GameObject cardsSpawned = GameObject.Find("CardGarbage");
            //cardsSpawned.GetComponent<CardDestroyer>().addCard(newCard);
            //spawnIndex++;
            //cardsSpawned.GetComponent<CollectionManager>().spawnedIndex = spawnIndex;
            //origPos = transform.position;
            /*Vector3 temp = transform.position;
            temp.y += .3f;
            transform.position = temp;*/
            dist = Camera.main.WorldToScreenPoint(transform.position);
            posX = Input.mousePosition.x - dist.x;
            posY = Input.mousePosition.y - dist.y;
            sprite.sortingOrder = 1;
        }
    
    }
    public void OnMouseDrag()
    {
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith"&& !GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused)
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
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith"&& !GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused)
        {
            if (transform.position.z < GameObject.Find("Deck Window").transform.position.z+.4)
            {
                Vector3 newPos = transform.position;
                Vector3 imagePos;
                Transform newParent = GameObject.Find("CardList").transform;
                GameObject deck = GameObject.Find("PlayerDeck");
                int deckSize = deck.GetComponent<VillageDeck>().currentSize;
                if (deckSize < deck.GetComponent<VillageDeck>().maxSize)
                {
                    if (deck.GetComponent<VillageDeck>().playableDeck.ContainsKey(gameObject.name))
                    {
                        if (deck.GetComponent<VillageDeck>().playableDeck[gameObject.name] < deck.GetComponent<VillageDeck>().allowableCopies)
                        {
                            deck.GetComponent<VillageDeck>().playableDeck[gameObject.name] += 1;
                        }
                        else
                        {
                            Destroy(gameObject);
                            return;
                        }
                    }
                    else
                    {
                        deck.GetComponent<VillageDeck>().playableDeck.Add(gameObject.name, 1);
                    }
                    Debug.Log(deck.GetComponent<VillageDeck>().currentSize);
                    Debug.Log(gameObject.name);
                    Debug.Log(deck.GetComponent<VillageDeck>().playableDeck[gameObject.name]);
                    //deck.GetComponent<VillageDeck>().currentSize = deckIndex;
                    deck.GetComponent<VillageDeck>().currentSize += 1;
                    //newPos.y += 500;
                    transform.position = newPos;
                    RawImage deckImage = Instantiate(imageInDeck) as RawImage;
                    imagePos = deckImage.rectTransform.localPosition;
                    deckImage.transform.parent = newParent;
                    deckImage.transform.localScale = new Vector3(1, 1, 1);
                    deckImage.transform.localEulerAngles = new Vector3(0, 0, 0);
                    imagePos.z = 0;
                    deckImage.rectTransform.localPosition = imagePos;
                    Destroy(gameObject);
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