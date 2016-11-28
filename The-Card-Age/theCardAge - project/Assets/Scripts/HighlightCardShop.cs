using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class HighlightCardShop : MonoBehaviour {
    public bool selected = false;
    public GameObject highlight;
    public void OnMouseDown()
    {
        if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "shop-sell")
        {
            GameObject selectedCards = GameObject.Find("ShopSelectedCards");
            Transform parent = GameObject.Find("Shop").transform;

            if (selected)
            {
                selected = false;
                selectedCards.GetComponent<SellManager>().cardsSelected.Remove(gameObject);
                Destroy(GameObject.Find(highlight.name));
            }
            else
            {
                selected = true;
                selectedCards.GetComponent<SellManager>().cardsSelected.Add(gameObject);
                highlight = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation, parent) as GameObject;
                highlight.name = "Highlighted " + gameObject.name;
                Vector3 position = highlight.transform.position;
                position.y -= .000001f;
                highlight.transform.position = position;
                Vector3 scale = highlight.transform.localScale;
                scale.x += .015f;
                scale.y += .01f;
                //scale.z += 1f;
                highlight.transform.localScale = scale;
                highlight.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f, 1f);
            }
        }
        
    }
}
