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
        public Camera MainCamera;

        public DuelConfig currentDuelConfig;
        public int movesCount;

        [SerializeField] private TurnType currentTurnType = TurnType.None;
        [SerializeField] private int myMoney;
        [SerializeField] private int opponentMoney;
        
        public event Action<int> OnMyMoneyChanged;
        public event Action<int> OnOpponentMoneyChanged;
        public event Action OnTurnChanged;

        public int MyMoney
        {
            get => myMoney;
            set
            {
                var oldValue = myMoney;
                var delta = Mathf.Abs(myMoney - value);
                myMoney = Mathf.Clamp(value,0,999999999);
                
                if (delta > 0) OnMyMoneyChanged?.Invoke(oldValue);
            }
        }

        public TurnType CurrentTurn
        {
            get => currentTurnType;
            set
            {
                if (value != currentTurnType)
                {
                    currentTurnType = value;
                    Debug.Log("Current: " + currentTurnType);
                    OnTurnChanged?.Invoke();
                }
            }
        }

        public int OpponentMoney
        {
            get => opponentMoney;
            set
            {
                var oldValue = opponentMoney;
                var delta = Mathf.Abs(opponentMoney - value);
                opponentMoney = Mathf.Clamp(value,0,999999999);
                
                if (delta > 0) OnOpponentMoneyChanged?.Invoke(oldValue);
            }
        }
        
        public TableComponent table;
        public BoardComponent board;
        public Stack<CardComponent> cardsInDeck = new Stack<CardComponent>();
        public List<CardComponent> cardsOnBoard = new List<CardComponent>();
        
        //Configs
        public List<CardConfig> cardConfigs = new List<CardConfig>();
        public List<DuelConfig> duelConfigs = new List<DuelConfig>();
    }
}