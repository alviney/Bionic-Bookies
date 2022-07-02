using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Steamworks;

public enum SessionState { Lobby, Betting, Race, RaceResults, Accusations }
public class Session
{
    public int numberOfRounds;
    public List<Racer> racers;
    public List<Gambler> gamblers;
    public Race race;
    public List<Payout> payouts;
    public List<GamblerSubmission> submissions;
    public SessionState state;


    public Session(List<Gambler> gamblers, int numberOfRounds)
    {
        this.numberOfRounds = numberOfRounds;

        this.racers = Racers.CreateRacers();
        this.gamblers = gamblers;
        this.submissions = new List<GamblerSubmission>();
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

    public SessionState GetNextState()
    {
        SessionState state = this.state;

        if (state == SessionState.Lobby) { state = SessionState.Betting; }
        else if (state == SessionState.Betting) state = SessionState.Race;
        else if (state == SessionState.Race) state = SessionState.RaceResults;
        else if (state == SessionState.RaceResults) state = SessionState.Accusations;
        else if (state == SessionState.Accusations) state = SessionState.Betting;

        return state;
    }

    public void AddSubmission(GamblerSubmission submission)
    {
        if (!submissions.Exists(s => s.gamblerName == submission.gamblerName))
        {
            Debug.Log("Gambler submission " + submission);
            submissions.Add(submission);
        }
        else
        {
            Debug.Log($"Submission for {submission.gamblerName} already added");
        }
    }

    public List<Bet> bets
    {
        get
        {
            List<Bet> bets = new List<Bet>();
            foreach (GamblerSubmission item in submissions)
            {
                foreach (Bet bet in item.bets)
                {
                    bets.Add(bet);
                }
            }
            return bets;
        }
    }

    public static Session NewOnline(int numberOfHumans, int numberOfAI, int numberOfRounds)
    {
        List<Gambler> gamblers = new List<Gambler>();

        numberOfHumans = 0;
        foreach (Friend friend in SteamworksLobbyManager.currentLobby.Members)
        {
            gamblers.Add(new Gambler(friend.Name, true, true));
        }


        for (int i = 0; i < numberOfHumans; i++)
        {
            gamblers.Add(new Gambler(GamblerNames.NewName, true, false));
        }

        for (int i = 0; i < numberOfAI; i++)
        {
            gamblers.Add(new Gambler(GamblerNames.NewName));
        }

        return new Session(gamblers, numberOfRounds);
    }

    public static Session New(int numberOfHumans, int numberOfAI, int numberOfRounds)
    {
        List<Gambler> gamblers = new List<Gambler>();

        for (int i = 0; i < numberOfHumans; i++)
        {
            gamblers.Add(new Gambler(GamblerNames.NewName, true, false));
        }

        for (int i = 0; i < numberOfAI; i++)
        {
            gamblers.Add(new Gambler(GamblerNames.NewName));
        }

        return new Session(gamblers, numberOfRounds);
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
