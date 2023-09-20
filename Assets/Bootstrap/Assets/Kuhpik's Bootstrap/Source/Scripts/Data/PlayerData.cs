using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;

namespace Kuhpik
{
    /// <summary>
    /// Used to store player's data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        public bool IsFirstLaunch;
        public bool IsSoundOff;
        public int LanguageIndex;
        public int CardBackIndex;
        public List<int> CardBackList;

        public int winsCount;
        
        [SerializeField] private int money;
        [SerializeField] private int recordMoney;
        public event Action OnMoneyChanged;
        public int Money
        {
            get => money;
            set
            {
                var delta = Mathf.Abs(money - value);
                money = Mathf.Clamp(value,0,999999999);
                
                if (delta>0) OnMoneyChanged?.Invoke();
            }
        }
        
        public int RecordMoney
        {
            get => recordMoney;
            set => recordMoney = Mathf.Clamp(value,0,999999999);
        }
    }
}