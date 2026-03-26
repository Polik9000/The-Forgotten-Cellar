using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class Finnish : MonoBehaviour
{
    public float waitTime = 2.0f;
    public float detectionRadius = 0.8f; // Tolerance, protože přesné souřadnice netrefíš

    private Coroutine teleportRoutine;
    private bool isWaiting = false;
    private bool onCooldown = false;
    public GameObject winPanel;
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        if (onCooldown) return;

        GameObject? panel = GetTargetPosition();

        if (panel != null && !isWaiting)
        {
            teleportRoutine = StartCoroutine(FinnishingTimer(panel));
        }
        else if (panel == null && isWaiting)
        {
            StopFinnishing();
        }
    }
    private GameObject? GetTargetPosition()
    {
            if (Vector2.Distance(player.transform.position, transform.position) < detectionRadius)
                return winPanel;
            else return null;
    }
    private IEnumerator FinnishingTimer(GameObject winPanel)
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTime);

        onCooldown = true;
        isWaiting = false;
        if (!winPanel.activeSelf)
        {
            winPanel.transform.Find("You_Died_Win").GetComponent<TMP_Text>().text = "You Win";
            winPanel.transform.Find("Try_Play_Again").Find("Text").GetComponent<TMP_Text>().text = "Play Again";
            winPanel.SetActive(true);
        }
        yield return new WaitForSeconds(1.0f); // Cooldown proti smyčce
        onCooldown = false;
    }

    private void StopFinnishing()
    {
        if (teleportRoutine != null) StopCoroutine(teleportRoutine);
        isWaiting = false;
    }

    // Vizualizace bodů v Editoru (pro ladění)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
