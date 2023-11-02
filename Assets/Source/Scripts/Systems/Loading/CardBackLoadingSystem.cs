using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class CardBackLoadingSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] string path;

    [SerializeField, BoxGroup("Testing")] int cardBackIndex;
    [SerializeField, Button("Card Back Select")] void ButtonAction() => player.cardBackIndex = cardBackIndex;

    public override void OnInit()
    {
        CardBackLoading();

        UpdateData();
    }
    void CardBackLoading()
    {
        game.CardBackList = new List<CardBackConfig>();

        var resources = Resources.LoadAll<CardBackConfig>(path);
        foreach (var resource in resources)
        {
            game.CardBackList.Add(resource);
        }
    }
    void UpdateData()
    {
        if (player.cardBackList == null)
        {
            player.cardBackList = new List<int>();
            player.cardBackList.Add(2);

            player.cardBackIndex = 0;
        }

        int count = player.cardBackList.Count;
        int maxCount = game.CardBackList.Count;

        for (int i = count; i < maxCount; i++)
        {
            player.cardBackList.Add(0);
        }

        OtherExtensions.SaveGame(player);
    }
}