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
        gambler = GameManager.gamblers.activeGambler;
        gambler.OnStatsChanged += UpdateUI;

        GameManager.gamblers.OnActiveGamblerChanged += OnActiveGamblerChanged;
        UpdateUI();
    }

    private void OnDisable()
    {
        gambler.OnStatsChanged -= UpdateUI;
        GameManager.gamblers.OnActiveGamblerChanged -= OnActiveGamblerChanged;
    }

    private void OnActiveGamblerChanged(Gambler gambler)
    {
        this.gambler.OnStatsChanged -= UpdateUI;
        gambler.OnStatsChanged += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI()
    {
        activeGambler.text = GameManager.gamblers.activeGambler.name;
        cashText.text = Dollarify(GameManager.gamblers.activeGambler.cash);
        debtText.text = Dollarify(GameManager.gamblers.activeGambler.debt);
        spendText.text = "";
    }

    private string Dollarify(int value)
    {
        return "$" + value.ToString();
    }
}
