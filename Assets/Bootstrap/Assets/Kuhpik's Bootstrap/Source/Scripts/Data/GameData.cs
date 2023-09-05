using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Source.Scripts.Components;
using Source.Scripts.ScriptableObjects;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class GameData
    {
        public TableComponent table;
        public Stack<CardComponent> cards = new Stack<CardComponent>();
        
        //Configs
        public List<CardConfig> cardConfigs = new List<CardConfig>();
    }
}