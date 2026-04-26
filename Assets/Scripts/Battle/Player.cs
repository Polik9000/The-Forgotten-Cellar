using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
public class Player : MonoBehaviour
{
    public float MaxHp;
    public float Hp;
    public float Dmg;
    public float WeaponDmg;
    public float Armor;
    public Collider2D Range;
    public bool enemyIsNear;
    public bool CanAttack;
    public float AttackSpeed;
    public GameObject DeadPanel;
    private StatsManager statsManager;
    public Image postava;
    [SerializeField] private GameObject deadPanel;
    [SerializeField] private TMP_Text youDiedText;
    [SerializeField] private TMP_Text tryAgainText;
    private void Start()
    {
        MaxHp = 10;
        Hp = 10;
        CanAttack = true;
        AttackSpeed = 2;
        Armor = 0;
        DeadPanel.SetActive(false);
        statsManager = FindAnyObjectByType<StatsManager>();
        postava.GetComponent<Image>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void DmgToEnemy()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Zjisti, co je pod kurzorem
        Collider2D hitCollider = Physics2D.OverlapPoint(cursorPos);
        if (hitCollider != null && hitCollider.CompareTag("Enemy"))
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null && CanAttack && enemyIsNear)
            {
                CanAttack = false;
                Dmg = WeaponDmg;

                float upravenyDmg = (Dmg + statsManager.DmgModifierUnitary) * statsManager.DmgModifierPercent;
                enemy.GainedDmg = (int)Math.Round(upravenyDmg);
                enemy.DmgToMe();

                StartCoroutine(Wait());
            }
        }
    }

    public void DmgToMe(float EnemyDmg)
    {
        float Armor = statsManager.Defense;
        float finalDmg;
        if (Armor >= EnemyDmg + 100f)
        {
            finalDmg = 0f;
        }
        else
        {
            float minimumDmgFloor = EnemyDmg * 0.1f; // 10% z původního poškození
            finalDmg = Mathf.Max(EnemyDmg - Armor, minimumDmgFloor);
        }
        Hp -= finalDmg;
        if (Hp <= 0 )
        {
            Die();
        }
    }
    private void Die()
    {
        Time.timeScale = 0f;
        if (!deadPanel.activeSelf)
        {
            youDiedText.text = "You Died";
            tryAgainText.text = "Try Again";
            deadPanel.SetActive(true);
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(AttackSpeed);
        CanAttack = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyIsNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyIsNear = false;
        }
    }
}
