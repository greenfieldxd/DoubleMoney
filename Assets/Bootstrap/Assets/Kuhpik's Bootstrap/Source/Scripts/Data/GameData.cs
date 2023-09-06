using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Source.Scripts;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Source.Scripts.ScriptableObjects;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class GameData
    {
        public TurnType currentTurnType = TurnType.My;
        public int movesCount;
        public bool blockClicks = true;

        [SerializeField] private int myMoney;
        [SerializeField] private int opponentMoney;
        
        public Actions actions = new Actions();
        
        public event Action OnMoneyChanged;

        public int MyMoney
        {
            get => myMoney;
            set
            {
                var delta = Mathf.Abs(myMoney - value);
                myMoney = Mathf.Clamp(value,0,999999999);
                
                if (delta>0) OnMoneyChanged?.Invoke();
            }
        }

        public int OpponentMoney
        {
            get => opponentMoney;
            set
            {
                var delta = Mathf.Abs(opponentMoney - value);
                opponentMoney = Mathf.Clamp(value,0,999999999);
                
                if (delta>0) OnMoneyChanged?.Invoke();
            }
        }
        
        public TableComponent table;
        public Stack<CardComponent> cardsInDeck = new Stack<CardComponent>();
        public List<CardComponent> cardsOnBoard = new List<CardComponent>();
        
        //Configs
        public List<CardConfig> cardConfigs = new List<CardConfig>();
    }
}