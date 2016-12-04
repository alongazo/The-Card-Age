using UnityEngine;
using System;
using System.Collections.Generic;

public class SpawnMap : MonoBehaviour {

    public List<GameObject> tiles;
    public Vector3 startCoords;
    public GameObject parent;
	// Use this for initialization
	void Start () {
        int rand = (int)Mathf.Round(UnityEngine.Random.Range(0, tiles.Count));
        
        Quaternion rotateSprite = Quaternion.identity;
        //rotateSprite.eulerAngles = new Vector3(90, 0, 0);
        float originalStartCoordy = startCoords.y;
        for(int i=0; i<18;i++)
        {
            for(int j=0;j<12;j++)
            {
                Instantiate(tiles[rand], startCoords, rotateSprite, parent.transform);
                rand = (int)Mathf.Round(UnityEngine.Random.Range(0, tiles.Count));
                //Debug.Log("Rand");
                //Debug.Log(rand);
                startCoords.y += 1.5f;
            }
            startCoords.y = originalStartCoordy;
            startCoords.x += 1.5f;
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
