using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string panelNameToShow;  // Jméno panelu v cílové scéně

    public void SwitchScene(string sceneName)
    {
        // Přepne scénu
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!string.IsNullOrEmpty(panelNameToShow))
        {
            GameObject panel = GameObject.Find(panelNameToShow);  // Najde panel podle názvu
            if (panel != null)
            {
                panel.SetActive(true);  // Zobrazí panel
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
