using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerHealthManager : MonoBehaviour
{
    public Slider healthBar; // Odkaz na UI prvek Slider pro zobrazení zdraví
    public TMP_Text healthText; // Odkaz na UI prvek Text pro zobrazení zdraví
    void Update()
    {
        Player player = FindAnyObjectByType<Player>();
        healthBar.value = player.Hp / player.MaxHp; // Aktualizace hodnoty zdraví na slideru
        healthText.text = player.Hp.ToString() + "/" + player.MaxHp.ToString(); // Aktualizace textu zdraví
        if (player.Hp <= 0)
        {
            player.Hp = 0; // Nastavení zdraví na nulu
        }
        if (player.Hp > player.MaxHp)
        {
            player.Hp = player.MaxHp; // Oprava zdraví, pokud překročí maximální hodnotu
        }
    }
}