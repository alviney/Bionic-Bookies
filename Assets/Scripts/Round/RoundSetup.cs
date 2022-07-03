using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;


public class RoundSetup : MonoBehaviour
{
    public TextMeshProUGUI roundTitle;
    public TextMeshProUGUI betTotal;
    public Button readyButton;
    public TextMeshProUGUI notEnoughCashText;
    public GameObject tamperModalPrefab;

    // Racers
    public GameObject racerBettingCardPrefab;
    public Transform racerBettingCardParent;

    // Gamblers
    public GameObject gamblerBettingCardPrefab;
    public Transform gamblerBettingCardParent;

    // Racing Cards
    private List<RacerBettingCard> cards = new List<RacerBettingCard>();

    public Vector3 betInputOffset = Vector3.zero;
    public UnityEvent OnAllGamblersReady;
    private List<RacerModifier> modifiers = new List<RacerModifier>();

    private void OnEnable()
    {
        roundTitle.text = "Race " + Store.race.raceNumber.ToString();
        betTotal.text = "$0";

        modifiers.Clear();

        PopulateBettingCards();
    }

    private void OnDisable()
    {
        foreach (RacerBettingCard card in cards)
        {
            card.OnClickHandler -= HandleRacerCardClick;
            card.OnBetUpdated -= HandleBetUpdate;

            foreach (Transform child in racerBettingCardParent) Destroy(child.gameObject);
            foreach (Transform child in gamblerBettingCardParent) Destroy(child.gameObject);
        }
    }

    private void PopulateBettingCards()
    {
        cards.Clear();

        foreach (Racer racer in Store.racers)
        {
            GameObject instance = Instantiate(racerBettingCardPrefab, racerBettingCardParent);
            RacerBettingCard card = instance.GetComponent<RacerBettingCard>();
            cards.Add(card);
            card.OnClickHandler += HandleRacerCardClick;
            card.OnBetUpdated += HandleBetUpdate;
            card.OnTamperCick += HandleTamperClick;
        }

        foreach (Gambler gambler in Store.gamblersByStanding)
        {
            Instantiate(gamblerBettingCardPrefab, gamblerBettingCardParent);
            if (!gambler.human)
            {
                // DOVirtual.DelayedCall(Random.Range(0.5f, 1.5f), () =>
                // {
                // });

                gambler.UpdateStatus(GamblerStatus.Ready);
            }
        }
    }

    public void OnReady()
    {
        Store.activeGambler.UpdateStatus(GamblerStatus.Ready);
        GamblerSubmission submission = new GamblerSubmission(Store.activeGambler.name, GetBets(), modifiers);
        SessionManager.instance.PostLobbyMemberDataUpdate(GamblerSubmission.JsonKey, submission.ToJson);
        SessionManager.instance.PostLobbyMemberDataUpdate("status", GamblerStatus.Ready.ToString());
    }

    private List<Bet> GetBets()
    {
        List<Bet> bets = new List<Bet>();
        foreach (RacerBettingCard card in cards)
        {
            if (card.betValue > 0)
            {
                Bet bet = new Bet(Store.activeGambler.name, card.racer.name, (int)card.betValue, 2);
                bet.Lock();
                bets.Add(bet);
                // SessionManager.instance.PlaceBet(bet);
            }
        }

        return bets;
    }

    private void OnAddModifier(RacerModifier racerModifier)
    {
        this.modifiers.Add(racerModifier);
    }

    private void HandleTamperClick(RacerBettingCard card)
    {
        GameObject instance = Instantiate(tamperModalPrefab, SessionManager.instance.modalContainer);
        TamperModal modal = instance.GetComponent<TamperModal>();
        modal.Present(card.racer);
        modal.OnAddModifier += OnAddModifier;
    }

    private void HandleBetUpdate(RacerBettingCard card)
    {
        float total = 0;
        foreach (RacerBettingCard _card in cards)
        {
            total += _card.betValue;
        }

        bool valid = total <= Store.activeGambler.cash;
        readyButton.interactable = valid;
        notEnoughCashText.enabled = !valid;

        betTotal.text = "$" + total.ToString();
    }

    private void HandleRacerCardClick(RacerBettingCard card)
    {
        foreach (RacerBettingCard _card in cards)
        {
            if (_card != card) _card.HideInput();
        }
        card.ShowInput();
    }
}
