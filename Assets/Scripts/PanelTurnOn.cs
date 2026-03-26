using UnityEngine;
public class PanelTurnOn : MonoBehaviour
{
    public static PanelTurnOn instance;
    public GameObject ESC;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Zajistí, že PanelManager přežije mezi scénami
        }
        else
        {
            instance.gameObject.SetActive(false);
        }
    }
    public void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);  // Zobrazí GameObject
            if (ESC != null)
            {
                ESC.SetActive(false);
            }
        }
    }
}
