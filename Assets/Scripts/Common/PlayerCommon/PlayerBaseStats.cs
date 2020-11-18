using System;

namespace Assets.Scripts.Common.PlayerCommon
{
    public enum PlayerBaseStatsType
    {
        Strength = 0,
        Dexterity = 1,
        Constitution = 2,
        Wisdom = 3,
        Intelegent = 4,
        Charisma = 5,
    }

    [Serializable]
    public class BaseStats
    {
        public string baseStatName;
        public int defaultStat;
        public int levelUpStat;
        public int additionalStat;
        public PlayerBaseStatsType statType;

        public int finalStat
        {
            get
            {
                return defaultStat + additionalStat + levelUpStat;
            }
        }
    }
}
