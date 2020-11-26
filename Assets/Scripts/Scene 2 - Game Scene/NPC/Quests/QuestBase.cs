using System;
using UnityEngine;

public abstract class QuestBase : ScriptableObject
{
    public string title;
    [TextArea(3, 240)]
    public string description;

    public QuestReward reward;

    public QuestStatus status = QuestStatus.None;

    public abstract bool AreRequirementsReached();

    public abstract string[] GetCompletionProgress();

    public virtual void UpdateProgress()
    {
        if (status != QuestStatus.Accepted && status != QuestStatus.RequirementsReached)
        {
            return;
        }

        if (AreRequirementsReached())
        {
            status = QuestStatus.RequirementsReached;
        }
        else
        {
            status = QuestStatus.Accepted;
        }
    }

    public virtual void Complete() 
    {
        Player.instance.ChangeGold(reward.gold);
        
        foreach(var item in reward.items)
        {
            Player.instance.inventory.TryAddItem(item);
        }

        Player.instance.quests.Remove(this);

        status = QuestStatus.Completed;
    }

    public virtual void Accept()
    {
        status = QuestStatus.Accepted;
        Player.instance.quests.Add(this);
        UpdateProgress();
    }

    public virtual void Cancel()
    {
        status = QuestStatus.Canceled;
        Player.instance.quests.Remove(this);
    }
}

[Serializable]
public class QuestReward
{
    public int gold;
    public int exp;
    public Item[] items;
}

public enum QuestStatus
{
    None = 0,
    Accepted = 1,
    RequirementsReached = 2,
    Completed = 3,
    Canceled = 4,
}