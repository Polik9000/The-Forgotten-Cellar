using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CursorVisualController : MonoBehaviour
{
    public Image cursorImage;              // Hlavní obrázek kurzoru (barevný)
    public Image chargeOverlayImage;      // Dítě s efektem nabíjení
    public Player player;                 // Odkaz na skript hráče
    public Sprite ColouredCursor;         // Barevný kurzor
    public Sprite GrayScaleCursor;        // Šedý kurzor
    public float currentCooldown;
    public Vector2 offset = Vector2.zero; // Volitelný posun

    void Start()
    {
        // Skryje systémový kurzor
        UnityEngine.Cursor.visible = false;

        // Najde hráče, pokud není přiřazen ručně
        if (player == null && FindAnyObjectByType<Player>() != null)
        {
            player = FindAnyObjectByType<Player>();
        }

        currentCooldown = 0f;

        if(chargeOverlayImage!=null)
        {
            // Ujisti se, že efekt nabíjení je nad kurzorem (z-index nebo v hierarchii)
            chargeOverlayImage.transform.SetAsLastSibling();
        }
    }
    void Update()
    {
        // Posune celý kurzor na pozici myši
        Vector3 mousePos = Input.mousePosition;
        cursorImage.transform.position = mousePos + (Vector3)offset;
        if (player != null)
        {
            // Změna kurzoru podle stavu hráče
            if (player.enemyIsNear)
                cursorImage.sprite = ColouredCursor;
            else
                cursorImage.sprite = GrayScaleCursor;

            // Efekt nabíjení útoku (pomocí shaderu)
            if (!player.CanAttack)
            {
                currentCooldown += Time.deltaTime;
                float fillAmount = Mathf.Clamp01(currentCooldown / player.AttackSpeed);
                chargeOverlayImage.material.SetFloat("_FillAmount", 1 - fillAmount);
            }
            else
            {
                currentCooldown = 0f;
                chargeOverlayImage.material.SetFloat("_FillAmount", 0);
            }
        }
    }
}
