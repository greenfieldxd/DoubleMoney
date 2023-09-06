using System;
using Source.Scripts.Components;
using Source.Scripts.Enums;

namespace Source.Scripts
{
    public class Actions
    {
        public event Action<TurnType, CardComponent> OnCalculate;

        public void Calculate(TurnType turnType, CardComponent card)
        {
            OnCalculate?.Invoke(turnType, card);
        }
    }
}