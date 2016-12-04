using UnityEngine;
using System.Collections;

public class CardCenterScreen : MonoBehaviour {
    public Transform cardCenterObject;
    public float speed = .2f;
    public bool cardClicked = false;
    public bool inCenter = false;
    private Vector3 originalPosition;
    // Use this for initialization
    void Start () {
        originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (cardClicked)
        {
            if (!inCenter)
            {
                transform.SetAsLastSibling();
                transform.position = Vector3.Lerp(transform.position, cardCenterObject.position, speed);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, originalPosition, speed);

            }
        }
        if(transform.position==cardCenterObject.position)
        {
            cardClicked = false;
            inCenter = true;
        }
        if(transform.position==originalPosition)
        {
            cardClicked = false;
            inCenter = false;
        }
    }
    public void moveCard()
    {
        cardClicked = true;
    }

}
