using Kuhpik;
using Source.Scripts.Extensions;

public class LanguageSelectSystem : GameSystemWithScreen<MenuUIScreen>
{
    public override void OnInit()
    {
        screen.LanguageButton.onClick.AddListener(OnLanguageChange);

        SelectLanguage();
    }
    void OnLanguageChange()
    {
        player.LanguageIndex++;

        if (player.LanguageIndex > screen.LanguageSpriteList.Count - 1)
            player.LanguageIndex = 0;

        UpdateLanguage();
        game.AudioSystem.CreateSound(0);

        OtherExtensions.SaveGame(player);
        OtherExtensions.TransformPunchScale(screen.LanguageButton.transform);
    }
    void SelectLanguage()
    {
        if (player.LanguageIndex == -1)
        {
            string language = YandexSDK.GetLanguage();

            switch (language)
            {
                default:
                    player.LanguageIndex = 0;

                    break;

                case "ru":
                    player.LanguageIndex = 1;

                    break;
            }
        }

        UpdateLanguage();
    }
    void UpdateLanguage()
    {
        Translator.SelectLanguage(player.LanguageIndex);

        screen.LanguageImage.sprite = screen.LanguageSpriteList[player.LanguageIndex];
    }
}