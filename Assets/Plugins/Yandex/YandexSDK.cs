using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSDK
{
    [DllImport("__Internal")]
    private static extern void ShowAd(string data);

    [DllImport("__Internal")]
    private static extern void LoadExtern(string data);

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern string GetLang();

    [DllImport("__Internal")]
    private static extern string GetTLD();

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void SetLeaderboard(string data);

    [DllImport("__Internal")]
    private static extern void GetLeaderboard(string data);

    [DllImport("__Internal")]
    private static extern void ShowReward(string data);

    public static void ShowInterstitial(string data)
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: ShowInterstitial");
#else
        ShowAd(data);
#endif
    }
    public static void SaveData(string data)
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: SaveData");
#else
        SaveExtern(data);
#endif
    }
    public static void LoadData(string data)
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: LoadData");
#else
        LoadExtern(data);
#endif
    }
    public static string GetLanguage()
    {
        Debug.Log("YandexSDK: GetLanguage");

#if UNITY_EDITOR
        return "null";
#else
        return GetLang();
#endif
    }
    public static string GetDomen()
    {
        Debug.Log("YandexSDK: GetDomen");

#if UNITY_EDITOR
        return "null";
#else
        return GetTLD();
#endif
    }
    public static void ShowRateGame()
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: ShowRateGame");
#else
        RateGame();
#endif
    }
    public static void SetLB(string data)
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: SetLeaderboard");
#else
        SetLeaderboard(data);
#endif
    }

    public static void GetLB(string data)
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: GetLeaderboard");
#else
        GetLeaderboard(data);
#endif
    }

    public static void ShowRewarded(string data)
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: ShowRewarded");
#else
        ShowReward(data);
#endif
    }
}