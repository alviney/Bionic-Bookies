using System.Linq;
using System.Collections.Generic;

public class Gamblers
{
    public static List<Gambler> CreateGamblers(string[] names, bool human = false, bool online = false)
    {
        List<Gambler> gamblers = new List<Gambler>();
        foreach (string name in names)
        {
            gamblers.Add(new Gambler(name, human, online));
        }

        return gamblers;
    }

    public static bool AreAllGamblersReady(List<Gambler> gamblers)
    {
        return gamblers.All(gambler => gambler.status == GamblerStatus.Ready);
    }

    public static void SetGamblerStatuses(List<Gambler> gamblers, GamblerStatus status)
    {
        foreach (Gambler gambler in gamblers)
        {
            gambler.status = status;
        }
    }

    public static List<Gambler> OrderByStanding(List<Gambler> gamblers)
    {
        return gamblers.OrderByDescending(g => g.cash).ToList();
    }

    public static Gambler GetGamblerByName(List<Gambler> gamblers, string name)
    {
        return gamblers.Find(gambler => gambler.name == name);
    }
}
