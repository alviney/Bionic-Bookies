using UnityEngine;
using System.Collections.Generic;

public class Session
{
    public int numberOfRounds;
    public List<Racer> racers;
    public List<Gambler> gamblers;
    public Race race;
    public List<Bet> bets;
    public List<Payout> payouts;

    public Session(List<Gambler> gamblers, int numberOfRounds)
    {
        this.numberOfRounds = numberOfRounds;

        this.racers = Racers.CreateRacers();
        this.gamblers = gamblers;
        this.bets = new List<Bet>();
        this.payouts = new List<Payout>();
    }

    public int GetOdds(Race race, Racer racer)
    {
        return Random.Range(2, 8);
    }

    public bool isFinished
    {
        get => race.raceNumber >= numberOfRounds;
    }

    public string ToJson
    {
        get => JsonUtility.ToJson(this, true);
    }

    public string JsonKey
    {
        get => LobbyDataKey.Session.ToString();
    }

    public static Session CreateFromJSON(string json)
    {
        return JsonUtility.FromJson<Session>(json);
    }
}
