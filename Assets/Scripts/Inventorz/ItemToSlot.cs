using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Linq;
public class ItemToSlot : MonoBehaviour
{
    private GameObject[] items;
    private GameObject[] slots;
    public GameObject Aitem;
    public Vector3 StartPosition;
    public bool PresunJinam;
    public float areaSize = 100f; // Velikost čtverce pro hledání slotů
    public GameObject inventory;
    public GameObject ChestINV;
    public GameObject[] Parents;
    public bool ChestOpen;  
    public GameObject OpenedChest;
    public bool udelejtoto;
    public bool start;
    public bool nemuzu1;
    public bool nemuzu2;
    public int x;
    public int y;
    void Start()
    {
        start = true;
        inventory.SetActive(true);
        ChestINV.SetActive(true);
        foreach (GameObject Parent in Parents)
        {
            if (Parent == null) continue;
            LayoutRebuilder.ForceRebuildLayoutImmediate(Parent.GetComponent<RectTransform>());
        }
        LoadItemsAndSlots();
        foreach (GameObject item in items)
        { 
            Aitem = item;
            ItemToSloting();
            if ((item.name.Contains("chest-1") || item.name.Contains("chest-2")) && start)
            {
                item.name = item.name;
            }
            else if ((!item.name.Contains("chest-1") || !item.name.Contains("chest-2")) && start)
            {
                item.name = item.name.Replace(item.name.PartAfter(':'), FindNearestObject(slots, item).name.PartAfter('_'));
            }
            else
            {
                item.name = item.name.Replace(item.name.PartAfter(':'), FindNearestObject(slots, item).name.PartAfter('_'));
            }
        }
        ChestINV.SetActive(false);
        inventory.SetActive(false);
    }
    void Update()
    {
        start = false;
        ItemPresun  itemPresun = FindAnyObjectByType<ItemPresun>();
    }
    public void ItemToSloting()
    {
        LoadItemsAndSlots();
        ProcessItemMovement();
        if (Aitem.name.PartAfter(':') != FindNearestObject(slots,Aitem).name.PartAfter('_'))
        {
            if (ChestOpen && FindNearestObject(slots,Aitem).name.Contains("Slot_chest") && OpenedChest != null)
            {
                Aitem.name = Aitem.name.Replace(Aitem.name.PartAfter(':'),FindNearestObject(slots,Aitem).name.PartAfter('_') + OpenedChest.name.PartAfter('-'));
            }
            else if ((Aitem.name.Contains("chest-1") || Aitem.name.Contains("chest-2")) && start)
            {
                Aitem.name = Aitem.name;
            }
            else
            {
                Aitem.name = Aitem.name.Replace(Aitem.name.PartAfter(':'),FindNearestObject(slots,Aitem).name.PartAfter('_'));
            }
        }
    }
    private void LoadItemsAndSlots()
    {
        items = GameObject.FindGameObjectsWithTag("item");
        slots = GameObject.FindGameObjectsWithTag("INVSlot").Concat(GameObject.FindGameObjectsWithTag("ChestSlot")).Concat(GameObject.FindGameObjectsWithTag("HotbarSlot")).ToArray();
    }
    public void ProcessItemMovement()
    {
        if (!udelejtoto)
        {
            GameObject nearestSlot = FindNearestObject(slots, Aitem);
            if (nearestSlot.name.Contains("Gear"))
            {
                if (Aitem.name.Contains("cap"))
                {
                    if (nearestSlot.name.Contains("Gear"))
                    {
                        Aitem.transform.position = GameObject.Find("Slot_Gear4IN").transform.position;
                    }
                    else
                    {
                        Aitem.transform.position = nearestSlot.transform.position;
                        PresunJinam = false;
                    }
                }
                else if (Aitem.name.Contains("pants"))
                {
                    if (nearestSlot.name.Contains("Gear"))
                    {
                        Aitem.transform.position = GameObject.Find("Slot_Gear6IN").transform.position;
                    }
                    else
                    {
                        Aitem.transform.position = nearestSlot.transform.position;
                        PresunJinam = false;
                    }
                }
                else if (Aitem.name.Contains("shoe") && Aitem.name.Contains("L"))
                {
                    if (nearestSlot.name.Contains("Gear"))
                    {
                        Aitem.transform.position = GameObject.Find("Slot_Gear7IN").transform.position;
                    }
                    else
                    {
                        Aitem.transform.position = nearestSlot.transform.position;
                        PresunJinam = false;
                    }
                }
                else if (Aitem.name.Contains("shoe") && Aitem.name.Contains("R"))
                {
                    if (nearestSlot.name.Contains("Gear"))
                    {
                        Aitem.transform.position = GameObject.Find("Slot_Gear8IN").transform.position;
                    }
                    else
                    {
                        Aitem.transform.position = nearestSlot.transform.position;
                        PresunJinam = false;
                    }
                }
                else
                {
                    Aitem.transform.position = StartPosition;
                    PresunJinam = false;
                }
            }
            else
            {
                Aitem.transform.position = nearestSlot.transform.position;
            }
        }
        else 
        {
            Aitem.transform.position = StartPosition;
            Aitem.transform.position = new Vector3 (Aitem.transform.position.x, Aitem.transform.position.y, -10000);
        }
        nemuzu1 = false;
        LoadItemsAndSlots();
    }

    public GameObject FindNearestObject(GameObject[] objects, GameObject target)
    {
        if(ChestINV.activeSelf)
        {
            areaSize = 65;
        }
        else if (inventory.activeSelf)
        {
            areaSize = 50;
        }
        else if (!ChestINV.activeInHierarchy && !inventory.activeInHierarchy)
        {
            areaSize = 60;
        }
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;
        
        foreach (GameObject obj in objects)
        {
            if (obj == null) continue;
            Vector3 diff = obj.transform.position - target.transform.position;
            if (Mathf.Abs(diff.x) < areaSize / 2 && Mathf.Abs(diff.y) < areaSize / 2)
            {
                float distance = diff.sqrMagnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = obj;
                }
            }
        }
        PresunJinam = nearest != null;
        return nearest;
    }

    private void OnDrawGizmos()
    {
        if (items == null) return;
        Gizmos.color = Color.green;
        foreach (GameObject item in items)
        {
            if (item != null)
            {
                Gizmos.DrawWireCube(item.transform.position, new Vector3(areaSize, areaSize, 0));
            }
        }
    }
    public static string PartBetween(string source, char startChar, char endChar)
    {
        int start = source.IndexOf(startChar);
        int end = source.IndexOf(endChar, start + 1); // hledáme konec až za začátkem

        if (start != -1 && end != -1 && end > start)
        {
            return source.Substring(start + 1, end - start - 1);
        }

        return ""; // nebo null, podle potřeby
    }
}
