using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class CardBackLoadingSystem : GameSystem
{
    [SerializeField, BoxGroup("Settings")] string path;

    [SerializeField, BoxGroup("Testing")] int cardBackIndex;
    [SerializeField, Button("Card Back Select")] void ButtonAction() => player.CardBackIndex = cardBackIndex;

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
        if (player.CardBackList == null)
        {
            player.CardBackList = new List<int>();
            player.CardBackList.Add(2);

            player.CardBackIndex = 0;
        }

        int count = player.CardBackList.Count;
        int maxCount = game.CardBackList.Count;

        for (int i = count; i < maxCount; i++)
        {
            player.CardBackList.Add(0);
        }

        Extensions.SaveGame(player);
    }
}