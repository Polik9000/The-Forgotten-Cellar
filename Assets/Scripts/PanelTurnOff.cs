using UnityEngine;
public class PanelTurnOff : MonoBehaviour
{
    public static PanelTurnOff instance;

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
            panel.SetActive(false);  // Skryje GameObject
        }

    }
}
