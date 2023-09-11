using System;
using System.Collections;
using DG.Tweening;
using Kuhpik;
using Source.Scripts.Enums;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class RockPaperScissorsSystem : GameSystemWithScreen<GameUIScreen>
    {
        [SerializeField] private float startMiniGameDelay = 2f;
        [SerializeField] private RockPaperScissorsType[] types;

        private RockPaperScissorsType _my;
        private RockPaperScissorsType _opponent;
        
        public override void OnInit()
        {
            foreach (var button in screen.RockPaperScissorsButtons)
            {
                button.button.onClick.AddListener(() => SelectTypes(button.type));
            }
            
            screen.SelectRandom.onClick.AddListener(() => SelectTypes(types.GetRandom()));
            StartCoroutine(StartMiniGame());
        }

        private IEnumerator StartMiniGame()
        {
            screen.ButtonsHolder.transform.DOScale(Vector3.zero, 0);
            yield return new WaitForSeconds(startMiniGameDelay);
            
            screen.ButtonsHolder.SetActive(true);
            screen.ButtonsHolder.transform.DOScale(Vector3.one, 0.2f);
        }

        private void SelectTypes(RockPaperScissorsType type)
        {
            _my = type;
            _opponent = types.GetRandom();
            
            Debug.Log($"My Type: {_my}");
            Debug.Log($"Opponent Type: {_opponent}");

            if (WhoWin() != TurnType.None)
            {
                screen.ButtonsHolder.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => screen.ButtonsHolder.SetActive(false));

                game.CurrentTurn = WhoWin();
                game.movesCount++;
            }
        }

        private TurnType WhoWin()
        {
            if (_my == _opponent) return TurnType.None;
            else if (_my == RockPaperScissorsType.Rock && _opponent == RockPaperScissorsType.Scissors) return TurnType.My;
            else if (_my == RockPaperScissorsType.Paper && _opponent == RockPaperScissorsType.Rock) return TurnType.My;
            else if (_my == RockPaperScissorsType.Scissors && _opponent == RockPaperScissorsType.Paper) return TurnType.My;
            else if (_my == RockPaperScissorsType.Rock && _opponent == RockPaperScissorsType.Paper) return TurnType.Opponent;
            else if (_my == RockPaperScissorsType.Paper && _opponent == RockPaperScissorsType.Scissors) return TurnType.Opponent;
            else if (_my == RockPaperScissorsType.Scissors && _opponent == RockPaperScissorsType.Rock) return TurnType.Opponent;

            return TurnType.None;
        }
    }
}