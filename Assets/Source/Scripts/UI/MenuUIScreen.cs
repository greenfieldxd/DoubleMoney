using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIScreen : UIScreen
{
    [SerializeField, BoxGroup("Component")] List<RecordComponent> recordList;

    [SerializeField, BoxGroup("Button")] Button playButton;
    [SerializeField, BoxGroup("Button")] Button languageButton;

    [SerializeField, BoxGroup("Image")] Image languageImage;

    [SerializeField, BoxGroup("Sprite")] List<Sprite> languageSpriteList;

    public List<RecordComponent> RecordList => recordList;
    public Button PlayButton => playButton;
    public Button LanguageButton => languageButton;
    public Image LanguageImage => languageImage;
    public List<Sprite> LanguageSpriteList => languageSpriteList;
}