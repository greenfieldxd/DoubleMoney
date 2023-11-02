using Kuhpik;
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
            //PlayerData loadingData = JsonUtility.FromJson<PlayerData>(data);

            //player.isFirstLaunch = loadingData.isFirstLaunch;
            //player.languageIndex = loadingData.languageIndex;
            //player.isSoundOff = loadingData.isSoundOff;
            //player.cardBackIndex = loadingData.cardBackIndex;
            //player.cardBackList = loadingData.cardBackList;
            //player.winsCount = loadingData.winsCount;
            //player.planeMatIndex = loadingData.planeMatIndex;
            //
            //player.Money = loadingData.Money;
            //player.RecordMoney = loadingData.RecordMoney;
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