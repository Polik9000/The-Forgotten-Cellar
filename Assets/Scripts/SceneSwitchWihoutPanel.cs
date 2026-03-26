using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchWithoutPanel : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        // Poté přepneme scénu
        SceneManager.LoadScene(sceneName);
    }
}