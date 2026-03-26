using UnityEngine;
using TMPro;

public class ItemStats : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent.name.Contains("pants_of_stealth"))   
        {
            gameObject.GetComponent<TMP_Text>().text = "Defense: 1";
        }
        if (gameObject.transform.parent.name.Contains("cap_of_stealth"))   
        {
            gameObject.GetComponent<TMP_Text>().text = "Defense: 1";
        }
        if (gameObject.transform.parent.name.Contains("shoe_of_stealthR"))   
        {
            gameObject.GetComponent<TMP_Text>().text = "Defense: 1";
        }
        if (gameObject.transform.parent.name.Contains("shoe_of_stealthL"))   
        {
            gameObject.GetComponent<TMP_Text>().text = "Defense: 1";
        }
    }
}
