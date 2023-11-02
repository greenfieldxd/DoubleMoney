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
        player.languageIndex++;

        if (player.languageIndex > screen.LanguageSpriteList.Count - 1)
            player.languageIndex = 0;

        UpdateLanguage();
        game.audioSystem.CreateSound(0);

        OtherExtensions.SaveGame(player);
        OtherExtensions.TransformPunchScale(screen.LanguageButton.transform);
    }
    void SelectLanguage()
    {
        if (player.languageIndex == -1)
        {
            string language = YandexSDK.GetLanguage();

            switch (language)
            {
                default:
                    player.languageIndex = 0;

                    break;

                case "ru":
                    player.languageIndex = 1;

                    break;
            }
        }

        UpdateLanguage();
    }
    void UpdateLanguage()
    {
        Translator.SelectLanguage(player.languageIndex);

        screen.LanguageImage.sprite = screen.LanguageSpriteList[player.languageIndex];
    }
}