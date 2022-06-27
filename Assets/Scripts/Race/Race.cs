using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Race
{
    public Action<List<Racer>> OnFinished;
    public int raceNumber;
    public List<Racer> racersFinished;

    public Race(int raceNumber)
    {
        this.racersFinished = new List<Racer>();


        this.raceNumber = raceNumber;
    }

    public void Start()
    {
        foreach (Racer racer in Store.racers)
        {
            racer.Race();
        }
    }

    public void AddRacerToFinished(Racer racer)
    {
        racersFinished.Add(racer);
        if (racersFinished.Count == Store.racers.Count)
        {
            Finish();
        }
    }

    public void Finish()
    {
        this.OnFinished?.Invoke(this.racersFinished);
    }
}
