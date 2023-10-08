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
                else StartCoroutine(Result());
            }
        }

        private IEnumerator Result()
        {
            yield return new WaitForSeconds(restartDelay);
            Bootstrap.Instance.ChangeGameState(GameStateID.Result);
        }
    }
}