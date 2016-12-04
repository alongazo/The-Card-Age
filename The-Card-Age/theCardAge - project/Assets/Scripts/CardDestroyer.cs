using UnityEngine;
using System;
using System.Collections.Generic;

public class CardDestroyer : MonoBehaviour {
    public GameObject shopCollection;
    public GameObject blacksmithCollection;
    public void destroyCards()
    {
        foreach(Transform card in transform)
        {
            GameObject.Destroy(card.gameObject);
        }
    }
    public void sortCollection()
    {
        shopCollection.GetComponent<CollectionManager>().cardCollection.Sort(compareListByName);
        shopCollection.GetComponent<CollectionManager>().spawnSet();
        blacksmithCollection.GetComponent<CollectionManager>().cardCollection.Sort(compareListByName);
        blacksmithCollection.GetComponent<CollectionManager>().spawnSet();

    }
    public void unblock()
    {
        GameObject.Find("SettingTracker").GetComponent<TrackSetting>().blocked = false;
    }
    private static int compareListByName(GameObject obj1, GameObject obj2)
    {
        return obj1.name.CompareTo(obj2.name);
    }

}
