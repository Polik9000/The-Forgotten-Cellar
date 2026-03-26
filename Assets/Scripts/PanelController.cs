using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel; // Panel, který chceme zobrazit/skrýt

    // Tato metoda bude volána při kliknutí na tlačítko
    public void TogglePanel()
    {
        // Pokud je panel aktivní, deaktivujeme ho, jinak ho aktivujeme
        panel.SetActive(!panel.activeSelf);
    }
}
