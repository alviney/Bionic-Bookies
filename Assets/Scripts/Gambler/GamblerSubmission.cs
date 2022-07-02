using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GamblerSubmission
{
    public Gambler owner;
    public List<Bet> bets;
    public List<RacerModifier> modifiers;

    public GamblerSubmission(Gambler owner, List<Bet> bets, List<RacerModifier> modifiers)
    {
        this.owner = owner;
        this.bets = bets;
        this.modifiers = modifiers;
    }

    public string ToJson
    {
        get => JsonUtility.ToJson(this, true);
    }

    public static string JsonKey
    {
        get => LobbyDataKey.GamblerSubmission.ToString();
    }

    public static GamblerSubmission CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<GamblerSubmission>(json);
    }
}
