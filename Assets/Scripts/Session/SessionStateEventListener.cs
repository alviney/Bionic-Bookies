using UnityEngine;
using UnityEngine.Events;

public class SessionStateEventListener : MonoBehaviour
{
    public UnityEvent OnLobbyEntered;
    public UnityEvent OnBettingEntered;
    public UnityEvent OnRaceEntered;
    public UnityEvent OnRaceResultsEntered;
    public UnityEvent OnAccusationsEntered;

    private void OnEnable()
    {
        SessionManager.instance.OnStateChanged += HandleStateChange;
    }

    private void OnDisable()
    {
        SessionManager.instance.OnStateChanged -= HandleStateChange;
    }

    private void HandleStateChange(SessionState state)
    {
        switch (state)
        {
            case SessionState.Lobby:
                OnLobbyEntered.Invoke();
                break;
            case SessionState.Betting:
                OnBettingEntered.Invoke();
                break;
            case SessionState.Race:
                OnRaceEntered.Invoke();
                break;
            case SessionState.RaceResults:
                OnRaceResultsEntered.Invoke();
                break;
            case SessionState.Accusations:
                OnAccusationsEntered.Invoke();
                break;
        }
    }
}
