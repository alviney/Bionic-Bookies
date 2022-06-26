using UnityEngine;
using UnityEngine.Events;

public class SessionSetup : MonoBehaviour
{
    public UnityEvent OnCreateLobby;
    public NumberInput numberOfHumans;
    public NumberInput numberOfAI;
    public NumberInput numberOfRounds;
    public GameObject numberOfHumansPanel;
    public GameObject lobbyPanel;
    public bool online = false;

    private void OnEnable()
    {
        numberOfHumans.SetValue(1);
        numberOfAI.SetValue(2);
        numberOfRounds.SetValue(5);

        lobbyPanel.SetActive(online);
        numberOfHumansPanel.SetActive(!online);
    }

    public void SetOnline()
    {
        online = true;
        OnCreateLobby.Invoke();
    }

    public void SetOffline()
    {
        online = false;
    }

    public void CreateSession()
    {
        SessionManager.instance.CreateSession(numberOfHumans.value, numberOfAI.value, numberOfRounds.value, online);
        SessionManager.instance.NextState();
    }

    public void StartSession()
    {
        // Store.session.Start();
    }
}
