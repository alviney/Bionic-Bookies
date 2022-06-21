using UnityEngine;

public class BetInput : MonoBehaviour
{
    public NumberInput numberInput;
    private Racer racer;

    public void PlaceBet()
    {
        int odds = GameManager.bookie.GetOdds(GameManager.race, this.racer);
        Bet bet = new Bet(GameManager.gamblers.activeGambler, racer, numberInput.value, odds);
        GameManager.bookie.PlaceBet(GameManager.race, bet);
        this.gameObject.SetActive(false);
    }

    public void Set(Racer racer)
    {
        this.racer = racer;
        if (GameManager.gamblers.activeGambler != null)
        {
            int min = Mathf.Min(numberInput.min, GameManager.gamblers.activeGambler.cash);
            int max = GameManager.gamblers.activeGambler.cash;
            numberInput.SetRange(min, max);
        }
    }
}
