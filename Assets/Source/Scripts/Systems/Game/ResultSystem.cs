using DG.Tweening;
using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Extensions;
using Source.Scripts.UI;
using System.Collections;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class ResultSystem : GameSystemWithScreen<ResultUIScreen>
    {
        [SerializeField, BoxGroup("Settings")] float comparisonTime = 1f;
        [SerializeField, BoxGroup("Settings")] float resultDelay = 0.5f;

        public override void OnInit()
        {
            screen.HomeButton.onClick.AddListener(() =>
            {
                YandexSDK.ShowInterstitial(OtherExtensions.YandexAdData());
                Bootstrap.Instance.GameRestart(0);
            });

            screen.ResultPanel.SetActive(false);
            StartCoroutine(MoneyComparison());
        }

        IEnumerator MoneyComparison()
        {
            foreach (var container in game.table.DuelistContainers)
            {
                container.moneyStack.transform.DOMove(container.resultPoint.position, comparisonTime);
                container.cardsStack.gameObject.SetActive(false);
                container.hand.gameObject.SetActive(false);
            }

            CameraController.Instance.SwitchCameraWithDelay(3);

            yield return new WaitForSeconds(comparisonTime);

            StartCoroutine(ApplyResult());
        }

        IEnumerator ApplyResult()
        {
            var victory = game.MyMoney > game.OpponentMoney;

            screen.WinPanel.SetActive(victory);
            screen.LosePanel.SetActive(!victory);
            screen.Reward.text = OtherExtensions.FormatNumberWithCommas(victory ? game.MyMoney + game.OpponentMoney : 0) + "$";

            if (victory)
            {
                player.winsCount++;
                player.Money += game.MyMoney + game.OpponentMoney;
                player.RecordMoney += game.MyMoney + game.OpponentMoney;
                
                if (player.winsCount % 5 == 0) player.planeMatIndex++;

                foreach (var effect in game.table.WinEffects)
                {
                    effect.Play();
                }
                
                OtherExtensions.SaveGame(player);
                YandexSDK.SetLB(YandexData());
            }

            yield return new WaitForSeconds(resultDelay);

            game.audioSystem.CreateMusic(-1);
            game.audioSystem.CreateSound(4);

            screen.ResultPanel.SetActive(true);

            if (player.RecordMoney >= 1000) YandexSDK.ShowRateGame();
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