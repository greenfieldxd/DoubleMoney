using System;
using DG.Tweening;
using Kuhpik;
using Source.Scripts.Enums;
using Source.Scripts.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class GameUIScreen : UIScreen
    {
        [field:SerializeField] public TextMeshProUGUI MoneyText { get; private set; }
        [field:SerializeField] public GameObject ButtonsHolder { get; private set; }
        [field:SerializeField] public Button SelectRandom { get; private set; }
        [field:SerializeField] public RockPaperScissorsButton[] RockPaperScissorsButtons { get; private set; }
        
        
        public void UpdateMoney(string value)
        {
            MoneyText.text = value;
            
            //if (DOTween.IsTweening(MoneyText.transform)) return;
            //MoneyText.transform.DOPunchScale(new Vector3(0.1f,0.1f,0.1f), 0.1f, 1);
        }

        [Serializable]
        public class RockPaperScissorsButton
        {
            public RockPaperScissorsType type;
            public Button button;
        }
    }
}