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
        foreach (Racer racer in GameManager.race.racersFinished)
        {
            results[i].text = racer.name;
            i++;
        }
    }

    public void OnNext()
    {
        if (GameManager.session.isFinished)
        {
            this.OnSessionFinished.Invoke();
        }
        else
        {
            this.OnNextRace.Invoke();
        }
    }
}
