using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class RaceSetup : MonoBehaviour
{
    public GameObject raceWorldPrefab;
    public UnityEvent OnRaceSetup;
    public UnityEvent OnRaceFinished;

    private GameObject raceWorld;

    private void OnEnable()
    {
        OnRaceSetup.Invoke();
        SpawnRaceWorld();
        GameManager.race.OnFinished += RaceFinished;
        GameManager.StartRace();
    }

    private void OnDisable()
    {
        GameManager.race.OnFinished -= RaceFinished;
    }

    private void SpawnRaceWorld()
    {
        if (raceWorld != null)
        {
            DestroyRaceWorld();
        }
        raceWorld = Instantiate(raceWorldPrefab, Vector3.zero, Quaternion.identity);
    }

    public void DestroyRaceWorld()
    {
        Destroy(raceWorld);
    }

    private void RaceFinished(List<Racer> racers)
    {
        this.OnRaceFinished.Invoke();
    }
}
