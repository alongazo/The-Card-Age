using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class RemoveFromDeck : MonoBehaviour {
    public Image xImage;
    public string cardName;
	public void OnMouseEnter()
    {
        Image x = Instantiate(xImage, transform) as Image;
        xImage.color = new Vector4(255, 0, 0, 255);
        x.transform.localPosition = new Vector3(-4.5f, .07f, -.004f);
        x.transform.localEulerAngles = new Vector3(0, 0, 0);
        x.rectTransform.sizeDelta = new Vector2(.12f, .7f);
        /*GameObject xImage = new GameObject("xImage");
        xImage = Instantiate(xImage, new Vector3(0, 0, 0), Quaternion.identity, transform) as GameObject;
        SpriteRenderer render = xImage.AddComponent<SpriteRenderer>();
        render.sprite = removeFromDeckImage;
        render.sortingOrder = 2;
        xImage.transform.localPosition = new Vector3(4.41f, 0.06f, 0.018f);
        xImage.transform.localScale = new Vector3(2, 1, 25);
        xImage.transform.localEulerAngles = new Vector3(0, 0, 0);*/
    }
    public void OnMouseExit()
    {
        foreach(Transform x in transform)
        {
            GameObject.Destroy(x.gameObject);
        }
    }
}
