using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerLevelManager : MonoBehaviour
{
    public Slider LevelSlider;
    public TMP_Text Zbyva;
    public TMP_Text Level;
    public int level;
    public int Xp;
    public int potrebneXp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        level = 1;
        Xp = 0;
        potrebneXp = 5;
        Level.text = level.ToString();
        Zbyva.text = Xp.ToString() +"/" + potrebneXp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Xp >= potrebneXp)
        {
            if(Xp == potrebneXp)
            {
                Xp = 0;
            }
            else if (Xp > potrebneXp)
            {
                int nadbyva = Xp - potrebneXp;
                Xp = nadbyva;
            }
            level++;
            potrebneXp += 5;
            Level.text = level.ToString();
            Zbyva.text = Xp.ToString() +"/" + potrebneXp.ToString();
            Player player = FindAnyObjectByType<Player>();
            player.MaxHp += 2;
            player.Hp +=2;
            StatsManager statsManager = FindAnyObjectByType<StatsManager>();
            statsManager.DmgModifierPercent += 0.1f;
        }
        else if (Xp < potrebneXp)
        {
            LevelSlider.value = (float)Xp/potrebneXp;
            Zbyva.text = Xp.ToString() +"/" + potrebneXp.ToString();
        }
    }
}