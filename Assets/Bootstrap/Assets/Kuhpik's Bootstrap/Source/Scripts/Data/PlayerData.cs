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
        public bool isFirstLaunch;
        public bool isSoundOff;
        public int languageIndex;
        public int winsCount;
        public int planeMatIndex;
        public int cardBackIndex;
        public List<int> cardBackList;

        [SerializeField] private int money;
        [SerializeField] private int recordMoney;
        
        public int Money
        {
            get => money;
            set => money = Mathf.Clamp(value,0,999999999);
        }
        
        public int RecordMoney
        {
            get => recordMoney;
            set => recordMoney = Mathf.Clamp(value,0,999999999);
        }
    }
}