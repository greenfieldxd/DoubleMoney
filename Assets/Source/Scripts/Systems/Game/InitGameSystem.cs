using System.Linq;
using Kuhpik;
using Source.Scripts.Extensions;
using Source.Scripts.ScriptableObjects;
using Source.Scripts.UI;
using UnityEngine;
using Random = System.Random;

namespace Source.Scripts.Systems.Game
{
    public class InitGameSystem : GameSystemWithScreen<GameUIScreen>
    {
        public override void OnInit()
        {
            game.OnMoneyChanged += UpdateMoney;
            UpdateMoney();

            game.currentDuelConfig = GetDuelConfig();
            game.MyMoney = game.currentDuelConfig.StartMoneyCount;
            game.OpponentMoney = game.currentDuelConfig.StartMoneyCount;
        }

        private void UpdateMoney()
        {
            screen.UpdateMoney();
            game.table.MyTextField.text = $"{game.MyMoney}$";
            game.table.OpponentTextField.text = $"{game.OpponentMoney}$";
        }

        private DuelConfig GetDuelConfig()
        {
            var winsCount = player.winsCount;
            
                var canGetVariants = game.duelConfigs.Where(x => winsCount >= x.NeededWins).ToArray();
                var rng = new Random();
                rng.Shuffle(canGetVariants);
                return canGetVariants.First();
        }
    }
}