using System;
using Source.Scripts.Components;
using Source.Scripts.Enums;

namespace Source.Scripts.Data
{
    [Serializable]
    public class HandData
    {
        public HandMoveType HandMoveType { get; private set; }
        public TurnType TurnType { get; private set; }
        public CardComponent Card { get; private set; }
        
        public HandData(CardComponent card, HandMoveType moveType, TurnType turnType)
        {
            Card = card;
            HandMoveType = moveType;
            TurnType = turnType;
        }
    }
}