using System;
using System.Collections.Generic;

public class Race
{
    public Action<List<Racer>> OnFinished;
    public Racer[] racers { get; private set; }
    public List<Racer> racersFinished { get; private set; }

    public Race(Racer[] racers)
    {
        this.racersFinished = new List<Racer>();

        this.racers = racers;
    }

    public void Start()
    {
        foreach (Racer racer in racers)
        {
            racer.Race();
        }
    }

    public void AddRacerToFinished(Racer racer)
    {
        racersFinished.Add(racer);
        if (racersFinished.Count == racers.Length)
        {
            Finish();
        }
    }

    public void Finish()
    {
        this.OnFinished?.Invoke(this.racersFinished);
    }
}
