using System;
using System.Globalization;
using System.Linq;
using DG.Tweening;
using Kuhpik;
using Source.Scripts.Enums;
using Source.Scripts.Extensions;
using Source.Scripts.ScriptableObjects;
using Source.Scripts.UI;
using UnityEngine;
using Random = System.Random;

namespace Source.Scripts.Systems.Game
{
    public class InitGameSystem : GameSystemWithScreen<GameUIScreen>
    {
        [SerializeField] private int startMoney = 10;
        
        private int _startMoney;
        
        public override void OnInit()
        {
            UpdateTableMoney();

            game.OnMyMoneyChanged += UpdateMyMoney;
            game.OnOpponentMoneyChanged += UpdateOpponentMoney;

            game.currentDuelConfig = GetDuelConfig();
            _startMoney = RoundUp(Mathf.Clamp(UnityEngine.Random.Range(startMoney, startMoney * (player.winsCount + 1)), 0, 1000));
            game.MyMoney = _startMoney;
            game.OpponentMoney = _startMoney;
        }

        private void UpdateTableMoney()
        {
            game.table.DuelistContainers.First(x => x.turnType == TurnType.My).moneyText.text = $"{OtherExtensions.FormatNumberWithCommas(game.MyMoney)}$";
            game.table.DuelistContainers.First(x => x.turnType == TurnType.Opponent).moneyText.text = $"{OtherExtensions.FormatNumberWithCommas(game.OpponentMoney)}$";
        }

        private DuelConfig GetDuelConfig()
        {
            var winsCount = player.winsCount;
            
                var canGetVariants = game.duelConfigs.Where(x => winsCount >= x.NeededWins).ToArray();
                var rng = new Random();
                rng.Shuffle(canGetVariants);
                return canGetVariants.First();
        }
        
        private int RoundUp(int val)
        {
            int tmp = 0;
            int number = val;
            if ((tmp = number % 10) != 0) number += number > -1 ? 10 - tmp : -tmp;
            
            return number;
        }

        private void UpdateMyMoney(int oldValue)
        {
            float valFloat = oldValue;
            Action action = () =>  game.table.DuelistContainers.First(x => x.turnType == TurnType.My).moneyText.text = $"{OtherExtensions.FormatNumberWithCommas((int)valFloat)}$";
            DOTween.To(() => valFloat, x => valFloat = x, game.MyMoney, 0.5f).OnUpdate(() => action.Invoke());
        }
        
        private void UpdateOpponentMoney(int oldValue)
        {
            float valFloat = oldValue;
            Action action = () => game.table.DuelistContainers.First(x => x.turnType == TurnType.Opponent).moneyText.text  = $"{OtherExtensions.FormatNumberWithCommas((int)valFloat)}$";
            DOTween.To(() => valFloat, x => valFloat = x, game.OpponentMoney, 0.5f).OnUpdate(() => action.Invoke());
        }
    }
}