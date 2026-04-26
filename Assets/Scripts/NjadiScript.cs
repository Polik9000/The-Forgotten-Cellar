using System;
using UnityEngine;

public class NjadiScript : MonoBehaviour
{
    [Obsolete]
    void Start()
    {
    Finnish script = FindObjectOfType<Finnish>();
    if (script != null)
        {
            Debug.Log("Skript je na objektu: " + script.gameObject.name);
        }
    else    
        {
            Debug.Log("Žádný GameObject s tímto skriptem nebyl nalezen.");
        }
    }
}
