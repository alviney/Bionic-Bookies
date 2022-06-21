public class Bet
{
    public Gambler gambler { get; private set; }
    public Racer racer { get; private set; }
    public int odds { get; private set; }
    public int value { get; private set; }
    public bool paidOut { get; private set; }

    public Bet(Gambler gambler, Racer racer, int value, int odds)
    {
        this.gambler = gambler;
        this.racer = racer;
        this.value = value;
        this.odds = odds;
    }

    public int Payout
    {
        get
        {
            if (paidOut)
            {
                return 0;
            }

            paidOut = true;
            return value * this.odds;
        }
    }
}
