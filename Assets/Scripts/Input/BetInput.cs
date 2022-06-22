using UnityEngine;

public class BetInput : MonoBehaviour
{
    public NumberInput numberInput;
    private Racer racer;

    public void PlaceBet()
    {
        int odds = Store.session.GetOdds(Store.activeRace, this.racer);
        Bet bet = new Bet(Store.activeGambler, racer, numberInput.value, odds);
        SessionManager.instance.PlaceBet(bet);
        this.gameObject.SetActive(false);
    }

    public void Set(Racer racer)
    {
        this.racer = racer;
        if (Store.activeGambler != null)
        {
            int min = Mathf.Min(numberInput.min, Store.activeGambler.cash);
            int max = Store.activeGambler.cash;
            numberInput.SetRange(min, max);
        }
    }
}
