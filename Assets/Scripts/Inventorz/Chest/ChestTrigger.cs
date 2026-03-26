using System.Linq;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public GameObject chestUI; // UI truhly
    public Texture openedTexture; // Textura po otevření
    public Texture originalTexture; // Původní textura
    public bool isPlayerNear = false;
    public bool isOpen = false;
    public GameObject PorovnavaciObjekt;
    private Renderer chestRenderer;
    void Start()
    {
        gameObject.transform.position = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y, 35);
        chestUI = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(obj => obj.name == "ChestMenu");
        PorovnavaciObjekt = Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(obj => obj.name == "InventoryMenu");
        chestRenderer = GetComponent<Renderer>();
        originalTexture = chestRenderer.material.mainTexture; // Uložení původní textury
        chestUI.SetActive(false); // UI je na začátku skryté   
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && PorovnavaciObjekt.activeSelf == false)
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                CloseChest();
            }
        }
    }
    void OpenChest()
    {
        ItemToSlot itemToSlot = FindAnyObjectByType<ItemToSlot>();
        Zkouska2 zkouska2 = FindAnyObjectByType<Zkouska2>();
        chestRenderer.material.mainTexture = openedTexture; // Změna textury
        isOpen = true;
        itemToSlot.ChestOpen = true;
        itemToSlot.OpenedChest = gameObject;
        zkouska2.openedChest = gameObject;
        chestUI.SetActive(true);
    }

    void CloseChest()
    {
        ItemToSlot itemToSlot = FindAnyObjectByType<ItemToSlot>();
        chestRenderer.material.mainTexture = originalTexture; // Vrácení původní textury
        isOpen = false;
        itemToSlot.ChestOpen = false;
        itemToSlot.OpenedChest = null;
        chestUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) // Opravený parametr
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) // Když hráč odejde od truhly
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}