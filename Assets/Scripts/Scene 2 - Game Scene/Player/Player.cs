using Assets.Scripts.Common.Constants;
using Assets.Scripts.Common.PlayerCommon;
using Assets.Scripts.Common.Services;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool disableRegen;
    private float disableRegenTime;
    public float regenCooldown = 5f;

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

    void Start()
    {
        LoadPlayerData();
    }

    public void LevelUp()
    {
        playerStats.availableStatPoints += 3;

        foreach (var stat in playerStats.characteristics)
        {
            stat.levelUpStat += 1;
        }
    }

    public void ChangeProfession(PlayerProfession cProfession)
    {
        profession = cProfession;
        SetUpProfression();
    }

    public void SetUpProfression()
    {
        foreach (var stat in playerStats.characteristics)
        {
            stat.defaultStat += profession.defaultStats[stat.statType];
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

    private void LoadPlayerData()
    {
        var data = SaveSystem.LoadPlayer();
        appearance = data.appearance;
        playerStats = data.stats;

        foreach (var part in appearance.parts)
        {
            var path = $"{ResourcesLocations.CharacterTextures}{part.textureName}";
            var texture = (Texture2D)Resources.Load(path);
            Material[] mats = characterRenderer.materials;
            mats[(int)part.partType].mainTexture = texture;
            characterRenderer.materials = mats;
        }
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
