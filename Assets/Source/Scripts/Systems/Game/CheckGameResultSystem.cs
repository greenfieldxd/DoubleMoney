using System.Collections;
using Kuhpik;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class CheckGameResultSystem : GameSystem
    {
        [SerializeField] private float restartDelay = 1f;
        public override void OnInit()
        {
            Supyrb.Signals.Get<CheckResultSignal>().AddListener(CheckResult);
        }

        private void CheckResult()
        {
            if (game.cardsOnBoard.Count == 0)
            {
                if (game.cardsInDeck.Count > 0) Supyrb.Signals.Get<GetCardsFromDeckSignal>().Dispatch();
                else
                {
                    var victory = game.MyMoney > game.OpponentMoney;
                    
                    if (victory)
                    {
                        player.winsCount++;
                        player.Money += game.MyMoney + game.OpponentMoney;
                        Bootstrap.Instance.SaveGame();
                    }

                    StartCoroutine(GameRestart());
                }
            }
        }

        private IEnumerator GameRestart()
        {
            yield return new WaitForSeconds(restartDelay);
            
            Bootstrap.Instance.GameRestart(0);
        }
    }
}