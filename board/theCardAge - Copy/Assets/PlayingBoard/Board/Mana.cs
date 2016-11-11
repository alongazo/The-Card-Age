using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour {

    public static int health = 100;
    public GameObject player;
	// Use this for initialization
	void Start () {
        InvokeRepeating("reduce", 1, 1);
	}
	
    void reduce()
    {
        health = health - 20;
        if (health < 0)
        {
            Destroy(player.gameObject);
        }
    }
	// Update is called once per frame
	void Update () {
	
	}
}
