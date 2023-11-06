using System.Linq;
using Kuhpik;
using Source.Scripts.Extensions;
using UnityEngine;

public class GameRecordSystem : GameSystemWithScreen<MenuUIScreen>
{
    LeaderboardData leaderboardData;
    public override void OnInit()
    {
        YandexSDK.GetLB(YandexData());
        screen.RecordList.Last().Value.text = OtherExtensions.FormatNumberWithCommas(player.RecordMoney) + "$";
    }
    public void YandexLeaderboardData(string data)
    {
        leaderboardData = JsonUtility.FromJson<LeaderboardData>(data);

        if (leaderboardData.entries.Length > 0)
        {
            for (int i = 0; i < leaderboardData.entries.Length; i++)
            {
                if (i >= screen.RecordList.Count - 1) break;

                screen.RecordList[i].Value.text = OtherExtensions.FormatNumberWithCommas(leaderboardData.entries[i].score) + "$";

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
            BoardName = "leader",
            Value = player.RecordMoney
        };

        return JsonUtility.ToJson(data);
    }
}