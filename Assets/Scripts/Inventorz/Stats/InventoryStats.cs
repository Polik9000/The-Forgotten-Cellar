using UnityEngine;
using TMPro;
public class InventoryStats : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StatsManager statsManager = FindAnyObjectByType<StatsManager>();
        gameObject.GetComponent<TMP_Text>().text = null;
        gameObject.GetComponent<TMP_Text>().text = "Max HP: " + statsManager.MaxHealth + "ada";
        gameObject.GetComponent<TMP_Text>().text = gameObject.GetComponent<TMP_Text>().text.Replace("ada","\nDefense: " + statsManager.Defense) + "ada";
        gameObject.GetComponent<TMP_Text>().text = gameObject.GetComponent<TMP_Text>().text.Replace("ada","\nDamage: " + statsManager.TotalDmg);
    }
}
