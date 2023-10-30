using Kuhpik;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class ResultSystem : GameSystem
    {
        public override void OnInit()
        {
            ApplyResult();
        }

        private void ApplyResult()
        {
            var victory = game.MyMoney > game.OpponentMoney;
                    
            if (victory)
            {
                player.winsCount++;
                player.Money += game.MyMoney + game.OpponentMoney;
                player.RecordMoney += game.MyMoney + game.OpponentMoney;
                OtherExtensions.SaveGame(player);

                YandexSDK.SetLB(YandexData());
            }

            //test
            Bootstrap.Instance.GameRestart(0);
        }
        string YandexData()
        {
            YandexData data = new YandexData()
            {
                BoardName = "leader",
                Value = player.RecordMoney
            };

            return JsonUtility.ToJson(data);
        }
    }
}