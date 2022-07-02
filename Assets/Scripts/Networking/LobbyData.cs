using UnityEngine;

public enum LobbyDataKey { Session, GamblerSubmission, GamblerStatus }
public class LobbyData
{
    public static string Key(Session session)
    {
        return LobbyDataKey.Session.ToString();
    }

    public static string Key(GamblerSubmission submission)
    {
        return LobbyDataKey.GamblerSubmission.ToString();
    }

    public static string Key(GamblerStatus status)
    {
        return LobbyDataKey.GamblerStatus.ToString();
    }
    // public LobbyData(Session session)
    // {

    // }

    // public LobbyDataKey Key(Session session)
    // {
    //     return LobbyDataKey.Session;
    // }

    // public LobbyDataKey Key(GamblerSubmission submission)
    // {
    //     return LobbyDataKey.GamblerSubmission;
    // }
}
