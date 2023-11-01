using Kuhpik;
using Source.Scripts.Systems;

namespace Source.Scripts.Tutorial
{
    public class MenuHandStep : TutorialStep
    {
        protected override void OnBegin()
        {
            UIManager.GetUIScreen<MenuUIScreen>().TutorHand.SetActive(true);
            UIManager.GetUIScreen<MenuUIScreen>().PlayButton.onClick.AddListener(Complete);
        }

        protected override void OnComplete()
        {
            UIManager.GetUIScreen<MenuUIScreen>().TutorHand.SetActive(false);
            UIManager.GetUIScreen<MenuUIScreen>().PlayButton.onClick.RemoveListener(Complete);
        }
    }
}