using System;

[Serializable]
public class LeaderboardData
{
    public string leaderboard;
    public Range[] ranges;
    public int userRank;
    public Entry[] entries;
}

[Serializable]
public class Range
{
    public int start;
    public int size;
}

[Serializable]
public class Entry
{
    public int score;
    public string extraData;
    public int rank;
    public Player player;
    public string formattedScore;
}

[Serializable]
public class Player
{
    public string getAvatarSrc;
    public string getAvatarSrcSet;
    public string lang;
    public string publicName;
    public ScopePermissions scopePermissions;
    public string uniqueID;
}

[Serializable]
public class ScopePermissions
{
    public string avatar;
    public string public_name;
}