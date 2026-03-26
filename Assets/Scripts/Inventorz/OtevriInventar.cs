using UnityEngine;

public class OtevriInventar : MonoBehaviour 
{
    public GameObject InventoryMenu;
    private bool menuActivated;
    void Start()
    {
        menuActivated = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !menuActivated)
        {
            Time.timeScale = 0; // Místo úplného zastavení hry ji jen zpomalíme
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && menuActivated)
        {   
            Time.timeScale = 1;
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }
    }
}