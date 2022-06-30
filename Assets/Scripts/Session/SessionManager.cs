using System;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

public class SessionManager : MonoBehaviour
{
    public static SessionManager instance;
    public Action<SessionState> OnStateChanged;
    public Action<Gambler> OnActiveGamblerChanged;
    public Transform modalContainer;

    private void Awake()
    {
        instance = this;
        SteamMatchmaking.OnLobbyDataChanged += HandleLobbyDataUpdate;
    }

    private void OnDestroy()
    {
        SteamMatchmaking.OnLobbyDataChanged -= HandleLobbyDataUpdate;
    }

    public void CreateSession(int numberOfHumans, int numberOfAI, int numberOfRounds)
    {
        if (isOnline)
        {
            Store.session = Session.NewOnline(numberOfHumans, numberOfAI, numberOfRounds);
        }
        else
        {
            Store.session = Session.New(numberOfHumans, numberOfAI, numberOfRounds);
        }
    }

    public void StartSession()
    {
        NextState();
    }

    public SessionState state
    {
        get => Store.session.state;
    }

    public void NextState()
    {
        SetSessionState(Store.session.GetNextState());
    }

    public void SetSessionState(SessionState newState)
    {
        Store.session.state = newState;
        OnStateChange(state);

        PostLobbyDataUpdate();

        Debug.Log("Transition to state : " + state);
        this.OnStateChanged?.Invoke(state);
    }

    private void OnStateChange(SessionState state)
    {
        switch (state)
        {
            case SessionState.Betting:
                // Reset gambler statuses
                Gamblers.SetGamblerStatuses(Store.gamblers, GamblerStatus.NotReady);
                // Set active gambler
                if (isOnline)
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

    private bool isOnline
    {
        get => SteamworksLobbyManager.currentLobby.Id != 0;
    }

    private bool isHost
    {
        get => SteamworksLobbyManager.currentLobby.IsOwnedBy(SteamClient.SteamId);
    }

    public void PostLobbyDataUpdate()
    {
        if (isHost)
        {
            if (SteamworksLobbyManager.currentLobby.IsOwnedBy(SteamClient.SteamId))
            {
                Debug.Log("Post Lobby Data Update ");
                SteamworksLobbyManager.currentLobby.SetData(Store.session.JsonKey, Store.session.ToJson);
            }
        }
    }

    public void HandleLobbyDataUpdate(Lobby lobby)
    {
        if (!isHost)
        {
            SessionState previousState = Store.session.state;


            string data = lobby.GetData(Store.session.JsonKey);
            if (data != "")
            {
                Store.session = Session.CreateFromJSON(data);
            }

            if (Store.session.state != previousState)
            {
                SetSessionState(Store.session.state);
            }
            Debug.Log("Handle lobby data update - 2 messages ");
            Debug.Log("1/2 - Old state: " + previousState);
            Debug.Log("2/2 - New state: " + Store.session.state);
        }
    }

    public void PlaceBet(Bet bet)
    {
        bet.Lock();
        if (isOnline)
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
