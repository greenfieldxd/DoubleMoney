using System;
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
            Supyrb.Signals.Get<AddTableMoneySignal>().AddListener(ChangeTableMoney);
            ChangeTableMoney();
        }

        private void ChangeTableMoney()
        {
            var myValue =  (int)(game.MyMoney / (float)game.startMoney);
            var opponentValue = (int)(game.OpponentMoney / (float)game.startMoney);

            ChangeStack(TurnType.My, myValue);
            ChangeStack(TurnType.Opponent, opponentValue);
        }

        private void ChangeStack(TurnType turnType, int stackValue)
        {
            var stack = game.table.DuelistContainers.First(x => x.turnType == turnType).moneyStack;
            var delta = stack.ItemsCount - stackValue;
            var isAdd = stack.ItemsCount < stackValue;

            if (Math.Abs(delta) == 0) return;

            for (int i = 0; i < Math.Abs(delta); i++)
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