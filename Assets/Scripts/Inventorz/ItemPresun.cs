using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
//problem je v tom ze se u veci co jsou v inventari(at uz v truhle nebo normalne) tak se jim pri presunu meni koncovka
public class ItemPresun : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool IsBeingDragged { get; private set; } = false;
    public GameObject Inventory;
    public GameObject ChestINV;
    public bool dragged;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        DragManager dragManager = FindFirstObjectByType<DragManager>().GetComponent<DragManager>();
    }
    void Update()
    {
        ItemToSlot itemToSlot = FindAnyObjectByType<ItemToSlot>();
        DragManager dragManager = FindAnyObjectByType<DragManager>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (gameObject.transform.position.z != -10000)
        {     
        canvasGroup.blocksRaycasts = false;
        IsBeingDragged = true;
        ItemToSlot itemToSlot = FindFirstObjectByType<ItemToSlot>().GetComponent<ItemToSlot>();
        itemToSlot.StartPosition = gameObject.transform.position;
        }
    }
    public void OnDrag(PointerEventData eventData){}
    public void OnEndDrag(PointerEventData eventData)
    {   
        if (gameObject.transform.position.z != -10000)
        {     
        canvasGroup.blocksRaycasts = true;
        IsBeingDragged = false;
        ItemToSlot itemToSlot = FindFirstObjectByType<ItemToSlot>().GetComponent<ItemToSlot>();
        itemToSlot.udelejtoto = false;
        itemToSlot.Aitem = gameObject;
        itemToSlot.ItemToSloting();
        }
    }

    // Metody volané z DragManageru
    public void OnBeginDragCustom()
    {
        if (gameObject.transform.position.z != -10000)
        {     
        ItemToSlot itemToSlot = FindAnyObjectByType<ItemToSlot>();
        dragged = true;
        }
    }

    public void OnDragCustom()
    {
        if (gameObject.transform.position.z != -10000)
        {     
        dragged = true;
        rectTransform.position = Input.mousePosition;
        }
    }

    public void OnEndDragCustom()
    {
        if (gameObject.transform.position.z != -10000)
        {     
        ItemToSlot itemToSlot = FindAnyObjectByType<ItemToSlot>();
        dragged = false;
        }
    }
}