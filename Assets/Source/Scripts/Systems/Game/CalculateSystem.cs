﻿using System.Collections;
using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Source.Scripts.ScriptableObjects;
using Source.Scripts.Signals;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class CalculateSystem : GameSystemWithScreen<GameUIScreen>
    {
        [SerializeField] private float nextTurnDelay = 1.5f;
        
        private TurnType _prevTurnType = TurnType.None;
        
        public override void OnInit()
        {
            Supyrb.Signals.Get<CalculateSignal>().AddListener(Calculate);
        }

        private void Calculate(TurnType turnType, CardComponent card)
        {
            _prevTurnType = turnType;
            game.CurrentTurn = TurnType.None;
            var cardConfig = card.Config;
            
            switch (cardConfig.CardType)
            {
                case CardType.Add:
                    if (turnType == TurnType.My) game.MyMoney += (int)cardConfig.Value;
                    else if (turnType == TurnType.Opponent) game.OpponentMoney += (int)cardConfig.Value;
                    break;
                
                case CardType.Multiply:
                    if (turnType == TurnType.My) game.MyMoney *= (int)cardConfig.Value;
                    else if (turnType == TurnType.Opponent) game.OpponentMoney *= (int)cardConfig.Value;
                    break;
                
                case CardType.Divide:
                    if (turnType == TurnType.My) game.OpponentMoney = Mathf.RoundToInt(game.OpponentMoney / cardConfig.Value);
                    else if (turnType == TurnType.Opponent) game.MyMoney = Mathf.RoundToInt(game.MyMoney / cardConfig.Value);
                    break;
                
                case CardType.AddPercentage:
                    if (turnType == TurnType.My) game.MyMoney = Mathf.RoundToInt(game.MyMoney + game.MyMoney * cardConfig.Value / 100);
                    else if (turnType == TurnType.Opponent) game.OpponentMoney = Mathf.RoundToInt(game.OpponentMoney + game.OpponentMoney * cardConfig.Value / 100);
                    break;
                
                case CardType.SubtractPercentage:
                    if (turnType == TurnType.My) game.OpponentMoney = Mathf.RoundToInt(game.OpponentMoney - game.OpponentMoney * cardConfig.Value / 100);
                    else if (turnType == TurnType.Opponent)game.MyMoney = Mathf.RoundToInt(game.MyMoney - game.MyMoney * cardConfig.Value / 100);
                    break;
                
                case CardType.StealPercentage:
                    if (turnType == TurnType.My)
                    {
                        var value = Mathf.RoundToInt(game.OpponentMoney * cardConfig.Value / 100);
                        game.OpponentMoney -= value;
                        game.MyMoney += value;
                    }
                    else if (turnType == TurnType.Opponent)
                    {
                        var value = Mathf.RoundToInt(game.MyMoney * cardConfig.Value / 100);
                        game.MyMoney -= value;
                        game.OpponentMoney += value;
                    }
                    break;
                
                case CardType.AddMove:
                    if (turnType == TurnType.My) game.movesCount += (int) cardConfig.Value;
                    else if (turnType == TurnType.Opponent) game.movesCount += (int) cardConfig.Value;
                    break;
            }

            game.movesCount--;
            
            StartCoroutine(DelayNextTurn());
        }

        private IEnumerator DelayNextTurn()
        {
            yield return new WaitForSeconds(nextTurnDelay);
            
            if (game.movesCount <= 0)
            {
                switch (_prevTurnType)
                {
                    case TurnType.My:
                        game.CurrentTurn = TurnType.Opponent;
                        break;
                    
                    case TurnType.Opponent:
                        game.CurrentTurn = TurnType.My;
                        break;
                }

                game.movesCount++;
            }
            else game.CurrentTurn = _prevTurnType;
            
            screen.UpdateTurnHolder(game.CurrentTurn);
            Supyrb.Signals.Get<CheckResultSignal>().Dispatch();
        }
    }
}