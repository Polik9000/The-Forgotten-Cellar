using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapColliderAdder : MonoBehaviour
{
    [Tooltip("Vyberte dlaždice, na které chcete přidat collider.")]
    public TileBase[] targetTiles; // Dlaždice, na které chceme přidat collidery.

    [Tooltip("Poměr zvětšení nebo zmenšení velikosti collideru (1 = původní velikost)")]
    public Vector2 colliderSizeMultiplier = Vector2.one;

    private void Start()
    {
        // Získáme Tilemap
        Tilemap tilemap = GetComponent<Tilemap>();
        if (tilemap == null)
        {
            return;
        }

        // Projdeme všechny pozice dlaždic na Tilemap
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(tilePosition);
                if (tile != null && IsTargetTile(tile)) // Pokud je dlaždice mezi cílovými
                {
                    // Získáme přesnou světovou pozici dlaždice
                    Vector3 localPosition = tilemap.GetCellCenterLocal(tilePosition);
                    GameObject colliderObject = new GameObject($"Collider_{x}_{y}");
                    // Posuneme collider o půl jednotky nahoru (pokud je potřeba)
                    colliderObject.transform.parent = transform;

                    // Vytvoříme nový GameObject pro collider
                    colliderObject.transform.localPosition = localPosition;
                    colliderObject.transform.parent = transform; // Nastavíme jako dítě Tilemap

                    // Přidáme BoxCollider2D
                    BoxCollider2D boxCollider = colliderObject.AddComponent<BoxCollider2D>();

                    // Přizpůsobíme velikost collideru
                    boxCollider.size = new Vector2(1f, 1f) * colliderSizeMultiplier;

                    // Nastavíme správný layer
                    colliderObject.layer = LayerMask.NameToLayer("walls");  // Přiřazení layeru "Walls"
                    Debug.Log($"Přidán collider na pozici: {localPosition} pro dlaždici: {tile.name}");
                }
            }
        }
    }

    // Funkce kontroluje, zda je dlaždice mezi cílovými
    private bool IsTargetTile(TileBase tile)
    {
        foreach (TileBase targetTile in targetTiles)
        {
            if (tile == targetTile)
            {
                return true;
            }
        }
        return false;
    }
}
