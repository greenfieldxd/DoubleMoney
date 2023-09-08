using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Source.Scripts.ScriptableObjects;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class CalculateSystem : GameSystem
    {
        public override void OnInit()
        {
            Supyrb.Signals.Get<CalculateSignal>().AddListener(Calculate);
        }

        private void Calculate(TurnType turnType, CardComponent card)
        {
            switch (turnType)
            {
                case TurnType.My:
                    MyTurnOperation(card);
                    break;
                
                case TurnType.Opponent:
                    break;
            }
        }

        private void MyTurnOperation(CardComponent card)
        {
            var cardConfig = card.Config;
            
            switch (cardConfig.CardType)
            {
                case CardType.Add:
                    game.table.MyStack.PushToStackWithJump(card.transform, () => game.MyMoney += (int)cardConfig.Value);
                    break;
                
                case CardType.Multiply:
                    game.table.MyStack.PushToStackWithJump(card.transform, () => game.MyMoney *= (int)cardConfig.Value);
                    break;
                
                case CardType.Divide:
                    game.table.OpponentStack.PushToStackWithJump(card.transform, () => game.OpponentMoney = Mathf.RoundToInt(game.OpponentMoney / cardConfig.Value));
                    break;
                
                case CardType.AddPercentage:
                    game.table.MyStack.PushToStackWithJump(card.transform, () => game.MyMoney = Mathf.RoundToInt(game.MyMoney + game.MyMoney * cardConfig.Value / 100));
                    break;
                
                case CardType.SubtractPercentage:
                    game.table.OpponentStack.PushToStackWithJump(card.transform, () => game.OpponentMoney = Mathf.RoundToInt(game.OpponentMoney - game.OpponentMoney * cardConfig.Value / 100));
                    break;
                
                case CardType.StealPercentage:
                    game.table.OpponentStack.PushToStackWithJump(card.transform, () =>
                    {
                        var value = Mathf.RoundToInt(game.OpponentMoney * cardConfig.Value / 100);
                        game.OpponentMoney -= value;
                        game.MyMoney += value;
                    });
                    break;
                
                case CardType.AddMove:
                    game.movesCount += (int)cardConfig.Value;
                    break;
            }
        }
    }
}