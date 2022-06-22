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

[System.Serializable]
public class Gambler
{
    public Action<GamblerStatus> OnStatusChanged;
    public Action OnStatsChanged;
    public string name;
    public int cash;
    public int debt;
    public GamblerStatus status;
    public bool human = false;
    public bool online = true;

    public Gambler(string name, bool human = false, bool online = false, int cash = 100)
    {
        this.name = name;
        this.cash = cash;
        this.debt = 0;
        this.status = GamblerStatus.NotReady;

        this.human = human;
        this.online = online;
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
