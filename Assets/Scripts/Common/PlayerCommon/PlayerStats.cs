using Assets.Scripts.Common.PlayerCommon;
using System;
using System.Linq;
using UnityEngine;

[Serializable]
public class Stat
{
    public Stat(float value)
    {
        defaultValue = value;
        this.value = value;
    }

    public float defaultValue;
    public float value;
}

[Serializable]
public class PlayerStats
{
    public int availableStatPoints = 10;

    public BaseStats[] baseStats = new BaseStats[]
    {
        new BaseStats() { statType = PlayerBaseStatsType.Strength, baseStatName = "STR" },
        new BaseStats() { statType = PlayerBaseStatsType.Dexterity, baseStatName = "DEX" },
        new BaseStats() { statType = PlayerBaseStatsType.Constitution, baseStatName = "CON" },
        new BaseStats() { statType = PlayerBaseStatsType.Wisdom, baseStatName = "WIS" },
        new BaseStats() { statType = PlayerBaseStatsType.Intelegent, baseStatName = "INT" },
        new BaseStats() { statType = PlayerBaseStatsType.Charisma, baseStatName = "CHA" },
    };

    [Header("Player Stats")]
    [SerializeField] public Stat speed = new Stat(6f);
    [SerializeField] public Stat sprintSpeed = new Stat(12f);
    [SerializeField] public Stat crouchSpeed = new Stat(3f);
    [SerializeField] public Stat jumpHeight = new Stat(1f);

    [SerializeField] public Stat maxHealth = new Stat(100f);
    [SerializeField] public Stat regenHealth = new Stat(5f);

    [SerializeField] public Stat maxMana = new Stat(100f);
    [SerializeField] public Stat manaRegen = new Stat(5f);

    [SerializeField] public Stat maxStamina = new Stat(100f);
    [SerializeField] public Stat staminaRegen = new Stat(10f);

    [Header("Current Stats")]
    [SerializeField] public int level;

    [SerializeField] public float currentMana = 100;
    [SerializeField] public float currentStamina = 100;

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
            currentHealth = Mathf.Clamp(value, 0, maxHealth.value);

            if(healthHearts != null) 
            { 
                healthHearts.UpdateHearts(value, maxHealth.value);
            }
        }
    }

    public void UpdateStats()
    {
        var dexterity = GetBaseStat(PlayerBaseStatsType.Dexterity).finalStat;
        var constitution = GetBaseStat(PlayerBaseStatsType.Constitution).finalStat;
        var strength = GetBaseStat(PlayerBaseStatsType.Strength).finalStat;
        var intelegent = GetBaseStat(PlayerBaseStatsType.Intelegent).finalStat;
        var wisdom = GetBaseStat(PlayerBaseStatsType.Wisdom).finalStat;
        var charisma = GetBaseStat(PlayerBaseStatsType.Charisma).finalStat;

        speed.value = speed.defaultValue + dexterity;
        sprintSpeed.value = sprintSpeed.defaultValue + dexterity * 1.5f;
        crouchSpeed.value = crouchSpeed.defaultValue + dexterity * .5f;
        jumpHeight.value = jumpHeight.defaultValue + dexterity * .2f;

        maxHealth.value = maxHealth.defaultValue + constitution * 10;
        regenHealth.value = regenHealth.defaultValue + constitution * 2;

        maxStamina.value = maxHealth.defaultValue + strength * 10;
        staminaRegen.value = staminaRegen.defaultValue + strength * 2;

        maxMana.value = maxMana.defaultValue + intelegent * 8 + charisma * 5;
        manaRegen.value = manaRegen.defaultValue + wisdom + charisma;
    }

    public bool SetBaseStat(PlayerBaseStatsType type, int amount)
    {
        if (amount > 0 && availableStatPoints - amount < 0)
        {
            return false;
        }

        var stat = GetBaseStat(type);

        if (amount < 0 && stat.additionalStat + amount < 0)
        {
            return false;
        }

        stat.additionalStat += amount;
        availableStatPoints -= amount;

        UpdateStats();
        return true;
    }

    public BaseStats GetBaseStat(PlayerBaseStatsType type)
    {
        return baseStats.Single(stat => stat.statType == type);
    }
}
