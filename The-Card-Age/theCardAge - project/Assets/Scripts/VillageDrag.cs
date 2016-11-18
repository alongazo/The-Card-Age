using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;

public class VillageDrag : MonoBehaviour
{

    Vector3 origPos;
    Vector3 dist;
    float posX;
    float posY;
   
    public void OnMouseDown()
    {
        origPos = transform.position;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
     
    }
    public void OnMouseDrag()
    {
        Vector3 curPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist.z);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
        transform.position = worldPos;
    }
    public void OnMouseUp()
    {
        /*if (transform.position.x > collidableObject.transform.position.x)
        {

        }*/
        //else
        //{
        transform.position = origPos;
        //}
    }
}