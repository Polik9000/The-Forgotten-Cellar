using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthManager : MonoBehaviour
{
    public Slider healthBar; // Odkaz na UI prvek Slider pro zobrazení zdraví
    public Transform target;
    public Vector3 offset = new Vector3 (0,2f,0); // Offset pro pozici health baru
    public Camera mainCamera; // Odkaz na hlavní kameru
    public RectTransform rectTransform; // Odkaz na RectTransform pro nastavení pozice health baru
    public Enemy enemy; // Nepřidávej GetComponent v Update!
    void Start()
    {
        // Získání odkazu na hlavní kameru
        mainCamera = Camera.main;
        // Získání odkazu na RectTransform pro nastavení pozice health baru
        rectTransform = healthBar.GetComponent<RectTransform>();
    }
    void Update()
    {
        healthBar.maxValue = 10;
        healthBar.value = 10*enemy.HP / enemy.MaxHp; // Aktualizace hodnoty zdraví na slideru
        if (enemy.HP <= 0)
        {
            enemy.HP = 0; // Nastavení zdraví na nulu
        }
        if (enemy.HP > enemy.MaxHp)
        {
            enemy.HP = enemy.MaxHp; // Oprava zdraví, pokud překročí maximální hodnotu
        }

        HeathBarPosition(); // Aktualizuj pozici health baru
    }

    void HeathBarPosition()
    {

        if (target != null && enemy.HP < enemy.MaxHp)
        {
            // Získání pozice cíle a přidání offsetu
            Vector3 worldPosition = target.position + offset;
            // Převod světových souřadnic na obrazovkové souřadnice
            Vector2 screenPoint = mainCamera.WorldToScreenPoint(worldPosition);
            
            // Porovnej pouze x a y hodnoty pro 2D pozice
            if (screenPoint != new Vector2(rectTransform.position.x, rectTransform.position.y))
            {
                rectTransform.position = screenPoint;
            }
        }
        else if (enemy.HP >= enemy.MaxHp)
        {
            // Skrytí health baru, pokud je zdraví maximální
            rectTransform.transform.position = new Vector3(-0, 0, -10000); // Skrytí health baru mimo obrazovku
        }
    }
}
