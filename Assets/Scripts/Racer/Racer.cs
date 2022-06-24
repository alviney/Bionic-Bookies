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

[System.Serializable]
public class Racer
{
    public System.Action OnRace;
    public string name;
    public float speed = 1f;
    public float acceleration = 1f;
    public int hair;
    public int hairColor;
    public int bodyColor;

    public Racer()
    {
        this.name = RacerNames.NewName;
        this.speed = Random.Range(1f, 1.4f);
        this.acceleration = Random.Range(0.3f, 0.6f);
        this.hair = Random.Range(0, 3);
        this.hairColor = Random.Range(0, 8);
        this.bodyColor = this.hairColor;
    }

    public void Race()
    {
        this.OnRace?.Invoke();
    }
}
