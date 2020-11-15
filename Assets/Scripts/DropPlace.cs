using UnityEngine;
using UnityEngine.EventSystems;

public class DropPlace : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var card = eventData.pointerDrag.GetComponent<CardMove>();
        if (!card) return;
        card.DefaultParent = transform;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null) return;

        var card = eventData.pointerDrag.GetComponent<CardMove>();
        if (card)
            card.DefaultTempCardParent = transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(eventData.pointerDrag == null) return;

        var card = eventData.pointerDrag.GetComponent<CardMove>();
        if (card && card.DefaultTempCardParent == transform)
            card.DefaultTempCardParent = card.DefaultParent;
    }
}