using System.Linq;
using Kuhpik;
using Lean.Pool;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class TableMoneySystem : GameSystem
    {
        [SerializeField] private MoneyComponent moneyPrefab;

        private int _changeValue;
        private int _moneyValue;
        
        public override void OnInit()
        {
            Supyrb.Signals.Get<AddTableMoneySignal>().AddListener(AddTableMoney);
            
            var myStack = game.table.DuelistContainers.First(x => x.turnType == TurnType.My).moneyStack;
            var opponentStack = game.table.DuelistContainers.First(x => x.turnType == TurnType.Opponent).moneyStack;
            _moneyValue = Random.Range(1, 4);
            Money(myStack, true);
            Money(opponentStack, true);
        }

        private void AddTableMoney(TurnType turnType, CardType cardType)
        {
            var myStack = game.table.DuelistContainers.First(x => x.turnType == TurnType.My).moneyStack;
            var opponentStack = game.table.DuelistContainers.First(x => x.turnType == TurnType.Opponent).moneyStack;
            _changeValue = Mathf.Clamp(Mathf.Abs(myStack.ItemsCount - opponentStack.ItemsCount), 1, 5);
            CalculateMoneyValue(turnType);

            switch (cardType)
            {
                case CardType.Add:
                    Money(turnType == TurnType.My ? myStack : opponentStack, true);
                    break;
                
                case CardType.Multiply:
                    Money(turnType == TurnType.My ? myStack : opponentStack, true);
                    break;
                
                case CardType.Divide:
                    Money(turnType == TurnType.My ? opponentStack : myStack, false);
                    break;
                
                case CardType.AddPercentage:
                    Money(turnType == TurnType.My ? myStack : opponentStack, true);
                    break;
                
                case CardType.SubtractPercentage:
                    Money(turnType == TurnType.My ? opponentStack : myStack, false);
                    break;
                
                case CardType.StealPercentage:
                    if (turnType == TurnType.My)
                    {
                        Money(myStack, true);
                        Money(opponentStack, false);
                    }
                    else if (turnType == TurnType.Opponent)
                    {
                        Money(opponentStack, true);
                        Money(myStack, false);
                    }
                    break;
            }
        }

        private void CalculateMoneyValue(TurnType turnType)
        {
            if (turnType == TurnType.My) _moneyValue =  game.MyMoney > game.OpponentMoney ? _changeValue + 1 : _changeValue - 1;
            else if (turnType == TurnType.Opponent) _moneyValue =  game.OpponentMoney > game.MyMoney ? _changeValue + 1 : _changeValue - 1;
        }

        private void Money(StackComponent stack, bool isAdd)
        {
            for (int i = 0; i < _moneyValue; i++)
            {
                if (isAdd)
                {
                    var money = LeanPool.Spawn(moneyPrefab, stack.transform.position, Quaternion.identity);
                    stack.PushToStack(money.transform, 0);
                }
                else
                {
                    if (stack.ItemsCount <= 1) continue;
                    
                    var money = stack.PopFromStack();
                    if (money != null) LeanPool.Despawn(money);
                }
            }
        }
    }
}