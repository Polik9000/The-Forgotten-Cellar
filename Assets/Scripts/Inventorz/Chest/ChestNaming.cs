using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChestNaming : MonoBehaviour
{
    public List<GameObject> Chests;
    public GameObject ChestParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Chests.AddRange(GameObject.FindGameObjectsWithTag("Chest"));
        foreach (GameObject chest in Chests)
        {
            chest.name = chest.name.Replace(chest.name.PartAfter('-'), chest.transform.GetSiblingIndex().ToString());
        }
    }
}
