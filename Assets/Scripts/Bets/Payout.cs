[System.Serializable]
public class Payout
{
    public Gambler gambler;
    public int value;

    public Payout(Gambler gambler, int value)
    {
        this.gambler = gambler;
        this.value = value;
    }
}
