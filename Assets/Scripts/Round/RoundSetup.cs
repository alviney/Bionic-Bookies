using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;


public class RoundSetup : MonoBehaviour
{
    public TextMeshProUGUI roundTitle;
    // Racers
    public GameObject racerBettingCardPrefab;
    public Transform racerBettingCardParent;

    // Gamblers
    public GameObject gamblerBettingCardPrefab;
    public Transform gamblerBettingCardParent;

    public BetInput cardBetInput;
    public UnityEvent OnAllGamblersReady;

    private void OnEnable()
    {
        roundTitle.text = "Race " + Store.race.raceNumber.ToString();

        PopulateBettingCards();
    }

    private void PopulateBettingCards()
    {
        foreach (Racer racer in Store.racers)
        {
            GameObject instance = Instantiate(racerBettingCardPrefab, racerBettingCardParent);
            instance.GetComponent<RacerBettingCard>().OnClickHandler += HandleRacerCardClick;
        }

        foreach (Gambler gambler in Store.gamblersByStanding)
        {
            Instantiate(gamblerBettingCardPrefab, gamblerBettingCardParent);
            if (!gambler.human)
            {
                DOVirtual.DelayedCall(Random.Range(0.5f, 1.5f), () =>
                {
                    gambler.UpdateStatus(GamblerStatus.Ready);
                });
            }
        }
    }

    public void OnReady()
    {
        Store.activeGambler.UpdateStatus(GamblerStatus.Ready);

        foreach (Gambler gambler in Store.gamblers)
        {
            Debug.Log(gambler.name + " " + gambler.status);
        }
        if (Store.allGamblersReady)
        {
            DOVirtual.DelayedCall(1, () =>
            {
                SessionManager.instance.NextState();
                foreach (Transform child in racerBettingCardParent) Destroy(child.gameObject, 1f);
                foreach (Transform child in gamblerBettingCardParent) Destroy(child.gameObject, 1f);
            });
        }
        else
        {
            // Store.gamblersController.NextGambler();
        }
    }

    private void HandleRacerCardClick(Racer racer, Transform transform)
    {
        cardBetInput.gameObject.SetActive(true);
        cardBetInput.Set(racer);
        cardBetInput.transform.position = transform.position + Vector3.down * 230;
    }
}
