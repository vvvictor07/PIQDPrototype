using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool disableRegen;
    private float disableRegenTime;
    public float regenCooldown = 5f;

    public PlayerStats playerStats;

    public PlayerProfession profession;
    public PlayerProfession Profession
    {
        get
        {
            return profession;
        }
        set
        {
            ChangeProfession(value);
        }
    }
    public void LevelUp()
    {
        playerStats.baseStatPoints += 3;

        for(int i = 0; i < playerStats.baseStats.Length; i++)
        {
           playerStats.baseStats[i].levelUpStat += 1;
        }
    }

    public void ChangeProfession(PlayerProfession cProfession)
    {
        this.profession = cProfession;
        SetUpProfression();
    }

    public void SetUpProfression()
    {
        for(int i = 0; i < playerStats.baseStats.Length; i++)
        {
            if (i < profession.defaultStats.Length)
            {
                playerStats.baseStats[i].defaultStat = profession.defaultStats[i].defaultStat;
            }
            
        }
    }
    private void Update()
    {
        if (!disableRegen) 
        {
            if(playerStats.CurrentHealth < playerStats.maxHealth)
            {
                playerStats.CurrentHealth = playerStats.regenHealth * Time.deltaTime;
            }           
        }
        else
        {
            if (Time.time > disableRegenTime + regenCooldown)
            {
                disableRegen = false;
            }
        }

    }
    public void DealDamage(float damage)
    {
        playerStats.CurrentHealth -= damage;
        disableRegen = true;
        disableRegenTime = Time.time;
    }
    public void Heal(float health)
    {
        playerStats.CurrentHealth += health;
    }

    //temp level up button & deal damage button
    public void OnGUI() 
    {
        if (GUI.Button(new Rect(130, 10, 100, 20), "Level Up"))
        {
            LevelUp();
        }

        if (GUI.Button(new Rect(130, 40, 120, 20), "Do Damage" + playerStats.CurrentHealth)) // temp damage button
        {
            DealDamage(25f);
        }
    }
}
