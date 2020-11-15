using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Customization : MonoBehaviour
{

    [SerializeField]
    private string TextureLocation = "Character/";
    public enum CustomiseParts { Skin, Hair, Eyes, Mouth, Clothes, Armour };

    public Renderer characterRenderer;

    public List<Texture2D>[] partsTexture = new List<Texture2D>[Enum.GetNames(typeof(CustomiseParts)).Length];

    public int[] currentPartsTextureIndex = new int[Enum.GetNames(typeof(CustomiseParts)).Length];

    private void Start()
    {
        int partCount = 0;
        foreach (string part in Enum.GetNames(typeof(CustomiseParts)))
        {
            int textureCount = 0;
            
            Texture2D tempTexture;

            partsTexture[partCount] = new List<Texture2D>();
            do
            {
                tempTexture = (Texture2D) Resources.Load(TextureLocation + part + "_" + textureCount);
                
                if(tempTexture != null)
                {
                    partsTexture[partCount].Add(tempTexture);
                }
                textureCount++;
            } while (tempTexture != null);
            partCount++;

        }
        //if(playerProfessions = !null && PlayerProfession.Length > 0)
        //{
        //    player.Profession = playerProfessions[0];
        //}
    }

    void SetTextrue(string type, int direction)
    {
        int partIndex = 0;

        switch (type)
        {
            case "Skin":
                //set textures
                partIndex = 0;
                break;
            case "Hair":
                partIndex = 1;
                break;
            case "Eyes":
                partIndex = 2;
                break;
            case "Mouth":
                partIndex = 3;
                break;
            case "Clothes":
                partIndex = 4;
                break;
            case "Armour":
                partIndex = 5;
                break;
            default:
                Debug.LogError("Invalid set texture type");
                break;
        }

        int max = partsTexture[partIndex].Count;

        int currentTexture = currentPartsTextureIndex[partIndex]++;
        currentTexture += direction;
        if(currentTexture < 0)
        {
            currentTexture = max - 1;

        }
        else if (currentTexture > max - 1)
        {
            currentTexture = 0;
        }
        currentPartsTextureIndex[partIndex] = currentTexture;

        Material[] mats = characterRenderer.materials;
        mats[partIndex].mainTexture = partsTexture[partIndex][currentTexture];
        characterRenderer.materials = mats;
    }

    public void SaveCharacter()
    {
        PlayerPrefs.SetInt("Skin Index", currentPartsTextureIndex[0]);
        PlayerPrefs.SetInt("Hair Index", currentPartsTextureIndex[1]);
        PlayerPrefs.SetInt("Eyes Index", currentPartsTextureIndex[2]);
        PlayerPrefs.SetInt("Mouth Index", currentPartsTextureIndex[3]);
        PlayerPrefs.SetInt("Clothes Index", currentPartsTextureIndex[4]);
        PlayerPrefs.SetInt("Arnor Index", currentPartsTextureIndex[5]);

        //save character name

        //PlayerPrefs.SetString("CharacterName", characterName);

        //save stats

        //for(int i = o; i < player.playerStats.baseStats.Length; i++)
        //{

        //PlayerPrefs.SetInt(player.playerStats.baseStats[i].baseStatName + " defaultStat", player.playerStats.baseStats[i].defaultStat);
        //PlayerPrefs.SetInt(player.playerStats.baseStats[i].baseStatName + " defaultStat", player.playerStats.baseStats[i].additionalStat);
        //PlayerPrefs.SetInt(player.playerStats.baseStats[i].baseStatName + " defaultStat", player.playerStats.baseStats[i].levelUpStat);

        //}

        ////PlayerPrefs.SetString("Character Profession" player.Profession.ProfessionName);
    }

    //testing with IGUI for character customization
    private void OnGUI()
    {
        float currentHeight = 40; // distance to increase between buttons for height

        GUI.Box(new Rect(Screen.width - 110, 10, 100, 90), "Top Right");

        GUI.Box(new Rect(10, 10, 100, 210), "Visuals");

        //method inside for loop  -- Skin, Hair, Eyes, Mouth, Clothes, Armour 
        string[] names = { "Skin", "Hair", "Eyes", "Mouth", "Clothes", "Armour" };

        for (int i = 0; i < names.Length; i++)
        {
            if (GUI.Button(new Rect(20, currentHeight + i * 30, 20, 20), "<"))
            {
                SetTextrue(names[i], -1);
            }

            GUI.Label(new Rect(45, currentHeight + i * 30, 60, 20), names[i]);

            if (GUI.Button(new Rect(80, currentHeight + i * 30, 20, 20), ">"))
            {
                SetTextrue(names[i], 1);
            }
        }
    }
}
