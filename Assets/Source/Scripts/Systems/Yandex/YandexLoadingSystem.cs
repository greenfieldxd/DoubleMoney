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
            PlayerData loadingData = JsonUtility.FromJson<PlayerData>(data);

            //player.LanguageIndex = loadingData.LanguageIndex;
            //player.IsSoundOff = loadingData.IsSoundOff;
            //player.ResponseAmount = loadingData.ResponseAmount;
            //player.ScoreAmount = loadingData.ScoreAmount;
        }

        UpdateState();
    }

    public void UpdateState()
    {
        screen.LoadingPanel.SetActive(false);

        Bootstrap.Instance.ChangeGameState(GameStateID.Loading);
    }
    string YandexData()
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