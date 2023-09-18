using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class RockPaperScissorsSystem : GameSystemWithScreen<GameUIScreen>
    {
        [SerializeField] private float startMiniGameDelay = 2f;
        [SerializeField] private float handMoveDuration = 0.5f;
        [SerializeField] private RockPaperScissorsType[] types;

        private RockPaperScissorsType _my;
        private RockPaperScissorsType _opponent;
        
        private static readonly int Rock = Animator.StringToHash("Rock");
        private static readonly int Scissors = Animator.StringToHash("Scissors");
        private static readonly int Paper = Animator.StringToHash("Paper");

        
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
            ButtonStatus(false);
            
            _my = type;
            _opponent = types.GetRandom();
            
            MoveHand(TurnType.My, () => AnimateHand(TurnType.My, _my, true), game.table.HandMiniGameMy.position);
            MoveHand(TurnType.Opponent, () => AnimateHand(TurnType.Opponent, _opponent, true), game.table.HandMiniGameOpponent.position);
            
            Debug.Log($"My Type: {_my}");
            Debug.Log($"Opponent Type: {_opponent}");

            var haveWinner = WhoWin() != TurnType.None;
            
            StartCoroutine(BackHands(0.5f, haveWinner));
        }

        private IEnumerator BackHands(float delay, bool haveWinner)
        {
            yield return new WaitForSeconds(delay + handMoveDuration);
            
            var myHand = game.table.Hands.First(x => x.TurnType == TurnType.My);
            var opponentHand = game.table.Hands.First(x => x.TurnType == TurnType.Opponent);

            MoveHand(TurnType.My, () => AnimateHand(TurnType.My, _my, false), myHand.StartPosition);
            MoveHand(TurnType.Opponent, () => AnimateHand(TurnType.Opponent, _opponent, false), opponentHand.StartPosition);
            
            yield return new WaitForSeconds(handMoveDuration);

            if (haveWinner)
            {
                screen.ButtonsHolder.transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => screen.ButtonsHolder.SetActive(false));

                game.CurrentTurn = WhoWin();
                screen.UpdateTurnHolder(game.CurrentTurn);
                screen.TurnObject.SetActive(true);
                game.movesCount++;
            }
            else ButtonStatus(true);
        }
        
        private void MoveHand(TurnType turnType, Action action, Vector3 position, Transform parent = null)
        { 
            var hand = game.table.Hands.First(x => x.TurnType == turnType);

            if (parent != null)
            {
                hand.transform.SetParent(parent);
                hand.transform.DOLocalMove(position, handMoveDuration).OnComplete(() => action?.Invoke());
            }
            else hand.transform.DOMove(position, handMoveDuration).OnComplete(() => action?.Invoke());
        }

        private void AnimateHand(TurnType turnType, RockPaperScissorsType type, bool status)
        {
            int animKey = 0;

            switch (type)
            {
                case RockPaperScissorsType.Rock:
                    animKey = Rock;
                    break;
                
                case RockPaperScissorsType.Scissors:
                    animKey = Scissors;
                    break;
                
                case RockPaperScissorsType.Paper:
                    animKey = Paper;
                    break;
                
            }

            var hand = game.table.Hands.First(x => x.TurnType == turnType);
            hand.Animator.SetBool(animKey, status);
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

        private void ButtonStatus(bool status)
        {
            foreach (var button in screen.RockPaperScissorsButtons)
            {
                button.button.interactable = status;
            }

            screen.SelectRandom.interactable = status;
        }
    }
}