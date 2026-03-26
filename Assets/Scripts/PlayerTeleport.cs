using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private List<TeleportPair> teleportPairs = new List<TeleportPair>();
    public float waitTime = 2.0f;
    public float detectionRadius = 0.8f; // Tolerance, protože přesné souřadnice netrefíš

    private Coroutine teleportRoutine;
    private bool isWaiting = false;
    private bool onCooldown = false;

    void Update()
    {
        if (onCooldown) return;

        Vector2? targetPos = GetTargetPosition();

        if (targetPos.HasValue && !isWaiting)
        {
            teleportRoutine = StartCoroutine(TeleportTimer(targetPos.Value));
        }
        else if (!targetPos.HasValue && isWaiting)
        {
            StopTeleport();
        }
    }

    private Vector2? GetTargetPosition()
    {
        foreach (var pair in teleportPairs)
        {
            // Kontrola okolí bodu A
            if (Vector2.Distance(transform.position, pair.posA) < detectionRadius)
                return pair.posB;
            // Kontrola okolí bodu B
            if (Vector2.Distance(transform.position, pair.posB) < detectionRadius)
                return pair.posA;
        }
        return null;
    }

    private IEnumerator TeleportTimer(Vector2 destination)
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        onCooldown = true;
        isWaiting = false;
        transform.position = destination;

        yield return new WaitForSeconds(1.0f); // Cooldown proti smyčce
        onCooldown = false;
    }

    private void StopTeleport()
    {
        if (teleportRoutine != null) StopCoroutine(teleportRoutine);
        isWaiting = false;
    }

    // Vizualizace bodů v Editoru (pro ladění)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        foreach (var pair in teleportPairs)
        {
            Gizmos.DrawWireSphere(pair.posA, detectionRadius);
            Gizmos.DrawWireSphere(pair.posB, detectionRadius);
            Gizmos.DrawLine(pair.posA, pair.posB);
        }
    }
}