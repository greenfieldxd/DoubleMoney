using Kuhpik;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class StartGameSystem : GameSystem
    {
        [SerializeField] private int startMoneyCount = 10;
        
        public override void OnInit()
        {
            game.OnMoneyChanged += UpdateMoney;
            
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