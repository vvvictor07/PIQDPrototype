using Assets.Scripts.Common.PlayerCommon;
using UnityEngine;
using UnityEngine.UI;

public class CustomizationStatController : MonoBehaviour
{
    public PlayerStatType statType;
    public string statName;
    public Text textElement;

    void Start()
    {
        CustomizationController.instance.OnStatsChange += UpdateValue;
    }

    public void IncrementPlayerStat()
    {
        CustomizationController.instance.SetStat(statType, 1);
    }

    public void DecrementPlayerStat()
    {
        CustomizationController.instance.SetStat(statType, -1);
    }

    private void UpdateValue(PlayerStats stats)
    {
        var value = stats.GetStats(statType).finalStat;
        textElement.text = $"{statName}: {value}";
    }
}
