using System;
using Source.Scripts.Components;
using Source.Scripts.Enums;

namespace Source.Scripts.Data
{
    [Serializable]
    public class HandData
    {
        public TurnType TurnType { get; private set; }
        public CardComponent Card { get; private set; }
        
        public HandData(CardComponent card, TurnType turnType)
        {
            Card = card;
            TurnType = turnType;
        }
    }
}