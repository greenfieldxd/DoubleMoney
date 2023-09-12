using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSDK
{
    [DllImport("__Internal")]
    private static extern void ShowAd();

    [DllImport("__Internal")]
    private static extern string GetLang();

    [DllImport("__Internal")]
    private static extern string GetTLD();

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void SetLeaderboard(int index, int value);

    [DllImport("__Internal")]
    private static extern void GetLeaderboard(int index);


    public static void ShowInterstitial()
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: ShowInterstitial");
#else
        ShowAd();
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

    public static void SaveData(string data)
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: SaveData");
#else
        SaveExtern(data);
#endif
    }

    public static void LoadData()
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: LoadData");
#else
        LoadExtern();
#endif
    }

    public static void ShowRateGame()
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: RateGame");
#else
        RateGame();
#endif
    }

    public static void SetLB(int index, int value)
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: SetLeaderboard");
#else
        SetLeaderboard(index, value);
#endif
    }

    public static void GetLB(int index)
    {
#if UNITY_EDITOR
        Debug.Log("YandexSDK: GetLeaderboard");
#else
        GetLeaderboard(index);
#endif
    }
}