public class Session
{
    public int numberOfHumans;
    public int numberOfAI;
    public int numberOfRounds;
    public int index;

    public Session(int numberOfHumans, int numberOfAI, int numberOfRounds)
    {
        this.numberOfHumans = numberOfHumans;
        this.numberOfAI = numberOfAI;
        this.numberOfRounds = numberOfRounds;
        index = 0;
    }

    public void Next()
    {
        this.index++;
    }

    public bool isFinished
    {
        get => index >= numberOfRounds;
    }
}
