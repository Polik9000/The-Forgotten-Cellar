using UnityEngine;

public class PersistentPanel : MonoBehaviour
{
    private void Awake()
    {
        // Zajistí, že tento objekt nebude zničen při načtení nové scény
        DontDestroyOnLoad(gameObject);
    }
}
