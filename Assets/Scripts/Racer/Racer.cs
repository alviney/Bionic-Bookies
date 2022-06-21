using UnityEngine;
using System.Collections.Generic;

public static class RacerNames
{
    public static List<string> full = new List<string> { "Fasty McFast Face", "Simple Steve", "What a Guy", "Lightning", "Sloth", "Bolt", "Fred" };

    public static string NewName
    {
        get
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
}

public class Racer
{
    public System.Action OnRace;
    public string name;
    public float speed = 1f;

    public Racer()
    {
        this.name = RacerNames.NewName;
        this.speed = Random.Range(8f, 12f);
    }

    public void Race()
    {
        this.OnRace?.Invoke();
    }
}
