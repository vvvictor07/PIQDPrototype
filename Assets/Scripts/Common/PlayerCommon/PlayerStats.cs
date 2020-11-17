using Assets.Scripts.Common.PlayerCommon;
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class BaseStats
{
    public string baseStatName;
    public int defaultStat;
    public int levelUpStat;
    public int additionalStat;
    public PlayerStatType statType;

    public int finalStat
    {
        get
        {
            return defaultStat + additionalStat + levelUpStat;
        }
    }
}

[Serializable]
public class PlayerStats
{
    public int availableStatPoints = 10;

    public BaseStats[] characteristics = new BaseStats[]
    {
        new BaseStats() { statType = PlayerStatType.Strength, baseStatName = "STR" },
        new BaseStats() { statType = PlayerStatType.Dexterity, baseStatName = "DEX" },
        new BaseStats() { statType = PlayerStatType.Constitution, baseStatName = "CON" },
        new BaseStats() { statType = PlayerStatType.Wisdom, baseStatName = "WIS" },
        new BaseStats() { statType = PlayerStatType.Intelegent, baseStatName = "INT" },
        new BaseStats() { statType = PlayerStatType.Charisma, baseStatName = "CHA" },
    };

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

    public bool SetStats(PlayerStatType characteristicType, int amount)
    {
        if (amount > 0 && availableStatPoints - amount < 0)
        {
            return false;
        }

        var stat = GetStats(characteristicType);

        if (amount < 0 && stat.additionalStat + amount < 0)
        {
            return false;
        }

        stat.additionalStat += amount;
        availableStatPoints -= amount;

        return true;
    }

    public BaseStats GetStats(PlayerStatType characteristicType)
    {
        return characteristics.Single(stat => stat.statType == characteristicType);
    }
}
