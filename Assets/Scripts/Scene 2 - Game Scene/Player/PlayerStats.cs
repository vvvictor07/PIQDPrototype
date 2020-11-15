using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BaseStats
{
    public string baseStatName;
    public int defaultStat;
    public int levelUpStat;
    public int additionalStat;

    public int finalStat
    {
        get
        {
            return defaultStat + additionalStat + levelUpStat;
        }
    }
}

[System.Serializable]
public class PlayerStats
{
    public int baseStatPoints = 10;
    public BaseStats[] baseStats;
    public bool SetStats(int statIndex, int amount)
    {
        if (amount > 0 && baseStatPoints - amount < 0)
        {
            return false;
        }
        else if(amount < 0 && baseStats[statIndex].additionalStat + amount < 0)
        {
            return false;
        }

        baseStats[statIndex].additionalStat += amount;
        baseStatPoints -= amount;

        return true;
    }

    [Header("Player Stats")]
    [SerializeField] public float speed = 6f;
    [SerializeField] public float sprintSpeed = 12f;
    [SerializeField] public float crouchSpeed = 3f;
    [SerializeField] public float jumpHeight = 1.0f;

    [Header("Current Stats")]
    [SerializeField] public int level;
    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float regenHealth = 5f;
    [SerializeField] public float currentMana = 100;
    [SerializeField] public float MaxMana = 100;
    [SerializeField] public float currentStamina = 100;
    [SerializeField] public float maxStamina = 100;

    public QuaterHearts healthHearts;

    private float currentHealth = 100; 
    public float CurrentHealth 
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);

            if(healthHearts != null) 
            { 
                healthHearts.UpdateHearts(value, maxHealth);
            }
        }
    }

    


}
