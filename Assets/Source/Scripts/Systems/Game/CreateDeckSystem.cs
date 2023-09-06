using System;
using System.Collections;
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
        [SerializeField] private int deckSize = 15;
        [SerializeField] private float yCardOffset = 0.01f;

        private float _yCardPos;

        public override void OnInit()
        {
            StartCoroutine(CreateDeck());
        }

        public override void OnUpdate()
        {
        }

        private IEnumerator CreateDeck()
        {
            var count = 0;

            var configs = game.cardConfigs.Where(x => player.winsCount >= x.UnlockAfterWinsCount).ToArray();
            var rng = new Random();
            rng.Shuffle(configs);

            foreach (var cardConfig in configs)
            {
                if (count >= deckSize) yield break;

                var card = Instantiate(cardPrefab, game.table.DeckPosition.position + new Vector3(-2.5f, 0, 0), Quaternion.Euler(0, 0, 0));
                card.Init(cardConfig);
                AnimationExtension.JumpAnim(card.transform, game.table.DeckPosition, new Vector3(0, _yCardPos, 0), 1f, new Vector3(0,0, 180));
                game.cardsInDeck.Push(card);

                count++;
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