using Kuhpik;
using Source.Scripts.Extensions;
using UnityEngine;

public class GameRecordSystem : GameSystemWithScreen<MenuUIScreen>
{
    LeaderboardData leaderboardData;
    public override void OnInit()
    {
        YandexSDK.GetLB(0);
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
}