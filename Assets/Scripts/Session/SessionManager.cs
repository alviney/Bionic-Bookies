using System;
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
        SteamMatchmaking.OnLobbyMemberDataChanged += HandleLobbyMemberDataUpdate;
    }

    private void OnDestroy()
    {
        SteamMatchmaking.OnLobbyDataChanged -= HandleLobbyDataUpdate;
        SteamMatchmaking.OnLobbyMemberDataChanged -= HandleLobbyMemberDataUpdate;
    }

    public void CreateSession(int numberOfHumans, int numberOfAI, int numberOfRounds)
    {
        if (isOnline) Store.session = Session.NewOnline(numberOfHumans, numberOfAI, numberOfRounds);
        else Store.session = Session.New(numberOfHumans, numberOfAI, numberOfRounds);
    }

    public void StartSession()
    {
        if (isHost)
        {
            SetSessionState(SessionState.Betting);
        }
    }

    public SessionState state
    {
        get => Store.session.state;
    }

    public void SetSessionState(SessionState newState)
    {
        if (isHost && newState == Store.session.state) return;

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
                    Store.session.submissions.Clear();
                    // Create race
                    int raceIndex = (Store.session.race?.raceNumber ?? 0) + 1;
                    Store.session.race = new Race(raceIndex);
                }
                break;

            case SessionState.Race:
                Store.session.modifiers.ForEach(m => m.AddToRacer());
                break;

            case SessionState.RaceResults:
                if (isHost)
                {
                    Store.racers.ForEach(r => r.ClearModifiers());
                    Store.session.payouts.Clear();

                    foreach (Bet bet in Store.session.bets)
                    {
                        if (bet.racerName == Store.race.racersFinished[0].name)
                        {
                            int payout = bet.Payout;
                            Store.GetGambler(bet.gamblerName).UpdateCash(payout);
                            Store.session.payouts.Add(new Payout(bet.gamblerName, payout));
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
                SteamworksLobbyManager.currentLobby.SetData(Session.JsonKey, Store.session.ToJson);
            }
        }
    }

    public void HandleLobbyDataUpdate(Lobby lobby)
    {
        if (!isHost)
        {
            SessionState previousState = Store.session?.state ?? SessionState.Lobby;

            Debug.Log("Handle lobby data update");
            string data = lobby.GetData(Session.JsonKey);
            if (data != "")
            {
                Store.session = Session.CreateFromJSON(data);
            }

            if (Store.session != null && Store.session.state != previousState)
            {
                SetSessionState(Store.session.state);
            }
        }
    }

    public void HandleLobbyMemberDataUpdate(Lobby lobby, Friend friend)
    {
        Debug.Log("Handle lobby member data update for " + friend.Name);
        HandleUpdateStatus(lobby, friend);

        if (isHost)
        {
            if (state == SessionState.Betting)
            {
                HandleSubmission(lobby, friend);
            }

            if (Store.allGamblersReady)
            {
                Debug.Log("All gamblers ready");
                SetSessionState(SessionState.Race);
            }
        }

    }

    private void HandleSubmission(Lobby lobby, Friend friend)
    {
        string data = lobby.GetMemberData(friend, GamblerSubmission.JsonKey);
        if (data != "")
        {
            GamblerSubmission submission = GamblerSubmission.CreateFromJSON(data);
            Store.session.AddSubmission(submission);
        }
    }

    private void HandleUpdateStatus(Lobby lobby, Friend friend)
    {
        string data = lobby.GetMemberData(friend, "status");
        if (data != "")
        {
            GamblerStatus status = (GamblerStatus)System.Enum.Parse(typeof(GamblerStatus), data);
            Debug.Log("Gambler status " + status);
            Store.GetGambler(friend.Name).UpdateStatus(status);
        }
    }

    public void PostLobbyMemberDataUpdate(string key, string value)
    {
        SteamworksLobbyManager.currentLobby.SetMemberData(key, value);
    }

    public void SetActiveGambler(Gambler gambler)
    {
        Store.activeGamblerName = gambler.name;
        OnActiveGamblerChanged?.Invoke(gambler);
    }
}
