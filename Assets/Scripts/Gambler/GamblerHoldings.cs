using UnityEngine;
using TMPro;

public class GamblerHoldings : MonoBehaviour
{
    public TextMeshProUGUI activeGambler;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI debtText;
    public TextMeshProUGUI spendText;
    private Gambler gambler;

    private void OnEnable()
    {
        gambler = Store.activeGambler;
        gambler.OnStatsChanged += UpdateUI;

        SessionManager.instance.OnActiveGamblerChanged += OnActiveGamblerChanged;
        UpdateUI();
    }

    private void OnDisable()
    {
        gambler.OnStatsChanged -= UpdateUI;
        SessionManager.instance.OnActiveGamblerChanged -= OnActiveGamblerChanged;
    }

    private void OnActiveGamblerChanged(Gambler gambler)
    {
        this.gambler.OnStatsChanged -= UpdateUI;
        gambler.OnStatsChanged += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI()
    {
        activeGambler.text = Store.activeGambler.name;
        cashText.text = Dollarify(Store.activeGambler.cash);
        debtText.text = Dollarify(Store.activeGambler.debt);
        spendText.text = "";
    }

    private string Dollarify(int value)
    {
        return "$" + value.ToString();
    }
}
