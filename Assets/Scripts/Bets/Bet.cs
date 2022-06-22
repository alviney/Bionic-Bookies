[System.Serializable]
public class Bet
{
    public Gambler gambler;
    public Racer racer;
    public int odds;
    public int value;
    public bool paidOut;
    private bool locked = false;

    public Bet(Gambler gambler, Racer racer, int value, int odds)
    {
        this.gambler = gambler;
        this.racer = racer;
        this.value = value;
        this.odds = odds;
    }

    public void Lock()
    {
        if (!locked)
        {
            gambler.UpdateCash(-this.value);
            locked = true;
        }
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
