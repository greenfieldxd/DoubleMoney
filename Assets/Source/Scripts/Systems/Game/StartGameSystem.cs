using Kuhpik;
using Source.Scripts.Enums;
using Source.Scripts.ScriptableObjects;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class StartGameSystem : GameSystem
    {
        [SerializeField] private DuelConfig duelConfig;
        [SerializeField] private int startMoneyCount = 10;
        
        public override void OnInit()
        {
            game.OnMoneyChanged += UpdateMoney;

            game.roundsCount = duelConfig.GetRoundsCount(player.winsCount);
            game.MyMoney = startMoneyCount;
            game.OpponentMoney = startMoneyCount;
        }

        private void UpdateMoney()
        {
            game.table.MyTextField.text = $"{game.MyMoney}$";
            game.table.OpponentTextField.text = $"{game.OpponentMoney}$";
        }
    }
}