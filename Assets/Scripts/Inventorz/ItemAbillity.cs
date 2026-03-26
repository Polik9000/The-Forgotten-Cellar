using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
public class ItemAbillity : MonoBehaviour
{
    public bool Called;
    void Start()
    {
        Called = false;
    }
    void Update()
    {
        if (Called == true)
        {
            Called = false;
            // Call the item abillity here
            if (gameObject.name.Contains("healing_potion"))
            {
                Player player = FindAnyObjectByType<Player>();
                if (player != null)
                {
                    player.Hp += 20; // Increase health by 20
                    gameObject.SetActive(false); // Deactivate the item after use
                }
            }
            if (gameObject.name.Contains("dagger"))
            {
                Player player = FindAnyObjectByType<Player>();
                player.WeaponDmg = 2;
                player.DmgToEnemy();
            }
        }
    }
}
