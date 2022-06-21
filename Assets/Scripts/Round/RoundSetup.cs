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
        GameManager.NewRace();

        roundTitle.text = "Race " + GameManager.session.index.ToString();

        PopulateBettingCards();
    }

    private void PopulateBettingCards()
    {
        foreach (Racer racer in GameManager.racers.all)
        {
            GameObject instance = Instantiate(racerBettingCardPrefab, racerBettingCardParent);
            instance.GetComponent<RacerBettingCard>().OnClickHandler += HandleRacerCardClick;
        }

        foreach (Gambler gambler in GameManager.gamblers.allByStanding)
        {
            Instantiate(gamblerBettingCardPrefab, gamblerBettingCardParent);
            if (!gambler.playerControlled)
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
        GameManager.gamblers.activeGambler.UpdateStatus(GamblerStatus.Ready);

        if (GameManager.gamblers.AllGamblersReady)
        {
            DOVirtual.DelayedCall(1, () =>
            {
                OnAllGamblersReady.Invoke();
                foreach (Transform child in racerBettingCardParent) Destroy(child.gameObject, 1f);
                foreach (Transform child in gamblerBettingCardParent) Destroy(child.gameObject, 1f);
            });
        }
        else
        {
            GameManager.gamblers.NextGambler();
        }
    }

    private void HandleRacerCardClick(Racer racer, Transform transform)
    {
        cardBetInput.gameObject.SetActive(true);
        cardBetInput.Set(racer);
        cardBetInput.transform.position = transform.position + Vector3.down * 230;
    }
}