using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragScript : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform draggingObjectRectTransform;
    private Vector3 velocity = Vector3.zero;

    [SerializeField]
    private float dampingSpeed = 5f;
    private void Awake()
    {
        draggingObjectRectTransform = transform as RectTransform;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
       if(RectTransformUtility.ScreenPointToLocalPointInRectangle(draggingObjectRectTransform,eventData.
           position,eventData.pressEventCamera,out var globalMousePosition))
        {
            draggingObjectRectTransform.position = Vector3.SmoothDamp(draggingObjectRectTransform.position,
                globalMousePosition, ref velocity, dampingSpeed);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
