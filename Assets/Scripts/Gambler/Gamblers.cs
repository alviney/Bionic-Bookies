using System;
using System.Collections.Generic;
using System.Linq;

public class Gamblers
{
    public Action<Gambler> OnActiveGamblerChanged;

    public Gambler[] all { get; private set; }
    public Gambler activeGambler;

    public Gamblers(int numberOfHumans = 1, int numberOfAI = 1)
    {
        int totalGamblers = numberOfHumans + numberOfAI;
        all = new Gambler[totalGamblers];
        for (int i = 0; i < totalGamblers; i++)
        {
            all[i] = new Gambler();
            if (i < numberOfHumans)
            {
                all[i].playerControlled = true;
            }
        }
        activeGambler = all[0];
    }

    public void ResetStatuses()
    {
        foreach (Gambler gambler in all)
        {
            gambler.UpdateStatus(GamblerStatus.NotReady);
            activeGambler = all[0];
        }
    }

    public bool AllGamblersReady
    {
        get
        {
            bool allReady = true;
            foreach (Gambler gambler in this.all)
            {
                if (gambler.status == GamblerStatus.NotReady)
                {
                    allReady = false;
                    break;
                }
            }

            return allReady;
        }
    }

    public void NextGambler()
    {
        foreach (Gambler gambler in this.all)
        {
            if (gambler.playerControlled && gambler.status == GamblerStatus.NotReady)
            {
                this.activeGambler = gambler;
                this.OnActiveGamblerChanged?.Invoke(gambler);
            }
        }
    }

    public Gambler[] allByStanding
    {
        get
        {
            return all.OrderByDescending(g => g.cash).ToArray();
        }
    }
}
