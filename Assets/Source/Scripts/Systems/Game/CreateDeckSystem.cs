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

        private IEnumerator CreateDeck()
        {
            var count = 0;
            
            foreach (CardType type in Enum.GetValues(typeof(CardType)))
            {
                if (count >= deckSize) yield break;

                var cardConfigs = game.cardConfigs.Where(x => x.CardType == type && player.winsCount >= x.UnlockAfterWinsCount).ToArray();
                var rng = new Random();
                rng.Shuffle(cardConfigs);
                var randomConfig = cardConfigs.FirstOrDefault();

                if (randomConfig != null)
                {
                    var card = Instantiate(cardPrefab, game.table.DeckPosition.position + new Vector3(-1f, 0, 0), Quaternion.Euler(0,0, 180));
                    card.Init(randomConfig);
                    AnimationExtension.JumpAnim(card.transform, game.table.DeckPosition,  new Vector3(0, _yCardPos, 0), 1f, Vector3.zero);
                
                    count++;
                    _yCardPos += yCardOffset;
                }
                
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}