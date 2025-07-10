using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHandler : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform _parentRect;
    private Vector2 _basePos;
    private Vector2 _startPos;
    private Vector2 _moveOffset;
    private void Awake()
    {
        _parentRect = transform.parent.GetComponent<RectTransform>();
        
        // _parentRect.SetAsFirstSibling();
        // _parentRect.SetAsLastSibling();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _parentRect.SetAsLastSibling();
        _basePos = _parentRect.anchoredPosition;
        _startPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _moveOffset = eventData.position - _startPos;
        _parentRect.anchoredPosition = _basePos + _moveOffset;
    }
}
