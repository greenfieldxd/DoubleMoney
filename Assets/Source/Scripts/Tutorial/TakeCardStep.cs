using Kuhpik;
using Source.Scripts.Data;
using Source.Scripts.Enums;
using Source.Scripts.Signals;
using Source.Scripts.Systems;
using Source.Scripts.UI;

namespace Source.Scripts.Tutorial
{
    public class TakeCardStep : TutorialStep
    {
        protected override void OnBegin()
        {
            UIManager.GetUIScreen<GameUIScreen>().TutorTakeCard.SetActive(true);
            Supyrb.Signals.Get<HandMoveSignal>().AddListener(CheckComplete);
        }

        protected override void OnComplete()
        {
            UIManager.GetUIScreen<GameUIScreen>().TutorTakeCard.SetActive(false);
            Supyrb.Signals.Get<HandMoveSignal>().RemoveListener(CheckComplete);
        }

        private void CheckComplete(HandData handData)
        {
            if (handData.TurnType == TurnType.My) Complete();
        }
    }
}