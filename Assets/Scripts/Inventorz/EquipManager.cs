using System.Collections.Generic;
using System.Data;
using Unity.Multiplayer.Center.Common;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipManager : MonoBehaviour
{
    private GameObject equip1_1;
    private GameObject equip1_2;
    private GameObject equip1_3;
    private GameObject equip1_4;
    private GameObject equip2_1;
    private GameObject equip2_2;
    private GameObject equip2_3;
    private GameObject equip2_4;
    private List<GameObject> items;
    private int Sellected;
    public GameObject Inventory;
    public GameObject ChestINV;
    void Start()
    {    
        items = new List<GameObject>();
        items.AddRange(GameObject.FindGameObjectsWithTag("item"));
        Sellected = 0;
    }
    void Update()
    {
        foreach (GameObject item in items)
        {
            if (item.name.Contains("equip1.1"))
            {
                equip1_1 = item;
            }
            else if (item.name.Contains("equip1.2"))
            {
                equip1_2 = item;
            }
            else if (item.name.Contains("equip1.3"))
            {
                equip1_3 = item;
            }
            else if (item.name.Contains("equip1.4"))
            {
                equip1_4 = item;
            }
            else if (item.name.Contains("equip2.1"))
            {
                equip2_1 = item;
            }
            else if (item.name.Contains("equip2.2"))
            {
                equip2_2 = item;
            }
            else if (item.name.Contains("equip2.3"))
            {
                equip2_3 = item;
            }
            else if (item.name.Contains("equip2.4"))
            {
                equip2_4 = item;
            }
        }     
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Sellected = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Sellected = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Sellected = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Sellected = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Sellected = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Sellected = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Sellected = 7;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Sellected = 8; 
        }
        if (Sellected >=1 && Sellected <= 4)
        {
            GameObject.Find("Slot_equip1." + Sellected + "HO").GetComponent<Image>().color = new Color32(255,255,255,50);
        }
        else if (Sellected >=5 && Sellected <= 8)
        {
            GameObject.Find("Slot_equip2." + (Sellected-4) + "HO").GetComponent<Image>().color = new Color32(255,255,255,50);
        }
        for (int i = 1; i < 9; i++)
        {
            if (Sellected != i && i <= 4)
            {
                GameObject.Find("Slot_equip1." + i + "HO").GetComponent<Image>().color = new Color32(255,255,255,0);
            }
            else if (Sellected != i && i > 4)
            {
            GameObject.Find("Slot_equip2." + (i-4) + "HO").GetComponent<Image>().color = new Color32(255,255,255,0);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Inventory.activeSelf && !ChestINV.activeSelf)
        {
            if (Sellected == 1)
            {
                equip1_1.GetComponent<ItemAbillity>().Called = true;
            }
            else if (Sellected == 2)
            {
                equip1_2.GetComponent<ItemAbillity>().Called = true;
            }
            else if (Sellected == 3)
            {
                equip1_3.GetComponent<ItemAbillity>().Called = true;
            }
            else if (Sellected == 4)
            {
                equip1_4.GetComponent<ItemAbillity>().Called = true;
            }
            else if (Sellected == 5)
            {
                equip2_1.GetComponent<ItemAbillity>().Called = true;
            }
            else if (Sellected == 6)
            {
                equip2_2.GetComponent<ItemAbillity>().Called = true;
            }
            else if (Sellected == 7)
            {
                equip2_3.GetComponent<ItemAbillity>().Called = true;
            }
            else if (Sellected == 8)
            {
                equip2_4.GetComponent<ItemAbillity>().Called = true;
            }
        }
    }   
}
