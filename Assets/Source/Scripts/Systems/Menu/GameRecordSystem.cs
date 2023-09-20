using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Extensions;
using UnityEngine;

public class GameRecordSystem : GameSystemWithScreen<MenuUIScreen>
{
    [SerializeField, BoxGroup("Settings")] string leaderboardName = "leader";

    LeaderboardData leaderboardData;
    public override void OnInit()
    {
        YandexSDK.GetLB(YandexData());
    }
    public void YandexLeaderboardData(string data)
    {
        leaderboardData = JsonUtility.FromJson<LeaderboardData>(data);

        if (leaderboardData.entries.Length > 0)
        {
            for (int i = 0; i < leaderboardData.entries.Length; i++)
            {
                if (i >= screen.RecordList.Count) break;

                screen.RecordList[i].Value.text = "$" + OtherExtensions.FormatNumberWithCommas(leaderboardData.entries[i].score);

                string name = leaderboardData.entries[i].player.publicName;
                screen.RecordList[i].PlayerName.text = name == "" ? Translator.GetText(3) : name;
            }
        }
    }
    string YandexData()
    {
        YandexData data = new YandexData()
        {
            ObjectName = "Game Record",
            MethodEndName = "YandexLeaderboardData",
            BoardName = leaderboardName
        };

        return JsonUtility.ToJson(data);
    }
}