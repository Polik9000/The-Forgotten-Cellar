using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using System.Windows;
using System;
using UnityEngine.Assertions.Must;
public class StayHere : MonoBehaviour
{
    private DragManager dragManager;
    private bool A;
    public GameObject Inventory;
    public int Width;
    public int Height;

    [System.Obsolete]
    void Start()
    {
        dragManager = FindAnyObjectByType<DragManager>();
        A = true;
        GameObject[] allObjects = FindObjectsOfType<GameObject>(true); // true umožňuje hledání deaktivovaných objektů

        foreach (GameObject obj in allObjects)
        {
            // Pokud je to ten správný objekt, který hledáte
            if (obj.name.Contains("InventoryMenu"))
            {
                Inventory = obj;
                // Zde můžete provádět další operace s tímto objektem
            }
        }
        Width = Screen.width;
        Height = Screen.height;
        if (dragManager.NameNow == null && dragManager.DescriptionNow == null && dragManager.StatsNow == null && dragManager.ImageNow == null)
        {
            gameObject.transform.position = new Vector3 (gameObject.transform.position.x,gameObject.transform.position.y, -10000);
        }
    }
    void Update()
    {
        if (A && Inventory.activeSelf)
        {
            A = false;
            if (gameObject.name.Contains("Name"))
            {
                SetWorldPosition(gameObject, new Vector3(Width*0.7534f, Height*0.818f, -10000));
            }
            if (gameObject.name.Contains("Description"))
            {
                SetWorldPosition(gameObject, new Vector3(Width*0.714f,Height*0.6f, -10000));
            }
            if (gameObject.name.Contains("Image"))
            {
                SetWorldPosition(gameObject, new Vector3(Width*0.651f, Height*0.827f, -10000));
            }
            if (gameObject.name.Contains("Stats"))
            {
                SetWorldPosition(gameObject, new Vector3(Width*0.709f, Height*0.504f, -10000));
            }
        }
        if (!A && !Inventory.activeSelf)
        {
            A = true;
        }
        if (dragManager.NameNow == null && dragManager.DescriptionNow == null && dragManager.StatsNow == null && dragManager.ImageNow == null)
        {
            gameObject.transform.position = new Vector3 (gameObject.transform.position.x,gameObject.transform.position.y, -10000);
        }
        if (dragManager != null)
        {
            Zapni();
            Vypni();
        }
    }
    public void Zapni()
    {
        if (dragManager.NameNow != null && dragManager.NameNow.transform.parent == gameObject.transform.parent && gameObject.name.Contains("Name"))
        {
            SetWorldPosition(gameObject, new Vector3(Width*0.7534f, Height*0.818f, 0));
        }
        if (dragManager.DescriptionNow != null && dragManager.DescriptionNow.transform.parent == gameObject.transform.parent && gameObject.name.Contains("Description"))
        {
            SetWorldPosition(gameObject, new Vector3(Width*0.714f,Height*0.6f, 0));
        }
        if (dragManager.ImageNow != null && dragManager.ImageNow.transform.parent == gameObject.transform.parent && gameObject.name.Contains("Image"))
        {
            SetWorldPosition(gameObject, new Vector3(Width*0.651f, Height*0.827f, 0));
        }
        if (dragManager.StatsNow != null && dragManager.StatsNow.transform.parent == gameObject.transform.parent && gameObject.name.Contains("Stats"))
        {
            SetWorldPosition(gameObject, new Vector3(Width*0.709f, Height*0.504f, 0));
        }
    }

    public void Vypni()
    {
        if (dragManager.NameNow != null && dragManager.NameNow.transform.parent != gameObject.transform.parent && gameObject.name.Contains("Name"))
        {
            SetWorldPosition(gameObject, new Vector3(Width*0.7534f, Height*0.818f, -10000));
        }
        if (dragManager.DescriptionNow != null && dragManager.DescriptionNow.transform.parent != gameObject.transform.parent && gameObject.name.Contains("Description"))
        {
            SetWorldPosition(gameObject, new Vector3(Width*0.714f,Height*0.6f, -10000));
        }
        if (dragManager.ImageNow != null && dragManager.ImageNow.transform.parent != gameObject.transform.parent && gameObject.name.Contains("Image"))
        {
            SetWorldPosition(gameObject, new Vector3(Width*0.651f, Height*0.827f, -10000));
        }
        if (dragManager.StatsNow != null && dragManager.StatsNow.transform.parent != gameObject.transform.parent && gameObject.name.Contains("Stats"))
        {
            SetWorldPosition(gameObject, new Vector3(Width*0.709f, Height*0.504f, -10000));
        }
    }
    void SetWorldPosition(GameObject me, Vector3 worldPosition)
    {
        me.transform.position = worldPosition;
    }
}