using System.Collections.Generic;

public class Store
{
    public static Session session;

    public static Race activeRace { get => session.race; }
    public static List<Racer> racers { get => session.racers; }
    public static Racer GetRacer(string name) { return racers.Find(r => r.name == name); }

    public static Race race { get => session.race; }
    public static List<Gambler> gamblers { get => session.gamblers; }
    public static List<Gambler> gamblersByStanding { get => Gamblers.OrderByStanding(session.gamblers); }
    public static string activeGamblerName;
    public static Gambler activeGambler { get => Gamblers.GetGamblerByName(gamblers, activeGamblerName); }
    public static Gambler GetGambler(string name) { return Gamblers.GetGamblerByName(gamblers, name); }
    public static bool allGamblersReady { get => Gamblers.AreAllGamblersReady(gamblers); }
}
