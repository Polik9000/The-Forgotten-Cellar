using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Zapnuti_vypnuti_pomoci_klavesnice : MonoBehaviour
{
    public string keyString; // Písmeno pro běžné klávesy (např. "A", "B", "C")
    public KeyCode specialKey = KeyCode.None; // Speciální klávesa (např. Tab, Escape, Space)

    public GameObject gameObject1;
    public GameObject[] porovnavaciGameObjecty;
    public bool Zapnuto_na_zacatku;
    public bool muzuZapnout;
    public int muzuInt;
    private void Start()
    {
        gameObject1.SetActive(Zapnuto_na_zacatku);
    }

    private void Update()
    {
        KeyCode key;
        bool keyPressed = false;

        // Kontrola běžné klávesy (písmeno)
        if (!string.IsNullOrEmpty(keyString) && System.Enum.TryParse(keyString.ToUpper(), out key))
        {
            if (Input.GetKeyDown(key))
            {
                keyPressed = true;
            }
        }

        // Kontrola speciální klávesy
        if (specialKey != KeyCode.None && Input.GetKeyDown(specialKey))
        {
            keyPressed = true;
        }

        // Pokud byla stisknuta platná klávesa
        if (keyPressed)
        {
            muzuInt = 0;
            muzuZapnout = false;
            foreach (GameObject porovnavaciGameObject in porovnavaciGameObjecty)
            {
                if (porovnavaciGameObject.activeSelf)
                {
                    muzuZapnout = false;
                }
                else
                {
                    muzuInt ++;
                }
            }
            if (muzuInt == porovnavaciGameObjecty.Length)
            {
                muzuZapnout = true;
            }
            if (muzuZapnout)
            {
                gameObject1.SetActive(!gameObject1.activeSelf);
            }
        }
    }
}