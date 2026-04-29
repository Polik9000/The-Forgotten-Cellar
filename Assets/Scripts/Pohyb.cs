using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Rychlost pohybu
    private Rigidbody2D rb; // Odkaz na Rigidbody2D
    public Vector2 startposition;
    private void Start()
    {
        // Získání reference na Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        transform.position = startposition;
    }

    private void Update()
    {
        // Získání vstupních hodnot pro pohyb (WSAD nebo šipky)
        float moveX = Input.GetAxisRaw("Horizontal"); // -1 pro 'A', 1 pro 'D'
        float moveY = Input.GetAxisRaw("Vertical");   // -1 pro 'S', 1 pro 'W'

        if (moveX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true; // Otočení sprite horizontálně

        }
        else if (moveX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false; // Otočení sprite zpět
        }
        // Nastavení pohybu pomocí Rigidbody2D velocity
        rb.linearVelocity = new Vector2(moveX, moveY) * moveSpeed;
    }
}
