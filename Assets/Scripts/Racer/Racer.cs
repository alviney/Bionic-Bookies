using UnityEngine;
using System.Collections.Generic;

public static class RacerNames
{
    public static List<string> full = new List<string> { "Fasty McFast Face", "Simple Steve", "What a Guy", "Lightning", "Sloth", "Martha", "Fred", "Noozer", "Quick Sticks", "Ricky Roller" };

    public static string GetNewName()
    {
        if (full.Count <= 0)
        {
            return "Racer " + Random.Range(0, 10000);
        }


        int index = Random.Range(0, full.Count - 1);
        string name = full[index];
        full.RemoveAt(index);
        return name;
    }
}

public enum RacerStat { Speed, Acceleration }

[System.Serializable]
public class Racer
{
    public System.Action OnRace;
    public string name;
    public Stat speed;
    public Stat acceleration;
    public int hair;
    public int hairColor;
    public int bodyColor;

    public Racer()
    {
        this.name = RacerNames.GetNewName();
        this.speed = new Stat(Random.Range(1f, 1.4f));
        this.acceleration = new Stat(Random.Range(0.3f, 0.6f));
        this.hair = Random.Range(0, 3);
        this.hairColor = Random.Range(0, 5);
        this.bodyColor = Random.Range(0, 5);
    }

    public void AddTamper(RacerModifier tamper)
    {
        switch (tamper.stat)
        {
            case RacerStat.Speed:
                speed.AddModifier(tamper.value);
                break;
            case RacerStat.Acceleration:
                acceleration.AddModifier(tamper.value);
                break;
        }
    }

    public void ClearModifiers()
    {
        speed.ClearModifiers();
        acceleration.ClearModifiers();
    }

    public void Race()
    {
        this.OnRace?.Invoke();
    }
}
