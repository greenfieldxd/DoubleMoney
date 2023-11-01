using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIScreen : UIScreen
{
    [SerializeField, BoxGroup("GameObject")] List<GameObject> panelList;

    [SerializeField, BoxGroup("Transform")] Transform cardContent;

    [SerializeField, BoxGroup("Component")] List<RecordComponent> recordList;

    [SerializeField, BoxGroup("Button")] Button playButton;
    [SerializeField, BoxGroup("Button")] Button languageButton;
    [SerializeField, BoxGroup("Button")] Button cardButton;
    [SerializeField, BoxGroup("Button")] Button cardHomeButton;
    [SerializeField, BoxGroup("Button")] Button cardSkipButton;
    [SerializeField, BoxGroup("Button")] Button cardBuyButton;
    [SerializeField, BoxGroup("Button")] Button cardAdButton;

    [SerializeField, BoxGroup("Image")] Image languageImage;
    [SerializeField, BoxGroup("Image")] List<Image> cardBackImageList;

    [SerializeField, BoxGroup("Tutor")] GameObject tutorHand;
    
    [SerializeField, BoxGroup("Text")] TMP_Text moneyText;

    [SerializeField, BoxGroup("Sprite")] List<Sprite> languageSpriteList;

    public List<GameObject> PanelList => panelList;
    public Transform CardContent => cardContent;
    public List<RecordComponent> RecordList => recordList;
    public Button PlayButton => playButton;
    public Button LanguageButton => languageButton;
    public Button CardButton => cardButton;
    public Button CardHomeButton => cardHomeButton;
    public Button CardSkipButton => cardSkipButton;
    public Button CardBuyButton => cardBuyButton;
    public Button CardAdButton => cardAdButton;
    public Image LanguageImage => languageImage;
    public List<Image> CardBackImageList => cardBackImageList;
    public TMP_Text MoneyText => moneyText;
    public List<Sprite> LanguageSpriteList => languageSpriteList;
    public GameObject TutorHand => tutorHand;

    public void SetPanelActive(int index)
    {
        for (int i = 0; i < panelList.Count; i++)
        {
            panelList[i].SetActive(i == index);
        }
    }
}