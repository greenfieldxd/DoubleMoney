using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Source.Scripts.Extensions;
using Source.Scripts.Signals;
using UnityEngine;
using Random = System.Random;

namespace Source.Scripts.Systems.Game
{
    public class CreateDeckSystem : GameSystem
    {
        [SerializeField] private CardComponent cardPrefab;
        [SerializeField] private float yCardOffset = 0.01f;

        private float _yCardPos;
        private int _deckSize;

        public override void OnInit()
        {
            Supyrb.Signals.Get<GetCardsFromDeckSignal>().AddListener(MoveCards);
            
            var boardPositionCount = game.table.GetComponentsInChildren<BoardPointComponent>().Length;
            _deckSize = game.currentDuelConfig.RoundCount * boardPositionCount;
            StartCoroutine(CreateDeck());
        }

        private IEnumerator CreateDeck()
        {
            yield return new WaitForSeconds(1f);

            var typeIndex = 0;
            var types = (CardType[]) Enum.GetValues(typeof(CardType));
            var rnd = new Random();

            
            var configs = game.cardConfigs.Where(x => player.winsCount >= x.UnlockAfterWinsCount).ToArray();
            var rng = new Random();
            rng.Shuffle(configs);

            while (game.cardsInDeck.Count < _deckSize)
            {
                CardType type = types[typeIndex];
                var configsShuffle = configs.Where(x => type == x.CardType).ToArray();

                if (configsShuffle.Length == 0)
                {
                    typeIndex++;
                    if (typeIndex >= types.Length) typeIndex = 0;
                    continue;
                }
                
                rnd.Shuffle(configsShuffle);
                var card = Instantiate(cardPrefab, game.table.DeckPosition.position + new Vector3(-6.5f, 0, 0), Quaternion.Euler(0, 0, 0));
                card.Init(configsShuffle.First(), game.cardBackList[player.cardBackData.cardBackIndex].Sprite);
                AnimationExtension.JumpAnim(card.transform, game.table.DeckPosition, new Vector3(0, _yCardPos, 0), 1f, new Vector3(0,0, 180));
                game.cardsInDeck.Push(card);

                typeIndex++;
                if (typeIndex >= types.Length) typeIndex = 0;
                _yCardPos += yCardOffset;


                yield return new WaitForSeconds(0.12f);
            }

            yield return new WaitForSeconds(0.7f);
            MoveCards();
        }

        private void MoveCards()
        {
            StartCoroutine(MoveCardsOnBoard());
        }

        private IEnumerator MoveCardsOnBoard()
        {
            var boardPoints = game.table.GetComponentsInChildren<BoardPointComponent>();

            foreach (var point in boardPoints)
            {
                if (game.cardsInDeck.Count > 0)
                {
                    var card = game.cardsInDeck.Pop();
                    game.cardsOnBoard.Add(card);
                    point.SetCardSlot(card);
                    card.SetAvailable(point.DependenceCount <= 0);
                    AnimationExtension.JumpAnim(card.transform, point.transform, Vector3.zero, 1f, Vector3.zero);
                }
                else yield break;

                yield return new WaitForSeconds(0.05f);
            }

            foreach (var point in game.board.BoardPointList)
            {
                if (!point.CardSlot) continue;

                point.CardSlot.SetAvailable(point.IsCardSlotAvailable(), true);
            }
        }
    }
}