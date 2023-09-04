using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Kuhpik;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class GameUIScreen : UIScreen
    {
        [field:SerializeField] public TextMeshProUGUI MoneyText { get; private set; }
        public void UpdateMoney()
        {
            //MoneyText.text = Bootstrap.Instance.PlayerData.Money.ToString();
            
            if (DOTween.IsTweening(MoneyText.transform)) return;
            MoneyText.transform.DOPunchScale(new Vector3(0.1f,0.1f,0.1f), 0.1f, 1);
        }
    }
}