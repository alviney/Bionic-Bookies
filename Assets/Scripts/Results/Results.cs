using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Results : MonoBehaviour
{
    public UnityEvent<Racer> OnRacerChanged;
    public UnityEvent OnNextRace;

    public GameObject reportModalPrefab;
    public List<ResultsEntry> results;
    private List<Racer> racers;
    private Racer activeRacer;

    private void OnEnable()
    {
        racers = Store.activeRace.racersFinished;
        int i = 0;
        foreach (Racer racer in racers)
        {
            results[i].SetName(racer.name);
            i++;
        }

        SelectRacer(0);
    }

    public void SelectRacer(int index)
    {
        results.ForEach(r => r.SetSelected(false));
        if (racers.Count > index)
        {
            results[index].SetSelected(true);
            this.activeRacer = racers[index];
            this.OnRacerChanged.Invoke(this.activeRacer);
        }
    }

    public void OnOpenReport()
    {
        GameObject instance = Instantiate(reportModalPrefab, SessionManager.instance.modalContainer);
        instance.GetComponent<AccusationModal>().Present(this.activeRacer);
    }

    public void OnNext()
    {
        SessionManager.instance.NextState();
    }
}
