using UnityEngine;
using System.Collections.Generic;

public class RaceSetup : MonoBehaviour
{
    public GameObject raceWorldPrefab;

    private GameObject raceWorld;

    private void OnEnable()
    {
        SpawnRaceWorld();
        Store.activeRace.OnFinished += RaceFinished;
        Store.activeRace.Start();
    }

    private void OnDisable()
    {
        Store.activeRace.OnFinished -= RaceFinished;
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
        SessionManager.instance.NextState();
    }
}
