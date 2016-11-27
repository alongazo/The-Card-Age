using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class CardPackDrag : MonoBehaviour {

    Vector3 origPos;
    Vector3 dist;
    float posX;
    float posY;
    public Vector3 snapIntoHolderCoords;
    public Vector3 placeIntoHolderCoords;
    private GameObject inHolder;
    private bool packPlaced = false;
    private Vector3 screenPoint, offset;
    private Ray mouseRayCast;
    private Plane movePlane;
    public void OnMouseDown()
    {
        /*Vector3 firstPoint = gameObject.transform.position;
        firstPoint.y += .2f;
        Vector3 secondPoint = firstPoint;
        secondPoint.x += 1;
        Vector3 thirdPoint = firstPoint;
        thirdPoint.z += 1;
        movePlane = new Plane(firstPoint, secondPoint, thirdPoint);*/

        //if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith")
        //{
        /*Quaternion rotateCard = Quaternion.identity;
        GameObject parent = GameObject.Find("Blacksmith");
        rotateCard.eulerAngles = new Vector3(90, 90, 0);
        GameObject newCard = Instantiate(cardCopy, transform.position, rotateCard, parent.transform) as GameObject;
        newCard.name = gameObject.name;
        origPos = transform.position;*/
        dist = Camera.main.WorldToScreenPoint(transform.position);
        origPos = transform.position;
        /*Debug.Log(transform.position);
        Debug.Log(transform.localPosition);
        Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z));
        Debug.Log(temp.x);
        Debug.Log(temp.y);
        Debug.Log(temp.z);*/
        //Debug.Log(dist.x);
        //Debug.Log(dist.y);
        //posX = Input.mousePosition.x - dist.x;
        //posY = Input.mousePosition.y - dist.y;
        //sprite.sortingOrder = 1;
        //}
    }

    public void OnMouseDrag()
    {
        //if (GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "blacksmith")
        //{
        if (!packPlaced)
        {

            /*mouseRayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (movePlane.Raycast(mouseRayCast, out rayDistance))
            {
                gameObject.transform.position = mouseRayCast.GetPoint(rayDistance);
            }*/
           
            Vector3 curPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
            //origPos = worldPos;
            //origPos.y += .1f;
            transform.position = worldPos;
            Debug.Log(transform.position);
            Debug.Log(transform.localPosition);
            Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z));
            Debug.Log(temp.x);
            Debug.Log(temp.y);
            Debug.Log(temp.z);
        }

        //origPos.y -= .1f;
        //}
    }

    public void OnMouseUp()
    {
        Vector3 curPos = transform.position;
        Debug.Log(curPos);
        Debug.Log(GameObject.Find("EnvelopeHolder").transform.position.z);
        if(curPos.z>GameObject.Find("EnvelopeHolder").transform.position.z-.5&&!packPlaced&&inHolder!=gameObject)
        {
            transform.localPosition = placeIntoHolderCoords;
            GameObject envelopeHolder = GameObject.Find("EnvelopeHolder");
            envelopeHolder.GetComponent<EnvelopeHolderManager>().placeCardPack(gameObject);
            packPlaced = true;
        }
        else
        {
            if (!packPlaced&&gameObject != inHolder)
            {
                gameObject.transform.position = origPos;
            }
        }
    }
}
