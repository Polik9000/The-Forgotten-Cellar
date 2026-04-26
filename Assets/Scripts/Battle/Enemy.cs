using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public float HP;
    public float MaxHp;
    public float Dmg;
    public int GainedDmg;
    public int XP;
    public float AttackSpeed;
    public bool CanAttack;
    public CircleCollider2D Range;
    public bool PlayerIsNear;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyHealthManager healthManager = GetComponentInChildren<EnemyHealthManager>();
        if (healthManager != null)
        {
            healthManager.enemy = this;
            healthManager.target = this.transform; // Ať se slider drží nad nepřítelem
        }
        player = GameObject.FindGameObjectWithTag("Player");
        CanAttack = true;
        if (gameObject.name.Contains("krysa"))
        {
            MaxHp = 5;
            Dmg = 2;
            AttackSpeed = 1;
            Range.radius = 0.2f;
            XP = 2;
        }
        if (gameObject.name.Contains("pavouk"))
        {
            MaxHp = 10;
            Dmg = 4;
            AttackSpeed = 3;
            Range.radius = 0.25f;
            XP = 5;
        }
        HP = MaxHp;
    }
    // Update is called once per frame
    void Update()
    {
        if (CanAttack && PlayerIsNear)
        {
            CanAttack = false;
            StartCoroutine(Wait());
            DmgToPlayer();
       }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(AttackSpeed); // Počká 2 sekundy
        CanAttack = true;
    }
    public void DmgToMe()
    {
        Debug.Log("jyc" + gameObject.name);
        if (GainedDmg != 0)
        {
            HP -= GainedDmg;
            GainedDmg = 0;
        }
        if (HP <= 0)
        {
            player.GetComponent<PlayerLevelManager>().Xp += XP;
            gameObject.SetActive(false);
        }
    }
    public void DmgToPlayer()
    {
        if (Dmg != 0 && player != null)
        {
            player.GetComponent<Player>().DmgToMe(Dmg);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerIsNear = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerIsNear = false;
        }
    }
}