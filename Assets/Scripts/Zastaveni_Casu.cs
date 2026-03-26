using JetBrains.Annotations;
using UnityEngine;

public class Zastaveni_Casu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject InventoryMenu;
    public GameObject ChestMenu;
    public GameObject PauseMenu;
    public GameObject Settings;
    public GameObject Tutorial;
    void Update()
    {
        if (InventoryMenu.activeSelf || ChestMenu.activeSelf || PauseMenu.activeSelf || Settings.activeSelf || Tutorial.activeSelf)
        {
            Time.timeScale = 0; // Zastaví čas, když je menu otevřené
        }
        else
        {
            Time.timeScale = 1; // Obnoví čas, když je menu zavřené
        }
    }
}
