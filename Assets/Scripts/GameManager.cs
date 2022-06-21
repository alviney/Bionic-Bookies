using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Racers racers;
    public static Gamblers gamblers;
    public static Race race;
    public static Session session;
    public static Bookie bookie;

    private void Awake()
    {
        UIScreen.activeScreen = null;
    }

    public static void StartSession(Session session)
    {
        GameManager.session = session;
        GameManager.racers = new Racers();
        GameManager.gamblers = new Gamblers(session.numberOfHumans, session.numberOfAI);
        GameManager.bookie = new Bookie();
    }

    public static void NewRace()
    {
        GameManager.race = new Race(racers.all);
        GameManager.session.Next();
        GameManager.gamblers.ResetStatuses();
    }

    public static void StartRace()
    {
        GameManager.race.Start();
    }
}
