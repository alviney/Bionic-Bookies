using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RacerBettingCard : MonoBehaviour
{
    public Action<RacerBettingCard> OnClickHandler;
    public Action<RacerBettingCard> OnBetUpdated;
    public Action<RacerBettingCard> OnTamperCick;
    public ModifiersList modifiersList;
    public NumberInput betInput;
    public TextMeshProUGUI oddsText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI betAlreadyPlacedText;
    public float betValue = 0;
    public TextMeshProUGUI racerName;
    public Animator animator;

    public Image hair;
    public Image body;
    public RacerPresetsSO racerPresets;
    public Racer racer;

    private void OnEnable()
    {
        this.racer = Store.racers[this.transform.GetSiblingIndex()];
        racerName.text = this.racer?.name;
        oddsText.text = "1/" + Store.session.GetOdds(Store.activeRace, this.racer).ToString();
        UpdateBetText();

        this.hair.sprite = racerPresets.hairSprites[racer.hair].front;
        this.hair.color = racerPresets.hairColors[racer.hairColor];
        this.body.color = racerPresets.bodyColors[racer.bodyColor];

        betInput.OnValueChange.AddListener(OnInputChanged);

        Invoke("StartAnimation", UnityEngine.Random.Range(0, 0.3f));

        if (this.transform.GetSiblingIndex() == 2)
        {
            Invoke("OnClick", 0.1f);
        }
    }

    private void OnDisable()
    {
        betInput.OnValueChange.RemoveListener(OnInputChanged);
    }

    private void OnInputChanged(int value)
    {
        this.betValue = value;
        OnBetUpdated?.Invoke(this);
    }

    private void UpdateBetText()
    {
        betText.text = betValue == 0 ? "" : "$" + betValue.ToString();
    }

    public void HideInput()
    {
        this.betInput.gameObject.SetActive(false);
        UpdateBetText();
        modifiersList.PopulateList(racer);
    }

    public void ShowInput()
    {
        this.betInput.gameObject.SetActive(true);
        // if (Store.activeGambler != null)
        // {
        //     betInput.SetRange(min, max);
        // }
    }

    public void OnTamper()
    {
        OnTamperCick.Invoke(this);
    }

    private void StartAnimation()
    {
        animator.enabled = true;
    }

    public void OnClick()
    {
        this.OnClickHandler?.Invoke(this);
    }
}
