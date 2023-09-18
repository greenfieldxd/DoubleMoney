using System;
using System.Globalization;
using System.Linq;
using DG.Tweening;
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
        private float _oldValue;
        
        public override void OnInit()
        {
            UpdateTableMoney();

            game.OnMyMoneyChanged += UpdateMyMoney;
            game.OnOpponentMoneyChanged += UpdateOpponentMoney;

            game.currentDuelConfig = GetDuelConfig();
            game.MyMoney = game.currentDuelConfig.StartMoneyCount;
            game.OpponentMoney = game.currentDuelConfig.StartMoneyCount;
        }

        private void UpdateTableMoney()
        {
            game.table.MyTextField.text = $"{OtherExtensions.FormatNumberWithCommas(game.MyMoney)}$";
            game.table.OpponentTextField.text = $"{OtherExtensions.FormatNumberWithCommas(game.OpponentMoney)}$";
        }

        private DuelConfig GetDuelConfig()
        {
            var winsCount = player.winsCount;
            
                var canGetVariants = game.duelConfigs.Where(x => winsCount >= x.NeededWins).ToArray();
                var rng = new Random();
                rng.Shuffle(canGetVariants);
                return canGetVariants.First();
        }

        private void UpdateMyMoney(int oldValue)
        {
            float valFloat = oldValue;
            Action action = () => game.table.MyTextField.text = $"{OtherExtensions.FormatNumberWithCommas((int)valFloat)}$";
            DOTween.To(() => valFloat, x => valFloat = x, game.MyMoney, 0.5f).OnUpdate(() => action.Invoke());
        }
        
        private void UpdateOpponentMoney(int oldValue)
        {
            float valFloat = oldValue;
            Action action = () => game.table.OpponentTextField.text = $"{OtherExtensions.FormatNumberWithCommas((int)valFloat)}$";
            DOTween.To(() => valFloat, x => valFloat = x, game.OpponentMoney, 0.5f).OnUpdate(() => action.Invoke());
        }
    }
}