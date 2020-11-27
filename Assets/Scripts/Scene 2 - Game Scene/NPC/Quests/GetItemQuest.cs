using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "GetItemQuest", menuName = "Quests/GetItemQuest", order = 51)]
public class GetItemQuest : QuestBase
{
    public ItemToFind[] itemsToFind;
    public bool removeItemsOnComplete;

    public override string[] GetCompletionProgress()
    {
        var progress = new List<string>();

        foreach (var itemToFind in itemsToFind)
        {
            var currentAmount = Player.instance.inventory.items
                .Where(item => item.id == itemToFind.item.id)
                .Sum(item => item.currentAmount);

            var itemProgress = $"{itemToFind.item.name}: {currentAmount}/{itemToFind.requiredAmount}";

            progress.Add(itemProgress);
        }

        return progress.ToArray();
    }

    public override bool AreRequirementsReached()
    {
        return itemsToFind.All(itemToFind => {
            var currentAmount = Player.instance.inventory.items
                .Where(item => item.id == itemToFind.item.id)
                .Sum(item => item.currentAmount);

            return currentAmount >= itemToFind.requiredAmount;
        });
    }

    public override void Complete()
    {
        if (removeItemsOnComplete)
        {
            foreach (var itemToFind in itemsToFind)
            {
                Player.instance.inventory.RemoveItem(itemToFind.item);
            }
        }

        Player.instance.inventory.OnStorageUpdate -= UpdateProgress;

        base.Complete();
    }

    public override void Accept()
    {
        Player.instance.inventory.OnStorageUpdate += UpdateProgress;
        base.Accept();
    }

    public override void Cancel()
    {
        Player.instance.inventory.OnStorageUpdate -= UpdateProgress;
        base.Cancel();
    }
}

[Serializable]
public class ItemToFind
{
    public Item item;
    public int requiredAmount;
}
