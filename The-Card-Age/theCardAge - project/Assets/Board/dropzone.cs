using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    //public drag.slot typeOfItem = drag.slot.Weapon;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        drag d = eventData.pointerDrag.GetComponent<drag>();
        if (d != null)
        {
         //   if (typeOfItem == d.typeOfItem)
         //   {
                d.placeholderParent = this.transform;

         //   }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;
        drag d = eventData.pointerDrag.GetComponent<drag>();
        if (d != null && d.placeholderParent == this.transform)
        {
         //   if (typeOfItem == d.typeOfItem)
         //   {
                d.placeholderParent = d.hand;

         //   }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        drag d = eventData.pointerDrag.GetComponent<drag>();

        if (d != null)
        {
            //if (typeOfItem == d.typeOfItem)
            //{
                d.hand = this.transform;

            //}
        }

    }
}
