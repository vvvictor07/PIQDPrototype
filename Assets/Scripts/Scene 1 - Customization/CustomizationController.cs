using Assets.Scripts.Common.Constants;
using Assets.Scripts.Common.PlayerCommon;
using Assets.Scripts.Common.Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationController : MonoBehaviour
{
    public Renderer characterRenderer;
    public Text availablePointsTextElement;

    private readonly Dictionary<AppearancePartType, List<Texture2D>> partsTextures = new Dictionary<AppearancePartType, List<Texture2D>>()
    {
        { AppearancePartType.Eyes, new List<Texture2D>() },
        { AppearancePartType.Armour, new List<Texture2D>() },
        { AppearancePartType.Clothes, new List<Texture2D>() },
        { AppearancePartType.Hair, new List<Texture2D>() },
        { AppearancePartType.Skin, new List<Texture2D>() },
        { AppearancePartType.Mouth, new List<Texture2D>() },
    };

    private readonly Dictionary<AppearancePartType, int> selectedParts = new Dictionary<AppearancePartType, int>()
    {
        { AppearancePartType.Eyes, 0 },
        { AppearancePartType.Armour, 0 },
        { AppearancePartType.Clothes, 0 },
        { AppearancePartType.Hair, 0 },
        { AppearancePartType.Skin, 0 },
        { AppearancePartType.Mouth, 0 },
    };

    private readonly PlayerStats statsData = new PlayerStats();
    private string playerName;
    private PlayerProfessionType selectedClass;

    public static CustomizationController instance;

    public delegate void UpdateStats(PlayerStats stats);
    public event UpdateStats OnStatsChange;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadTextures();
        UpdateStatsVisuals();
        UpdateClass(0);
    }

    public void SelectPreviousAppearancePart(int partIndex)
    {
        var partType = (AppearancePartType)partIndex;

        int max = partsTextures[partType].Count;
        var index = selectedParts[partType] - 1;

        if (index < 0)
        {
            index = max - 1;
        }

        SetTexture(partType, index);
    }

    public void SelectNextAppearancePart(int partIndex)
    {
        var partType = (AppearancePartType)partIndex;
        int max = partsTextures[partType].Count - 1;
        var index = selectedParts[partType] + 1;

        if (index > max)
        {
            index = 0;
        }

        SetTexture(partType, index);
    }

    public void UpdateName(string value)
    {
        playerName = value;
    }

    public void UpdateClass(int classIndex)
    {
        var playerClass = (PlayerProfessionType)classIndex;
        selectedClass = playerClass;

        var classData = PlayerProfession.professions[playerClass];
        foreach (var stat in statsData.baseStats)
        {
            stat.defaultStat = classData.defaultStats[stat.statType];
        }

        UpdateStatsVisuals();
    }

    public void RandomizeAppearance()
    {
        foreach (var type in partsTextures.Keys)
        {
            var textures = partsTextures[type];
            var random = Random.Range(0, textures.Count);
            SetTexture(type, random);
        }
    }

    public void ResetAppearance()
    {
        foreach (var type in partsTextures.Keys)
        {
            SetTexture(type, 0);
        }
    }

    public void SetStat(PlayerBaseStatsType statType, int value)
    {
        statsData.SetBaseStat(statType, value);
        UpdateStatsVisuals();
    }

    public void RandomizeStats()
    {
        ResetStats();

        var pointsTotal = statsData.availableStatPoints;

        for (var i = 0; i < pointsTotal; i++)
        {
            var random = Random.Range(0, statsData.baseStats.Length);
            var randomStat = statsData.baseStats[random];
            statsData.SetBaseStat(randomStat.statType, 1);
        }

        UpdateStatsVisuals();
    }

    public void ResetStats()
    {
        foreach (var stat in statsData.baseStats)
        {
            statsData.availableStatPoints += stat.additionalStat;
            stat.additionalStat = 0;
        }
        UpdateStatsVisuals();
    }

    public void Save()
    {
        statsData.UpdateStats();

        var appearanceData = new PlayerAppearance()
        {
            parts = selectedParts
            .Select(x => new PlayerAppearancePart
            {
                partType = x.Key,
                textureName = partsTextures[x.Key][x.Value].name,
            })
            .ToArray(),
        };

        var data = new PlayerData
        {
            appearance = appearanceData,
            stats = statsData,
            name = playerName,
            playerClass = selectedClass,
        };

        SaveSystem.SavePlayer(data);
    }

    private void SetTexture(AppearancePartType type, int textureIndex)
    {
        selectedParts[type] = textureIndex;

        var texture = GetSelectedTextureByType(type);
        
        Material[] mats = characterRenderer.materials;
        mats[(int)type].mainTexture = texture;
        characterRenderer.materials = mats;
    }

    private void UpdateStatsVisuals()
    {
        OnStatsChange(statsData);
        availablePointsTextElement.text = $"Points: {statsData.availableStatPoints}";
    }

    private void LoadTextures()
    {
        foreach (var part in partsTextures)
        {
            int textureCount = 0;

            Texture2D tempTexture;

            do
            {
                var path = $"{ResourcesLocations.CharacterTextures}{part.Key}_{textureCount}";
                tempTexture = (Texture2D)Resources.Load(path);

                if (tempTexture != null)
                {
                    part.Value.Add(tempTexture);
                }

                textureCount++;
            }
            while (tempTexture != null);
        }
    }

    private Texture2D GetSelectedTextureByType(AppearancePartType type)
    {
        var selectedIndex = selectedParts[type];
        return partsTextures[type][selectedIndex];
    }
}
