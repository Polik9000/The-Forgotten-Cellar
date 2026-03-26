using Unity.VisualScripting;
using UnityEngine;

public class OpenButton : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject ChestInventory;
    public void OnClick()
    {
        if (!ChestInventory.activeSelf)
        {
            Inventory.SetActive(true);
        }
    }
}