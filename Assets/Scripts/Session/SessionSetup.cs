using UnityEngine;
using UnityEngine.Events;

public class SessionSetup : MonoBehaviour
{
    public UnityEvent OnSessionCreated;
    public NumberInput numberOfHumans;
    public NumberInput numberOfAI;
    public NumberInput numberOfRounds;

    private void OnEnable()
    {
        numberOfHumans.SetValue(1);
        numberOfAI.SetValue(2);
        numberOfRounds.SetValue(5);
    }

    public void CreateSession()
    {
        Session session = new Session(numberOfHumans.value, numberOfAI.value, numberOfRounds.value);
        GameManager.StartSession(session);
        OnSessionCreated.Invoke();
    }
}
