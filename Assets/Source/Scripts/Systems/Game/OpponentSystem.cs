using System.Collections;
using System.Linq;
using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Components;
using Source.Scripts.Enums;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class OpponentSystem : GameSystem
    {
        [SerializeField] [BoxGroup("Opponent Delay")] private float minWaitDelay;
        [SerializeField] [BoxGroup("Opponent Delay")]private float maxWaitDelay;
        [SerializeField] [BoxGroup("Type priority")] private CardType[] hotPriorityTypes;
        [SerializeField] [BoxGroup("Type priority")] private CardType[] mediumPriorityTypes;
        [SerializeField] [BoxGroup("Type priority")] private CardType[] lowPriorityTypes;
        
        public override void OnInit()
        {
            game.OnTurnChanged += OpponentTurnNow;
        }

        private void OpponentTurnNow()
        {
            if (game.CurrentTurn == TurnType.Opponent)  StartCoroutine(SearchRoutine());
        }

        private IEnumerator SearchRoutine()
        {
            CardComponent card = null;

            yield return new WaitForSeconds(Random.Range(minWaitDelay, maxWaitDelay));

            while (card == null)
            {
                card = GetCardOnBoard();
                yield return new WaitForEndOfFrame();
            }
            
            game.cardsOnBoard.Remove(card);
            BoardPointComponent point = game.board.BoardPointList.First(x => x.CardSlot == card);
            point.SetCardSlot(null);
            card.SetAvailable(false);
                        
            Supyrb.Signals.Get<CardTakeSignal>().Dispatch();
            Supyrb.Signals.Get<CalculateSignal>().Dispatch(game.CurrentTurn, card);
        }

        private CardComponent GetCardOnBoard()
        {
            var availableCards = game.cardsOnBoard.Where(x => x.IsAvailable).ToArray();
            
            var hotCard = SearchCard(availableCards, hotPriorityTypes);
            if (hotCard) return hotCard; 
            
            var mediumCard = SearchCard(availableCards, mediumPriorityTypes);
            if (mediumCard) return mediumCard;
            
            var lowCard = SearchCard(availableCards, lowPriorityTypes);
            if (lowCard) return lowCard;
            
            return null;
        }

        private CardComponent SearchCard(CardComponent[] cards, CardType[] types)
        {
            var randomType = types.GetRandom();

            var card = cards.FirstOrDefault(x => x.Config.CardType == randomType);
            return card != null ? card : null;
        }
    }
}