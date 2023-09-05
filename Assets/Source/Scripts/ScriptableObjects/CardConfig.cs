using System;
using System.Linq;
using NaughtyAttributes;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CardConfig", menuName = "Config/CardConfig")]
    public class CardConfig : ScriptableObject
    {
        [field:SerializeField] public CardType CardType { get; private set; }
        [field:SerializeField] public Color CardColor { get; private set; }
        [field:SerializeField] public float Value { get; private set; }
        [field:SerializeField] public string StringBeforeValue { get; private set; }
        [field:SerializeField] public string StringAfterValue { get; private set; }
        [field:SerializeField] public int UnlockAfterWinsCount { get; private set; }

        public string GetCardText()
        {
            return $"{StringBeforeValue}{Value}{StringAfterValue}";     
        }
    }
}