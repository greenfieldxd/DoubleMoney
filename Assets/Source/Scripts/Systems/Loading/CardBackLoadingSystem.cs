using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class CardBackLoadingSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] string path;

    [SerializeField, BoxGroup("Testing")] int cardBackIndex;
    [SerializeField, Button("Card Back Select")] void ButtonAction() => player.cardBackData.cardBackIndex = cardBackIndex;

    public override void OnInit()
    {
        CardBackLoading();

        UpdateData();
    }
    void CardBackLoading()
    {
        game.cardBackList = new List<CardBackConfig>();

        var resources = Resources.LoadAll<CardBackConfig>(path);
        foreach (var resource in resources)
        {
            game.cardBackList.Add(resource);
        }
    }
    void UpdateData()
    {
        if (player.cardBackData.cardBackList == null)
        {
            player.cardBackData.cardBackList = new List<int>();
            player.cardBackData.cardBackList.Add(2);

            player.cardBackData.cardBackIndex = 0;
        }

        int count = player.cardBackData.cardBackList.Count;
        int maxCount = game.cardBackList.Count;

        for (int i = count; i < maxCount; i++)
        {
            player.cardBackData.cardBackList.Add(0);
        }

        OtherExtensions.SaveGame(player);
    }
}