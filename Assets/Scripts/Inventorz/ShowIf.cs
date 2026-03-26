using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShowIf : MonoBehaviour
{
    public List<GameObject> UIs;
    public List<GameObject> UIs2;
    public GameObject[] CompareObjects;
    private bool nemuzuZapnout;
    private int muzuZapnout;
    public GameObject Inventory;
    void Start()
    {
        Inventory.SetActive(true);
        UIs2.AddRange(Resources.FindObjectsOfTypeAll<GameObject>().Where(go => go.name == "Name"));
        UIs2.AddRange(Resources.FindObjectsOfTypeAll<GameObject>().Where(go => go.name == "Image"));
        UIs2.AddRange(Resources.FindObjectsOfTypeAll<GameObject>().Where(go => go.name == "Description"));
        UIs2.AddRange(Resources.FindObjectsOfTypeAll<GameObject>().Where(go => go.name == "Stats"));
        Inventory.SetActive(false);
    }
    void Update()
    {
        foreach (GameObject CompareObject in CompareObjects)
        {
            if (CompareObject.activeSelf)
            {
                nemuzuZapnout = true;
            }
            else if (!CompareObject.activeSelf)
            {
                muzuZapnout ++;
            }
        }
        if (muzuZapnout == CompareObjects.Length)
        {
            nemuzuZapnout = false;
            muzuZapnout = 0;
        }
        else if (muzuZapnout != CompareObjects.Length)
        {
            muzuZapnout = 0;
        }
        foreach (GameObject UI in UIs)
        {
            if (!nemuzuZapnout)
            {
                UI.SetActive(true);
            }
            else if (nemuzuZapnout)
            {
                UI.SetActive(false);
            }
        }
        foreach (GameObject UI2 in UIs2)
        {
            if (!nemuzuZapnout)
            {
                UI2.SetActive(false);
            }
            else if (nemuzuZapnout)
            {
                if (Inventory.activeSelf)
                {
                    UI2.SetActive(true);
                }
            }
        }
    }
}
