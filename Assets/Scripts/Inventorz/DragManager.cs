using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using Unity.Mathematics;

public class DragManager : MonoBehaviour
{
    public GameObject ChestINV;
    public GameObject Inventory;
    private List<ItemPresun> draggableItems = new();
    public ItemPresun currentDraggingItem = null;
    public GameObject NameNow;
    public GameObject ImageNow;
    public GameObject DescriptionNow;
    public GameObject StatsNow;
    void Start()
    {
        FindAllDraggableItems();
    }

    void Update()
    {
        DetectDraggingItem();
    }

    void FindAllDraggableItems()
    {
        draggableItems.Clear();
        GameObject[] allItems = GameObject.FindGameObjectsWithTag("item");

        foreach (GameObject item in allItems)
        {
            if (item.TryGetComponent<ItemPresun>(out var dragScript))
            {
                draggableItems.Add(dragScript);
            }
        }
    }

    void DetectDraggingItem()
    {
        foreach (ItemPresun item in draggableItems)
        {
            if (item.transform.position.z != -10000)
            {
                if (item.IsBeingDragged)
                {
                    if (currentDraggingItem != item)
                    {
                        NameNow = item.transform.Find("Name")?.gameObject;
                        ImageNow = item.transform.Find("Image")?.gameObject;
                        DescriptionNow = item.transform.Find("Description")?.gameObject;
                        StatsNow = item.transform.Find("Stats")?.gameObject;
                        currentDraggingItem = item;
                        currentDraggingItem.OnBeginDragCustom();
                    }
                    currentDraggingItem?.OnDragCustom();
                    return;
                }
            }
        }
        if (currentDraggingItem != null)
        {
            currentDraggingItem.OnEndDragCustom();
            currentDraggingItem = null;
        }
    }
}