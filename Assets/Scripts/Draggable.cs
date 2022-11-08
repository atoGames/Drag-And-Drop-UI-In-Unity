using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [HideInInspector] public Transform m_LastParent;
    protected CanvasGroup _CanvasGroup;
    public bool m_IsSuccessfullyDropped;

    private void Start()
    {
        // add and take Ref of canvas group 
        _CanvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Makw blocks raycasts  false
        ChangeBlocksRaycasts(false);
        // Reset 
        m_IsSuccessfullyDropped = false;
        // Ref last parent
        m_LastParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Start dragging
        transform.localPosition += (Vector3)eventData.delta;
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
