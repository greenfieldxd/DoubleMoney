using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class CardBackSystem : GameSystemWithScreen<MenuUIScreen>
{
    [SerializeField, BoxGroup("Prefab")] CardBackComponent cardBackPrefab;

    [SerializeField, BoxGroup("Debug"), ReadOnly] int cardBackIndex;
    [SerializeField, BoxGroup("Debug"), ReadOnly] List<CardBackComponent> cardBackList = new List<CardBackComponent>();
    public override void OnInit()
    {
        screen.CardButton.onClick.AddListener(OpenPanel);
        screen.CardHomeButton.onClick.AddListener(ClosePanel);
        screen.CardSkipButton.onClick.AddListener(CloseInfoPanel);
        screen.CardBuyButton.onClick.AddListener(() => OnCardBuy(screen.CardBuyButton.transform));
        screen.CardAdButton.onClick.AddListener(() => YandexSDK.ShowRewarded(YandexData()));

        CreateCardBack();
        screen.SetPanelActive(0);
    }
    public void YandexRewardedStart()
    {
        CloseInfoPanel();
    }
    public void YandexRewardedEnd()
    {
        player.CardBackList[cardBackIndex] = 2;
        Extensions.SaveGame(player);

        UpdateCardBack();
    }
    void CreateCardBack()
    {
        for (int i = 0; i < game.CardBackList.Count; i++)
        {
            CardBackComponent cardBack = Instantiate(cardBackPrefab, screen.CardContent);
            CardBackConfig config = game.CardBackList[i];

            cardBack.IconImage.sprite = config.Sprite;
            cardBack.PriceText.text = "$" + OtherExtensions.FormatNumberWithCommas(config.Price);
            cardBack.CardButton.name = i.ToString();

            cardBack.CardButton.onClick.AddListener(() => OnCardSelect(cardBack.CardButton.transform));
            cardBack.BuyButton.onClick.AddListener(() => OnCardBuyOpen(cardBack.CardButton.transform));
            cardBack.AdButton.onClick.AddListener(() => OnCardAdOpen(cardBack.CardButton.transform));

            cardBackList.Add(cardBack);
        }
    }
    void OnCardBuyOpen(Transform button)
    {
        cardBackIndex = int.Parse(button.name);
        int price = game.CardBackList[cardBackIndex].Price;

        if (price <= player.Money)
        {
            screen.PanelList[2].SetActive(true);
            screen.PanelList[3].SetActive(true);
        }

        Extensions.TransformPunchScale(button);
        game.AudioSystem.CreateSound(0);
    }
    void OnCardAdOpen(Transform button)
    {
        cardBackIndex = int.Parse(button.name);

        screen.PanelList[2].SetActive(true);
        screen.PanelList[4].SetActive(true);

        Extensions.TransformPunchScale(button);
        game.AudioSystem.CreateSound(0);
    }
    void OnCardSelect(Transform button)
    {
        int index = int.Parse(button.name);

        if (player.CardBackList[index] != 2) return;

        player.CardBackIndex = index;
        Extensions.SaveGame(player);

        UpdateCardBack();

        Extensions.TransformPunchScale(button);
        game.AudioSystem.CreateSound(0);
    }
    void OnCardBuy(Transform button)
    {
        int price = game.CardBackList[cardBackIndex].Price;
        player.Money -= price;

        player.CardBackList[cardBackIndex] = 1;
        Extensions.SaveGame(player);

        UpdateCardBack();
        CloseInfoPanel();

        Extensions.TransformPunchScale(button);
        game.AudioSystem.CreateSound(0);
    }
    void UpdateCardBack()
    {
        screen.MoneyText.text = "$" + OtherExtensions.FormatNumberWithCommas(player.Money);

        foreach (var image in screen.CardBackImageList)
        {
            image.sprite = game.CardBackList[player.CardBackIndex].Sprite;
        }

        for (int i = 0; i < cardBackList.Count; i++)
        {
            CardBackComponent cardBack = cardBackList[i];
            int status = player.CardBackList[i];
            int statusPrevious = player.CardBackList[Mathf.Clamp(i - 1, 0, cardBackList.Count)];

            cardBack.ActivePanel.SetActive(player.CardBackIndex == i);
            cardBack.ClosePanel.SetActive(statusPrevious <= 0);

            cardBack.BuyButton.gameObject.SetActive(status == 0);
            cardBack.AdButton.gameObject.SetActive(status == 1);
        }
    }
    void OpenPanel()
    {
        UpdateCardBack();

        game.AudioSystem.CreateSound(0);
        YandexSDK.ShowInterstitial();
        screen.SetPanelActive(1);
    }
    void ClosePanel()
    {
        game.AudioSystem.CreateSound(0);
        YandexSDK.ShowInterstitial();
        screen.SetPanelActive(0);
    }
    void CloseInfoPanel()
    {
        YandexSDK.ShowInterstitial();
        screen.SetPanelActive(1);
    }
    string YandexData()
    {
        YandexData data = new YandexData()
        {
            ObjectName = "Card Back",
            MethodStartName = "YandexRewardedStart",
            MethodEndName = "YandexRewardedEnd",
        };

        return JsonUtility.ToJson(data);
    }
}