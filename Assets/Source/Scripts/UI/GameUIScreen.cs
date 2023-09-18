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
        [field:SerializeField] public GameObject TurnObject { get; private set; }
        [field:SerializeField] public TextMeshProUGUI TurnText { get; private set; }
        [field:SerializeField] public Image TurnImage { get; private set; }
        [field:SerializeField] public Color ColorRed { get; private set; }
        [field:SerializeField] public Color ColorGreen { get; private set; }
        [field:SerializeField] public GameObject ButtonsHolder { get; private set; }
        [field:SerializeField] public Button SelectRandom { get; private set; }
        [field:SerializeField] public RockPaperScissorsButton[] RockPaperScissorsButtons { get; private set; }


        public void UpdateTurnHolder(TurnType type)
        {
            switch (type)
            {
                case TurnType.My:
                    TurnImage.color = ColorGreen;
                    TurnText.text = "your move";
                    break;
                
                case TurnType.Opponent:
                    TurnImage.color = ColorRed;
                    TurnText.text = "opponent move";
                    break;
                
            }
        }
        
        [Serializable]
        public class RockPaperScissorsButton
        {
            public RockPaperScissorsType type;
            public Button button;
        }
    }
}