using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float MaxHp;
    public float Hp;
    public float Dmg;
    public float WeaponDmg;
    public float EnemyDmg;
    public float Armor;
    public Collider2D Range;
    public bool enemyIsNear;
    public bool CanAttack;
    public float AttackSpeed;
    public GameObject DeadPanel;
    private StatsManager statsManager;
    public Image postava;
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

    public void DmgToMe()
    {
        float Armor = statsManager.Defense;
        float finalDmg;
        if (Armor > EnemyDmg)
        {
            if ((Armor-100) > EnemyDmg)
            {
                finalDmg = 0;
            }
            else 
            {
                finalDmg = (1 - ((Armor-EnemyDmg)/100))*EnemyDmg ;
            }
        } 
        else if (Armor == EnemyDmg)
        {
            finalDmg = 1;
        }
        else
        {
            finalDmg = EnemyDmg - Armor;
        }
        Hp -= finalDmg;

        if (Hp <= 0 )
        {
            Time.timeScale = 0;
            if (!DeadPanel.activeSelf)
            {
                DeadPanel.transform.Find("You_Died_Win").GetComponent<TMP_Text>().text = "You Died";
                DeadPanel.transform.Find("Try_Play_Again").Find("Text").GetComponent<TMP_Text>().text = "Try Again";
                DeadPanel.SetActive(true);
            }
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
