using System.Collections.Generic;
using UnityEngine;

public class Winnings : MonoBehaviour
{
    public GameObject winnerPrefab;

    private void OnEnable()
    {
        SpawnPayouts(GameManager.bookie.GetPayouts(GameManager.race));
    }

    private void OnDisable()
    {
        foreach (Transform child in this.transform) Destroy(child.gameObject);
    }

    public void SpawnPayouts(List<Payout> payouts)
    {
        foreach (Payout payout in payouts)
        {
            GameObject instance = Instantiate(winnerPrefab, this.transform);
            instance.GetComponent<Winnner>().Set(payout.gambler.name, "$" + payout.value.ToString());
        }
    }
}
