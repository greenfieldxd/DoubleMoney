﻿using System;
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
        public int winsCount;
        
        [SerializeField] private int money;
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
    }
}