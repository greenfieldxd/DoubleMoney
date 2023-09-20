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
        
        public override void OnInit()
        {
            Supyrb.Signals.Get<AddTableMoneySignal>().AddListener(AddTableMoney);
        }

        private void AddTableMoney(TurnType turnType, CardType cardType)
        {
            var myStack = game.table.DuelistContainers.First(x => x.turnType == TurnType.My).moneyStack;
            var opponentStack = game.table.DuelistContainers.First(x => x.turnType == TurnType.Opponent).moneyStack;
            
             switch (cardType)
            {
                case CardType.Add:
                    Money(turnType == TurnType.My ? myStack : opponentStack, true, 2);
                    break;
                
                case CardType.Multiply:
                    Money(turnType == TurnType.My ? myStack : opponentStack, true, 2);
                    break;
                
                case CardType.Divide:
                    Money(turnType == TurnType.My ? opponentStack : myStack, false, 2);
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
                        Money(myStack, true, 2);
                        Money(opponentStack, false);
                    }
                    else if (turnType == TurnType.Opponent)
                    {
                        Money(opponentStack, true, 2);
                        Money(myStack, false);
                    }
                    break;
            }
        }

        private void Money(StackComponent stack, bool isAdd, int howMatch = 1)
        {
            for (int i = 0; i < howMatch; i++)
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