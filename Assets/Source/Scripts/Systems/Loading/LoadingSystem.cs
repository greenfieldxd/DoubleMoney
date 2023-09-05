using System.Collections.Generic;
using Kuhpik;
using Source.Scripts.ScriptableObjects;
using UnityEngine;

namespace Source.Scripts.Systems.Loading
{
    public class LoadingSystem : GameSystem
    {
        [SerializeField] private List<CardConfig> cardConfigs;
        
        public override void OnInit()
        {
            game.cardConfigs = cardConfigs;
        }
    }
}
