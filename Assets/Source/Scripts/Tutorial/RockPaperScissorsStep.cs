using System.Collections;
using Kuhpik;
using Source.Scripts.Enums;
using Source.Scripts.Signals;
using Source.Scripts.Systems;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Tutorial
{
    public class RockPaperScissorsStep : TutorialStep
    {
        protected override void OnBegin()
        {
            UIManager.GetUIScreen<GameUIScreen>().TutorHand.SetActive(true);
            Supyrb.Signals.Get<MiniGameSignal>().AddListener(CheckComplete);
        }

        protected override void OnComplete()
        {
            UIManager.GetUIScreen<GameUIScreen>().TutorHand.SetActive(false);
            Supyrb.Signals.Get<MiniGameSignal>().RemoveListener(CheckComplete);
        }

        private void CheckComplete(bool haveWinner)
        {
            if (haveWinner) Complete();
        }
    }
}