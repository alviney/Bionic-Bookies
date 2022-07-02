using System;
using UnityEngine;
using TMPro;

public class TamperModalItem : MonoBehaviour
{
    public Action<TamperModalItem> OnSelect;
    public TextMeshProUGUI costText;
    public int cost;
    [SerializeField]
    public RacerModifier tamper;

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        costText.text = "$" + cost.ToString();
    }

    public void Select()
    {
        this.OnSelect?.Invoke(this);
    }
}
