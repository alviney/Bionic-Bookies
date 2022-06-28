using System;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

public enum SessionState { Lobby, Betting, Race, RaceResults, Accusations }
public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;
    public Action<SessionState> OnStateChanged;
    public Action<Gambler> OnActiveGamblerChanged;
    public Transform modalContainer;
    public SessionState state;
    public bool online = false;
    private bool isHost = true;

    private void Awake()
    {
        instance = this;
        SteamMatchmaking.OnLobbyDataChanged += HandleLobbyDataUpdate;
    }

    private void OnDestroy()
    {
        SteamMatchmaking.OnLobbyDataChanged -= HandleLobbyDataUpdate;
    }

    public void CreateSession(int numberOfHumans, int numberOfAI, int numberOfRounds, bool online)
    {
        List<Gambler> gamblers = new List<Gambler>();
        if (online)
        {
            numberOfHumans = 0;
            foreach (Friend friend in SteamworksLobbyManager.currentLobby.Members)
            {
                gamblers.Add(new Gambler(friend.Name, true, true));
            }
        }

        if (isHost)
        {
            for (int i = 0; i < numberOfHumans; i++)
            {
                gamblers.Add(new Gambler(GamblerNames.NewName, true, false));
            }

            for (int i = 0; i < numberOfAI; i++)
            {
                gamblers.Add(new Gambler(GamblerNames.NewName));
            }
        }

        Store.session = new Session(gamblers, numberOfRounds);
        this.online = online;
    }

    public void NextState()
    {
        if (state == SessionState.Lobby) { state = SessionState.Betting; }
        else if (state == SessionState.Betting) state = SessionState.Race;
        else if (state == SessionState.Race) state = SessionState.RaceResults;
        else if (state == SessionState.RaceResults) state = SessionState.Accusations;
        else if (state == SessionState.Accusations) state = SessionState.Betting;

        OnStateChange(state);

        PostLobbyDataUpdate();

        if (!online || isHost)
        {
            this.OnStateChanged?.Invoke(state);
        }
    }

    private void OnStateChange(SessionState state)
    {
        switch (state)
        {
            case SessionState.Betting:
                // Reset gambler statuses
                Gamblers.SetGamblerStatuses(Store.gamblers, GamblerStatus.NotReady);
                // Set active gambler
                if (online)
                {
                    SetActiveGambler(Gamblers.GetGamblerByName(Store.gamblers, SteamClient.Name));
                }
                else
                {
                    SetActiveGambler(Store.gamblers[0]);
                }

                if (isHost)
                {
                    // Clear previous race bets
                    Store.session.bets.Clear();
                    // Create race
                    int raceIndex = (Store.session.race?.raceNumber ?? 0) + 1;
                    Store.session.race = new Race(raceIndex);
                }
                break;

            case SessionState.RaceResults:
                if (isHost)
                {
                    Store.racers.ForEach(r => r.ClearModifiers());
                    Store.session.payouts.Clear();
                    foreach (Bet bet in Store.session.bets)
                    {
                        if (bet.racer.name == Store.race.racersFinished[0].name)
                        {
                            int payout = bet.Payout;
                            Store.GetGambler(bet.gambler.name).UpdateCash(payout);
                            Store.session.payouts.Add(new Payout(bet.gambler, payout));
                        }
                    }
                }
                break;
        }
    }

    public void PostLobbyDataUpdate()
    {
        if (isHost && online)
        {
            if (SteamworksLobbyManager.currentLobby.IsOwnedBy(SteamClient.SteamId))
            {
                SteamworksLobbyManager.currentLobby.SetData(Store.session.JsonKey, Store.session.ToJson);
            }
        }
    }

    public void HandleLobbyDataUpdate(Lobby lobby)
    {
        if (online && !isHost)
        {
            string data = lobby.GetData(Store.session.JsonKey);
            if (data != "")
            {
                Store.session = Session.CreateFromJSON(data);
            }

            Debug.Log("Transition to state : " + state);
            this.OnStateChanged?.Invoke(state);
        }
    }

    public void PlaceBet(Bet bet)
    {
        bet.Lock();
        if (online)
        {
            // postBet
            Store.session.bets.Add(bet);
        }
        else
        {
            Store.session.bets.Add(bet);
        }
    }

    public void SetActiveGambler(Gambler gambler)
    {
        Store.activeGamblerName = gambler.name;
        OnActiveGamblerChanged?.Invoke(gambler);
    }
}
