using UnityEngine;

public class BetResults : MonoBehaviour
{
    public GameObject betEntryPrefab;

    private void OnDisable()
    {
        foreach (Transform child in this.transform) Destroy(child.gameObject);
    }

    public void Present(Racer racer)
    {
        foreach (Transform child in transform) Destroy(child.gameObject);

        foreach (Bet bet in Store.session.bets)
        {
            if (bet.racerName == racer.name)
            {
                GameObject instance = Instantiate(betEntryPrefab, this.transform);
                Payout payout = Store.session.payouts.Find(p => p.gamblerName == bet.gamblerName);
                string value;
                if (payout != null)
                {
                    value = "$" + payout.value.ToString();
                }
                else
                {
                    value = "-$" + bet.value.ToString();
                }
                instance.GetComponent<BetEntry>().Set(bet.gamblerName, value);
            }
        }
    }
}
