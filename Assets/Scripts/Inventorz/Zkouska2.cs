using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

public class Zkouska2 : MonoBehaviour
{
    public List<GameObject> items;
    public GameObject openedChest;
    public GameObject ChestINV;
    public GameObject inventory;
    public GameObject hotbar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        items.AddRange(GameObject.FindGameObjectsWithTag("item"));
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject item in items)
        {
            if (inventory.activeInHierarchy && item.transform.position.z != 0 && !item.name.Contains("IN") && !item.name.Contains("chest-"))
            {
                item.name = item.name.Replace('H', 'I').Replace('O', 'N').Replace('C', 'I').Replace('H', 'N');
            }
            if (ChestINV.activeInHierarchy && item.transform.position.z != 0 && !item.name.Contains("CH"))
            {
                item.name = item.name.Replace('H', 'C').Replace('O', 'H').Replace('I', 'C').Replace('N', 'H');
            }
            if (hotbar.activeSelf && item.transform.position.z != 0 && !item.name.Contains("chest-") && !item.name.Contains("HO"))
            {
                item.name = item.name.Replace('H', 'O').Replace('C', 'H').Replace('I', 'H').Replace('N', 'O');
            }
            if(GameObject.Find("Slot_" + item.name.PartAfter(':')) != null)
            {
                item.transform.position = new Vector3 (GameObject.Find("Slot_" + item.name.PartAfter(':')).transform.position.x,GameObject.Find("Slot_" + item.name.PartAfter(':')).transform.position.y, 0);
            }
            else if (ChestINV.activeInHierarchy && openedChest != null && item.name.PartAfter(':').Contains("chest-") && openedChest.name.PartAfter('-').Contains(item.name.PartAfter('H')))
            {
                item.transform.position = new Vector3(GameObject.Find("Slot_" + GetStringBetween(item.name, ':','H') + "H").transform.position.x, GameObject.Find("Slot_" + GetStringBetween(item.name, ':', 'H') + "H").transform.position.y, 0);
            }
            else
            {
                item.transform.position = new Vector3 (item.transform.position.x,item.transform.position.y,-10000);
            }
        }
    }
    public static string GetStringBetween(string text, char start, char end)
    {
        int startIndex = text.IndexOf(start);
        int endIndex = text.IndexOf(end, startIndex + 1);

        if (startIndex != -1 && endIndex != -1 && endIndex > startIndex)
        {
            return text.Substring(startIndex + 1, endIndex - startIndex - 1);
        }
        return string.Empty; // Vrátí prázdný řetězec, pokud znaky nejsou nalezeny
    }
}
