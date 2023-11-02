using Kuhpik;
using Source.Scripts.Data;
using UnityEngine;
public class YandexLoadingSystem : GameSystemWithScreen<YandexUIScreen>
{
    public override void OnInit()
    {
        screen.LoadingPanel.SetActive(true);

#if UNITY_EDITOR
        UpdateState();
#else
        YandexSDK.LoadData(YandexData());
#endif
    }
    public void YandexLoadingData(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            PlayerData loadingData = JsonUtility.FromJson<PlayerData>(data);
            CardBackData cardsLoadingData = JsonUtility.FromJson<CardBackData>(data);

            player.isFirstLaunch = loadingData.isFirstLaunch;
            player.languageIndex = loadingData.languageIndex;
            player.isSoundOff = loadingData.isSoundOff;
            player.winsCount = loadingData.winsCount;
            player.planeMatIndex = loadingData.planeMatIndex;
            player.Money = loadingData.Money;
            player.RecordMoney = loadingData.RecordMoney;
            
            player.cardBackData = cardsLoadingData;
        }

        UpdateState();
    }

    public void UpdateState()
    {
        screen.LoadingPanel.SetActive(false);

        Bootstrap.Instance.ChangeGameState(GameStateID.Loading);
    }
    private string YandexData()
    {
        YandexData data = new YandexData()
        {
            ObjectName = "Yandex Loading",
            MethodEndName = "YandexLoadingData",
            MethodErrorName = "UpdateState"
        };

        return JsonUtility.ToJson(data);
    }
}