using Assets.Scripts.Common.Constants;
using Assets.Scripts.Common.PlayerCommon;
using Assets.Scripts.Common.Services;
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float healthRegenCooldown = 5f;
    private bool disableHealthRegen;

    private float disableHealthRegenTime;

    [SerializeField] private float staminaRegenCooldown = 3f;
    public bool disableStaminaUsage;
    private float disableStaminaUsageTime;

    [SerializeField] private float consumablesUsageCooldown = 3f;
    public bool disableConsumablesUsage;
    private float disableConsumablesUsageTime;

    public Storage inventory = new Storage();
    public Item[] startingItems;

    public int gold = 0;

    public PlayerStats playerStats;
    public PlayerAppearance appearance;

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

    public Renderer characterRenderer;

    public static Player instance;

    void Awake()
    {
        instance = this;
        LoadPlayerData();

        inventory.items.AddRange(startingItems.Select(item => Instantiate(item)));
    }

    public void LevelUp()
    {
        playerStats.availableStatPoints += 3;

        foreach (var stat in playerStats.baseStats)
        {
            stat.levelUpStat += 1;
        }

        playerStats.UpdateStats();
    }

    public void ChangeProfession(PlayerProfession cProfession)
    {
        profession = cProfession;
        SetUpProfression();
    }

    public void SetUpProfression()
    {
        foreach (var stat in playerStats.baseStats)
        {
            stat.defaultStat = profession.defaultStats[stat.statType];
        }

        playerStats.UpdateStats();
    }

    private void Update()
    {
        ProcessHealthRegen();
        ProcessStaminaRegen();
        ProcessConsumablesCooldown();
    }

    public void DealDamage(float damage)
    {
        playerStats.CurrentHealth -= damage;
        disableHealthRegen = true;
        disableHealthRegenTime = Time.time;
    }

    public void Heal(float health)
    {
        playerStats.CurrentHealth += health;
    }

    private void ProcessHealthRegen()
    {
        if (!disableHealthRegen)
        {
            if (playerStats.CurrentHealth < playerStats.maxHealth.value)
            {
                playerStats.CurrentHealth += playerStats.regenHealth.value * Time.deltaTime;
            }
        }
        else
        {
            if (Time.time > disableHealthRegenTime + healthRegenCooldown)
            {
                disableHealthRegen = false;
            }
        }
    }

    private void ProcessStaminaRegen()
    {
        if (playerStats.CurrentStamina < 1)
        {
            disableStaminaUsage = true;
            disableStaminaUsageTime = Time.time;
            playerStats.CurrentStamina = 1;
        }

        if (playerStats.CurrentStamina < playerStats.maxStamina.value)
        {
            playerStats.CurrentStamina += playerStats.staminaRegen.value * Time.deltaTime;
        }

        if (disableStaminaUsage)
        {
            if (Time.time > disableStaminaUsageTime + staminaRegenCooldown)
            {
                disableStaminaUsage = false;
            }
        }
    }

    private void LoadPlayerData()
    {
        var data = SaveSystem.LoadPlayer();

        appearance = data.appearance;
        playerStats = data.stats;

        playerStats.healthHearts = FindObjectOfType<QuaterHearts>();

        Profession = PlayerProfession.professions[data.playerClass];

        foreach (var part in appearance.parts)
        {
            var path = $"{ResourcesLocations.CharacterTextures}{part.textureName}";
            var texture = (Texture2D)Resources.Load(path);
            Material[] mats = characterRenderer.materials;
            mats[(int)part.partType].mainTexture = texture;
            characterRenderer.materials = mats;
        }
    }

    private void ProcessConsumablesCooldown()
    {
        if (disableConsumablesUsage && Time.time > disableConsumablesUsageTime + consumablesUsageCooldown)
        {
            disableConsumablesUsage = false;
            inventory.InvokeOnStorageUpdate();
        }
    }

    public void SetConsumablesOnCooldown()
    {
        disableConsumablesUsage = true;
        disableConsumablesUsageTime = Time.time;
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

        GUI.TextArea(new Rect(10, 40, 120, 20), $"{playerStats.CurrentStamina}/{playerStats.maxStamina.value}");
    }
}
