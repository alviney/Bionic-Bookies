[System.Serializable]
public class Payout
{
    public string gamblerName;
    public int value;

    public Payout(string gamblerName, int value)
    {
        this.gamblerName = gamblerName;
        this.value = value;
    }
}
