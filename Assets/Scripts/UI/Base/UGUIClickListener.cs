using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UGUIClickListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Action<PointerEventData> onClick;
    public Action<bool, PointerEventData> onPointerDown;
    public Action<bool, PointerEventData> onPointerUp;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null)
            onClick(eventData);
    }

    public void AddClick(Action<PointerEventData> action)
    {
        onClick += action;
    }

    public void RemoveClick()
    {
        onClick = null;
    }

    public void RemovePointerDown()
    {
        onPointerDown = null;
    }

    public static UGUIClickListener Get(GameObject go)
    {
        UGUIClickListener listener = go.GetComponent<UGUIClickListener>();
        if (listener == null) listener = go.AddComponent<UGUIClickListener>();
        return listener;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDown != null)
            onPointerDown(true, eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onPointerDown != null)
            onPointerDown(false, eventData);
        if (onPointerUp != null)
            onPointerUp(true, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(onPointerDown!=null)
            onPointerDown(true, eventData);
    }
}