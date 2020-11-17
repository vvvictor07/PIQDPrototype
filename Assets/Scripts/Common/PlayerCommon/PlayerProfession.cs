using System;
using System.Collections.Generic;

namespace Assets.Scripts.Common.PlayerCommon
{
    [Serializable]
    public class PlayerProfession
    {
        public string ProfessionName = "Profession";

        public string AbilityName = "Ability";
        public string AbilityDescription = "Does an action";

        public Dictionary<PlayerStatType, int> defaultStats = new Dictionary<PlayerStatType, int>()
        {
            { PlayerStatType.Strength, 0 },
            { PlayerStatType.Dexterity, 0 },
            { PlayerStatType.Constitution, 0 },
            { PlayerStatType.Intelegent, 0 },
            { PlayerStatType.Wisdom, 0 },
            { PlayerStatType.Charisma, 0 },
        };
    }
}
