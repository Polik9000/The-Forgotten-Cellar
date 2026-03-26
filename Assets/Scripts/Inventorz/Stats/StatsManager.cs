using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class StatsManager : MonoBehaviour
{
    public List<GameObject> ItemsInGear;
    public List<GameObject> EquipedItems;
    public int Defense;
    public int MaxHealth;
    public int TotalDmg;
    public float DmgModifierPercent;
    public int DmgModifierUnitary;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Defense = 0;
        DmgModifierPercent = 1;
        DmgModifierUnitary = 0;
        ItemsInGear = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        DragManager dragManager = FindAnyObjectByType<DragManager>();
        ItemsInGear = new List<GameObject>();
        Defense = 0;
        MaxHealth = 0;
        if (dragManager.NameNow != null && dragManager.NameNow.GetComponent<TMP_Text>().text.Contains("Dagger") /* || ... */ )
        {
            if (dragManager.NameNow.GetComponent<TMP_Text>().text.Contains("Dagger"))
            {
                TotalDmg = (int)Math.Round((2 + DmgModifierUnitary) * DmgModifierPercent);
            }
        }
        else
        {
            TotalDmg = 0;
        }
        ItemsInGear.AddRange(GameObject.FindGameObjectsWithTag("item").ToArray());
        for (int i = ItemsInGear.Count - 1; i >= 0; i--)
        {
            if (!ItemsInGear[i].name.Contains("Gear"))
            {
                ItemsInGear.RemoveAt(i);
            }
        }
        foreach (GameObject item in ItemsInGear)
        {
            if(item.name.Contains("pants_of_stealth"))
            {
                Defense += 1;
            }
            if(item.name.Contains("cap_of_stealth"))
            {
                Defense += 1;
            }
            if(item.name.Contains("shoe_of_stealthR"))
            {
                Defense += 1;
            }
            if(item.name.Contains("shoe_of_stealthL"))
            {
                Defense += 1;
            }
        }
        Player player = FindAnyObjectByType<Player>();
        MaxHealth += (int)player.MaxHp;
    }
}