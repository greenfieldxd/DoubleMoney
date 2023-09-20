using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Data;
using Source.Scripts.Enums;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class HandMoveCardsSystem : GameSystem
    {
        [SerializeField] private float handMoveDuration = 0.5f;

        private static readonly int Take = Animator.StringToHash("Take");

        public override void OnInit()
        {
            Supyrb.Signals.Get<HandMoveSignal>().AddListener(MoveHand);
        }

        private void MoveHand(HandData data)
        {
            var hand = game.table.DuelistContainers.First(x => x.turnType == data.TurnType).hand;
            var card = data.Card;

            var afterMoveAction = new Action(() =>
            {
                hand.Animator.SetBool(Take, true);
                StartCoroutine(DelayRoutine(0.3f, () =>
                {
                    MoveToStack(card, data.TurnType, () =>
                    {
                        hand.Animator.SetBool(Take, false);
                        MoveHand(hand, null, hand.StartPosition);
                    });
                }));
            });

            MoveHand(hand, afterMoveAction, Vector3.zero, card.transform);
        }


        private IEnumerator DelayRoutine(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }

        private void MoveHand(HandComponent hand, Action action, Vector3 position, Transform parent = null)
        {
            if (parent != null)
            {
                hand.transform.SetParent(parent);
                hand.transform.DOLocalMove(position, handMoveDuration).OnComplete(() => action?.Invoke());
            }
            else hand.transform.DOMove(position, handMoveDuration).OnComplete(() => action?.Invoke());
        }

        private void MoveToStack(CardComponent card, TurnType turnType, Action onComplete)
        {
            var cardType = card.Config.CardType;
            var myStack = game.table.DuelistContainers.First(x => x.turnType == TurnType.My).cardsStack;
            var opponentStackStack = game.table.DuelistContainers.First(x => x.turnType == TurnType.Opponent).cardsStack;
            
            var action = new Action(() =>
            {
                onComplete?.Invoke();
                Supyrb.Signals.Get<CalculateSignal>().Dispatch(turnType, card);
            });

            switch (cardType)
            {
                case CardType.Add:
                    if (turnType == TurnType.My) myStack.PushToStackWithJump(card.transform, action);
                    else if (turnType == TurnType.Opponent) opponentStackStack.PushToStackWithJump(card.transform, action);
                    break;

                case CardType.Multiply:
                    if (turnType == TurnType.My) myStack.PushToStackWithJump(card.transform, action);
                    else if (turnType == TurnType.Opponent) opponentStackStack.PushToStackWithJump(card.transform, action);
                    break;

                case CardType.Divide:
                    if (turnType == TurnType.My) opponentStackStack.PushToStackWithJump(card.transform, action);
                    else if (turnType == TurnType.Opponent) myStack.PushToStackWithJump(card.transform, action);
                    break;

                case CardType.AddPercentage:
                    if (turnType == TurnType.My) myStack.PushToStackWithJump(card.transform, action);
                    else if (turnType == TurnType.Opponent) opponentStackStack.PushToStackWithJump(card.transform, action);
                    break;

                case CardType.SubtractPercentage:
                    if (turnType == TurnType.My) opponentStackStack.PushToStackWithJump(card.transform, action);
                    else if (turnType == TurnType.Opponent) myStack.PushToStackWithJump(card.transform, action);
                    break;

                case CardType.StealPercentage:
                    if (turnType == TurnType.My) opponentStackStack.PushToStackWithJump(card.transform, action);
                    else if (turnType == TurnType.Opponent) myStack.PushToStackWithJump(card.transform, action);
                    break;

                case CardType.AddMove:
                    if (turnType == TurnType.My) myStack.PushToStackWithJump(card.transform, action);
                    else if (turnType == TurnType.Opponent) opponentStackStack.PushToStackWithJump(card.transform, action);
                    break;
            }
        }
    }
}