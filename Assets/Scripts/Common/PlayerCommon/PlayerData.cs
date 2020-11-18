using System;

namespace Assets.Scripts.Common.PlayerCommon
{
    [Serializable]
    public class PlayerData
    {
        public PlayerAppearance appearance;

        public PlayerStats stats;

        public string name;

        public PlayerProfessionType playerClass;

        public PlayerData()
        {
        }

        public PlayerData(Player player)
        {
            appearance = player.appearance;
            stats = player.playerStats;
        }
    }
}