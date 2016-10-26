using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestCardThing : MonoBehaviour {

    public Sprite background1;
    public Sprite background2;
    public GameObject prefab;

    bool boolean;
    
    Image prefab_background;


	// Use this for initialization
	void Start () {
        //prefab = (GameObject)Resources.Load("Card", typeof(GameObject));
        Debug.Log(prefab.GetComponent<Image>().sprite.name);
        prefab_background = prefab.GetComponent<Image>();
        if (prefab_background == null)
        {
            Debug.Log("It's null :<");
        }
        boolean = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Return))
        {
            if (boolean)
            {

                Debug.Log(prefab.GetComponent<Image>().sprite.name);
                prefab_background.sprite = background1;
            } else
            {

                Debug.Log(prefab.GetComponent<Image>().sprite.name);
                prefab_background.sprite = background2;
            }
            boolean = !boolean;
        }
	}
}
