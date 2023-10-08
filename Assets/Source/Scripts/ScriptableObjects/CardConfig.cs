using System;
using System.Linq;
using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CardConfig", menuName = "Config/CardConfig")]
    public class CardConfig : ScriptableObject
    {
        [SerializeField] private float value;
        [SerializeField] private float valueModifierMin;
        [SerializeField] private float valueModifierMax;
        [SerializeField] private float maxValue;
        [SerializeField] private bool needRoundUp;
        
        [field:SerializeField] public CardType CardType { get; private set; }
        [field:SerializeField] public Color CardColor { get; private set; }
        [field:SerializeField] public string StringBeforeValue { get; private set; }
        [field:SerializeField] public string StringAfterValue { get; private set; }
        [field:SerializeField] public int UnlockAfterWinsCount { get; private set; }
        
        public int CreateValue(int winsCount)
        {
            var modifiedValue = Random.Range(valueModifierMin, valueModifierMax) * (winsCount < 2 ? 2 : Random.Range(1, winsCount));
            var clamped = Mathf.Clamp(value + modifiedValue, 0, maxValue);
            return needRoundUp ? RoundUp((int)clamped) : (int)clamped;
        }
        
      
        public string GetCardText(int val)
        {
            return $"{StringBeforeValue}{val}{StringAfterValue}";     
        }
        
        private int RoundUp(int val)
        {
            int tmp = 0;
            int number = val;
            if ((tmp = number % 5) != 0) number += number > -1 ? 5 - tmp : -tmp;
            
            return number;
        }
    }
}