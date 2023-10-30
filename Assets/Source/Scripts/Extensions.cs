using DG.Tweening;
using Kuhpik;
using UnityEngine;

public class Extensions
{
    public static void TransformPunchScale(Transform target, float punch = 0.1f, float duration = 1f, int vibrato = 4,
       float elasticity = 1f, System.Action callback = null)
    {
        target.DORewind();
        target.DOPunchScale(new Vector3(punch, punch, punch), duration, vibrato, elasticity).OnComplete(() => {
            callback?.Invoke();
        });
    }
    public static void SaveGame(PlayerData data)
    {
        Bootstrap.Instance.SaveGame();

        var @string = JsonUtility.ToJson(data);
        YandexSDK.SaveData(@string);
    }
    public static string YandexAdData()
    {
        YandexData data = new YandexData()
        {
            ObjectName = "Audio Loading",
            MethodStartName = "YandexInterstitialStart",
            MethodEndName = "YandexInterstitialEnd",
        };

        return JsonUtility.ToJson(data);
    }
}