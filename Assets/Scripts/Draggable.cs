using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [HideInInspector] public RectTransform m_RectTransform;
    [HideInInspector] public Transform m_LastParent;
    protected CanvasGroup _CanvasGroup;
    public bool m_IsSuccessfullyDropped;

    private void Start()
    {
        // Get rectTransform 
        m_RectTransform = transform as RectTransform;
        // add and take Ref of canvas group 
        _CanvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Makw blocks raycasts  false
        ChangeBlocksRaycasts(false);
        // Reset 
        m_IsSuccessfullyDropped = false;
        // Ref
        m_LastParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Start dragging
        m_RectTransform.anchoredPosition += eventData.delta;
        // Set this to root > canvas
        ChangeParent(transform.root);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // If we not Successfully Dropped the card
        if (!m_IsSuccessfullyDropped)
        {
            // Move card back to last parent
            LeanTween.move(transform.gameObject, m_LastParent.position, 0.2f).setOnComplete(() => ChangeParent(m_LastParent));
        }
        // Make blocks raycasts true after end drop
        ChangeBlocksRaycasts(true);
    }

    protected void ChangeBlocksRaycasts(bool active) => _CanvasGroup.blocksRaycasts = active;
    private void ChangeParent(Transform t) => transform.SetParent(t);

}
