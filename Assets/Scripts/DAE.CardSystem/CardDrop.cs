using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Card dropped");
        if (eventData.pointerDrag != null)
        {
            //eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
            //    GetComponent<RectTransform>().anchoredPosition;
            if (eventData.pointerDrag != null)
            {
                Debug.Log("Dropped object was: " + eventData.pointerDrag);
            }
        }
    }
}
