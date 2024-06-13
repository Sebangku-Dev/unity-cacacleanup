using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropSlot : MonoBehaviour, IDropHandler
{
    public Hint hint;
    public string triggerObject;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (transform.childCount == 0)
            {
                if ((triggerObject != null && eventData.pointerDrag.gameObject.name == triggerObject) || triggerObject == null)
                {
                    GameObject dropped = eventData.pointerDrag;
                    DragAndDrop dragAndDrop = dropped.GetComponent<DragAndDrop>();
                    dragAndDrop.parentAfterDrag = transform;
                    eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                }
            }

        }
    }
}
