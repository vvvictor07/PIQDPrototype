using System;
using System.Linq;

namespace Assets.Scripts.Common.PlayerCommon
{
    public enum AppearancePartType
    {
        Skin = 0,
        Hair = 1,
        Eyes = 2,
        Mouth = 3,
        Clothes = 4,
        Armour = 5,
    };

    [Serializable]
    public class PlayerAppearance
    {
        public PlayerAppearancePart[] parts = new PlayerAppearancePart[]
        {
            new PlayerAppearancePart() { partType = AppearancePartType.Eyes },
            new PlayerAppearancePart() { partType = AppearancePartType.Clothes },
            new PlayerAppearancePart() { partType = AppearancePartType.Armour },
            new PlayerAppearancePart() { partType = AppearancePartType.Mouth },
            new PlayerAppearancePart() { partType = AppearancePartType.Skin },
            new PlayerAppearancePart() { partType = AppearancePartType.Hair },
        };

        public void SetPartTexture(AppearancePartType type, string name)
        {
            var part = GetPart(type);
            part.textureName = name;
        }

        public PlayerAppearancePart GetPart(AppearancePartType type)
        {
            return parts.Single(part => part.partType == type);
        }
    }

    [Serializable]
    public class PlayerAppearancePart
    {
        public string textureName;
        public AppearancePartType partType;
    }
}
