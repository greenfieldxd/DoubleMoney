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

        private float _moneyValue;
        
        public override void OnInit()
        {
            Supyrb.Signals.Get<AddTableMoneySignal>().AddListener(AddTableMoney);
            AddTableMoney();
        }

        private void AddTableMoney()
        {
            Money(TurnType.My);
            Money(TurnType.Opponent);
        }
        
        private void Money(TurnType turnType)
        {
            var stack = game.table.DuelistContainers.First(x => x.turnType == turnType).moneyStack;
            
            if (turnType == TurnType.My) _moneyValue =  game.MyMoney / (float)game.startMoney;
            else if (turnType == TurnType.Opponent) _moneyValue = game.OpponentMoney / (float)game.startMoney;

            var delta = Mathf.Abs(stack.ItemsCount - _moneyValue);
            var isAdd = stack.ItemsCount < _moneyValue;
            
            Debug.Log("Money value: " + _moneyValue);
            Debug.Log("Delta: " + delta);
            Debug.Log("IsAdd: " + isAdd);
            
            if (Math.Abs(delta) < 1) return;

            for (int i = 0; i < delta; i++)
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