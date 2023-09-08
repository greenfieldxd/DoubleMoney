using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Kuhpik;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Source.Scripts.Extensions;
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
            foreach (var point in game.Board.BoardPointList)
            {
                CardComponent card = Instantiate(cardPrefab, point.transform);
                point.SetCardSlot(card);

                card.SetAvailable(point.DependenceCount <= 0);
            }
            //var boardPositionCount = game.table.GetComponentsInChildren<BoardPositionComponent>().Length;
            //_deckSize = game.roundsCount * boardPositionCount;
            //StartCoroutine(CreateDeck());
        }

        private IEnumerator CreateDeck()
        {
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
                var card = Instantiate(cardPrefab, game.table.DeckPosition.position + new Vector3(-2.5f, 0, 0), Quaternion.Euler(0, 0, 0));
                card.Init(configsShuffle.First());
                AnimationExtension.JumpAnim(card.transform, game.table.DeckPosition, new Vector3(0, _yCardPos, 0), 1f, new Vector3(0,0, 180));
                game.cardsInDeck.Push(card);

                typeIndex++;
                if (typeIndex >= types.Length) typeIndex = 0;
                _yCardPos += yCardOffset;


                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.7f);

            StartCoroutine(MoveCardsOnBoard());
        }

        private IEnumerator MoveCardsOnBoard()
        {
            var boardPositions = game.table.GetComponentsInChildren<BoardPositionComponent>();

            foreach (var boardPositionComponent in boardPositions)
            {
                if (game.cardsInDeck.Count > 0)
                {
                    var card = game.cardsInDeck.Pop();
                    game.cardsOnBoard.Add(card);
                    AnimationExtension.JumpAnim(card.transform, boardPositionComponent.transform, Vector3.zero, 1f, Vector3.zero);
                }
                else yield break;

                yield return new WaitForSeconds(0.05f);
            }

            game.blockClicks = false;
        }
    }
}