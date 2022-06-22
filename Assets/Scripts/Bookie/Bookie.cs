// using System.Collections.Generic;
// using UnityEngine;

public class Bookie
{
    // public Dictionary<Race, List<Bet>> books;

    // public Bookie()
    // {
    //     this.books = new Dictionary<Race, List<Bet>>();
    // }

    // public void PlaceBet(Race race, Bet bet)
    // {
    //     if (this.books.ContainsKey(race))
    //     {
    //         this.books[race].Add(bet);
    //     }
    //     else
    //     {
    //         this.books.Add(race, new List<Bet>() { bet });
    //     }

    //     bet.gambler.UpdateCash(-bet.value);
    // }

    // public int GetOdds(Race race, Racer racer)
    // {
    //     return Random.Range(2, 8);
    // }

    // public List<Payout> GetPayouts(Race race)
    // {
    //     List<Payout> payouts = new List<Payout>();

    //     List<Bet> bets;
    //     if (books.TryGetValue(race, out bets))
    //     {
    //         foreach (Bet bet in bets)
    //         {
    //             if (bet.racer == race.racersFinished[0])
    //             {
    //                 int payout = bet.Payout;
    //                 bet.gambler.UpdateCash(payout);
    //                 payouts.Add(new Payout(bet.gambler, payout));
    //             }
    //         }
    //     }

    //     return payouts;
    // }
}
