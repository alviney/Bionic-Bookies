using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Results : MonoBehaviour
{
    public TextMeshProUGUI[] results;
    public UnityEvent OnNextRace;
    public UnityEvent OnSessionFinished;

    private void OnEnable()
    {
        int i = 0;
        foreach (Racer racer in Store.activeRace.racersFinished)
        {
            results[i].text = racer.name;
            i++;
        }
    }

    public void OnNext()
    {
        if (Store.session.isFinished)
        {
            this.OnSessionFinished.Invoke();
        }
        else
        {
            SessionManager.instance.NextState();
        }
    }
}
