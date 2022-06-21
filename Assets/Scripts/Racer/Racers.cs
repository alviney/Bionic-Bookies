public class Racers
{
    public Racer[] all { get; private set; }

    public Racers()
    {
        all = CreateRacers();
    }

    private Racer[] CreateRacers()
    {
        return new Racer[] { new Racer(), new Racer(), new Racer(), new Racer(), new Racer() };
    }
}
