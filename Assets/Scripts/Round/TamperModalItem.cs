using System;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct Tamper
{
    public Tamper(RacerStat stat, float value)
    {
        this.stat = stat;
        this.value = value;
    }
    [SerializeField]
    public RacerStat stat;
    [SerializeField]
    public float value;
}

public class TamperModalItem : MonoBehaviour
{
    public Action<TamperModalItem> OnSelect;
    public TextMeshProUGUI costText;
    public int cost;
    [SerializeField]
    public Tamper tamper;

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
