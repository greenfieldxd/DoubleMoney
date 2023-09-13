using System.Collections.Generic;
using System.Linq;
using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.ScriptableObjects;
using UnityEngine;

namespace Source.Scripts.Systems.Loading
{
    public class LoadingSystem : GameSystem
    {
        public override void OnInit()
        {
            var cardConfigs = Resources.LoadAll("Cards", typeof(CardConfig)).Cast<CardConfig>().ToList();
            var duelConfigs = Resources.LoadAll("DuelConfigs", typeof(DuelConfig)).Cast<DuelConfig>().ToList();

            game.cardConfigs = cardConfigs;
            game.duelConfigs = duelConfigs;
            
            game.table = FindObjectOfType<TableComponent>();
        }
    }
}