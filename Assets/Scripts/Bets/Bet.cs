[System.Serializable]
public class Bet
{
    public string gamblerName;
    public Racer racer;
    public int odds;
    public int value;
    public bool paidOut;
    private bool locked = false;

    public Bet(string gamblerName, Racer racer, int value, int odds)
    {
        this.gamblerName = gamblerName;
        this.racer = racer;
        this.value = value;
        this.odds = odds;
    }

    public void Lock()
    {
        if (!locked)
        {
            Store.GetGambler(gamblerName).UpdateCash(-this.value);
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
