using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Droppable : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag)
        {
            // Ref
            var _DragObj = eventData.pointerDrag.transform as RectTransform;
            // For swap part
            var _Draggable = _DragObj.GetComponent<Draggable>();
            // Is this transform don't has child
            if (this.transform.childCount == 0)
            {
                _DragObj.SetParent(this.transform);
                // Reset position
                _DragObj.anchoredPosition = Vector3.zero;
            }
            else
            {
                // Swap part
                CardSwap(_Draggable);
            }
            // Is successfully dropped?
            _Draggable.m_IsSuccessfullyDropped = true;
        }
    }

    protected void CardSwap(Draggable draggable)
    {
        // Current/Dragging card
        draggable.transform.SetParent(transform.GetChild(0).parent);

        // Move card to new position
        LeanTween.move(transform.GetChild(0).gameObject, draggable.m_LastParent.position, 0.2f).setOnComplete(() =>
        transform.GetChild(0).SetParent(draggable.m_LastParent)
        );
        // Reset local position 
        draggable.transform.localPosition = Vector3.zero;
    }


}
