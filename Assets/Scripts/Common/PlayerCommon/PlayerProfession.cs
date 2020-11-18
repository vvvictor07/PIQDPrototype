using System;
using System.Collections.Generic;

namespace Assets.Scripts.Common.PlayerCommon
{
    public enum PlayerProfessionType
    {
        Barbarian = 0,
        Bard = 1,
        Druid = 2,
        Monk = 3,
        Paladin = 4,
        Ranger = 5,
        Sorcerer = 6,
        Warlock = 7,
    }

    [Serializable]
    public class PlayerProfession
    {
        public string professionName = "Profession";

        public string abilityName = "Ability";
        public string abilityDescription = "Does an action";

        public Dictionary<PlayerStatType, int> defaultStats = new Dictionary<PlayerStatType, int>()
        {
            { PlayerStatType.Strength, 0 },
            { PlayerStatType.Dexterity, 0 },
            { PlayerStatType.Constitution, 0 },
            { PlayerStatType.Intelegent, 0 },
            { PlayerStatType.Wisdom, 0 },
            { PlayerStatType.Charisma, 0 },
        };

        public static Dictionary<PlayerProfessionType, PlayerProfession> professions = new Dictionary<PlayerProfessionType, PlayerProfession>()
        {
            { PlayerProfessionType.Barbarian, new PlayerProfession
                {
                    professionName = "Barbarian",
                    defaultStats = new Dictionary<PlayerStatType, int>
                    {
                        { PlayerStatType.Strength, 3 },
                        { PlayerStatType.Dexterity, 2 },
                        { PlayerStatType.Constitution, 2 },
                        { PlayerStatType.Intelegent, 1 },
                        { PlayerStatType.Wisdom, 1 },
                        { PlayerStatType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Bard, new PlayerProfession
                {
                    professionName = "Bard",
                    defaultStats = new Dictionary<PlayerStatType, int>
                    {
                        { PlayerStatType.Strength, 1 },
                        { PlayerStatType.Dexterity, 1 },
                        { PlayerStatType.Constitution, 1 },
                        { PlayerStatType.Intelegent, 2 },
                        { PlayerStatType.Wisdom, 2 },
                        { PlayerStatType.Charisma, 3 },
                    }
                }
            },
            { PlayerProfessionType.Druid, new PlayerProfession
                {
                    professionName = "Druid",
                    defaultStats = new Dictionary<PlayerStatType, int>
                    {
                        { PlayerStatType.Strength, 1 },
                        { PlayerStatType.Dexterity, 2 },
                        { PlayerStatType.Constitution, 2 },
                        { PlayerStatType.Intelegent, 2 },
                        { PlayerStatType.Wisdom, 2 },
                        { PlayerStatType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Monk, new PlayerProfession
                {
                    professionName = "Monk",
                    defaultStats = new Dictionary<PlayerStatType, int>
                    {
                        { PlayerStatType.Strength, 1 },
                        { PlayerStatType.Dexterity, 3 },
                        { PlayerStatType.Constitution, 2 },
                        { PlayerStatType.Intelegent, 1 },
                        { PlayerStatType.Wisdom, 2 },
                        { PlayerStatType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Paladin, new PlayerProfession
                {
                    professionName = "Paladin",
                    defaultStats = new Dictionary<PlayerStatType, int>
                    {
                        { PlayerStatType.Strength, 2 },
                        { PlayerStatType.Dexterity, 1 },
                        { PlayerStatType.Constitution, 3 },
                        { PlayerStatType.Intelegent, 1 },
                        { PlayerStatType.Wisdom, 2 },
                        { PlayerStatType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Ranger, new PlayerProfession
                {
                    professionName = "Ranger",
                    defaultStats = new Dictionary<PlayerStatType, int>
                    {
                        { PlayerStatType.Strength, 1 },
                        { PlayerStatType.Dexterity, 4 },
                        { PlayerStatType.Constitution, 1 },
                        { PlayerStatType.Intelegent, 2 },
                        { PlayerStatType.Wisdom, 1 },
                        { PlayerStatType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Sorcerer, new PlayerProfession
                {
                    professionName = "Sorcerer",
                    defaultStats = new Dictionary<PlayerStatType, int>
                    {
                        { PlayerStatType.Strength, 1 },
                        { PlayerStatType.Dexterity, 1 },
                        { PlayerStatType.Constitution, 1 },
                        { PlayerStatType.Intelegent, 3 },
                        { PlayerStatType.Wisdom, 2 },
                        { PlayerStatType.Charisma, 2 },
                    }
                }
            },
            { PlayerProfessionType.Warlock, new PlayerProfession
                {
                    professionName = "Warlock",
                    defaultStats = new Dictionary<PlayerStatType, int>
                    {
                        { PlayerStatType.Strength, 1 },
                        { PlayerStatType.Dexterity, 1 },
                        { PlayerStatType.Constitution, 1 },
                        { PlayerStatType.Intelegent, 4 },
                        { PlayerStatType.Wisdom, 2 },
                        { PlayerStatType.Charisma, 1 },
                    }
                }
            },
        };
    }
}
