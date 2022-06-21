using System;
using System.Collections.Generic;

public static class GamblerNames
{
    public static List<string> full = new List<string> { "Alex the great", "The Kind Prince", "Big Rock", "Sly Cybil", "Slippery Dave", "Harry the Hungry" };

    public static string NewName
    {
        get
        {
            if (full.Count <= 0)
            {
                return "Gambler " + UnityEngine.Random.Range(0, 10000);
            }

            int index = UnityEngine.Random.Range(0, full.Count - 1);
            string name = full[index];
            full.RemoveAt(index);
            return name;
        }
    }
}

public enum GamblerStatus { Ready, NotReady }

public class Gambler
{
    public Action<GamblerStatus> OnStatusChanged;
    public Action OnStatsChanged;
    public string name { get; private set; }
    public int cash { get; private set; }
    public int debt { get; private set; }
    public GamblerStatus status { get; private set; }
    public bool playerControlled = false;

    public Gambler(int cash = 100)
    {
        this.name = GamblerNames.NewName;
        this.cash = cash;
        this.debt = 0;
        this.status = GamblerStatus.NotReady;
    }

    public void UpdateCash(int change)
    {
        cash += change;
        OnStatsChanged?.Invoke();
    }

    public void UpdateDebt(int change)
    {
        debt += change;
        OnStatsChanged?.Invoke();
    }

    public void UpdateStatus(GamblerStatus newStatus)
    {
        if (newStatus != status)
        {
            status = newStatus;
            OnStatusChanged?.Invoke(status);
        }
    }

    public string cashString
    {
        get => "$" + cash.ToString();
    }
}
