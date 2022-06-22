using UnityEngine;

public enum LobbyDataKey { Session }
public class LobbyData : MonoBehaviour
{

    public LobbyData(Session session)
    {

    }

    public LobbyDataKey Key(Session session)
    {
        return LobbyDataKey.Session;
    }

    public string Data(Session session)
    {
        return JsonUtility.ToJson(session);
    }
}
