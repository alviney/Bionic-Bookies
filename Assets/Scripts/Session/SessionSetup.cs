using UnityEngine;
using UnityEngine.Events;

public class SessionSetup : MonoBehaviour
{
    public UnityEvent OnCreateSession;
    public NumberInput numberOfHumans;
    public NumberInput numberOfAI;
    public NumberInput numberOfRounds;
    public GameObject numberOfHumansPanel;
    public GameObject lobbyPanel;

    private void OnEnable()
    {
        numberOfHumans.SetValue(1);
        numberOfAI.SetValue(2);
        numberOfRounds.SetValue(5);
    }

    public void CreateSession()
    {
        SessionManager.instance.CreateSession(numberOfHumans.value, numberOfAI.value, numberOfRounds.value);
        OnCreateSession.Invoke();
    }

    public void StartSession()
    {
        // Store.session.Start();
    }
}
