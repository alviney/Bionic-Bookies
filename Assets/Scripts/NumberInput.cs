using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class NumberInput : MonoBehaviour
{
    public UnityEvent<int> OnValueChange;
    public TextMeshProUGUI displayValue;
    public Button downButton;
    public Button upButton;
    public string prefix;
    public int min = 0;
    public int max = 100;
    public int step = 10;
    public int value = 0;

    private void OnEnable()
    {
        SetValue(this.value);
    }

    public void Increment()
    {
        if (value < max)
        {
            value += step;
            SetValue(Mathf.Min(value, max));
        }
    }

    public void Decrement()
    {
        if (value > min)
        {
            value -= step;
            SetValue(Mathf.Max(value, min));
        }
    }

    public void SetValue(int value)
    {
        downButton.interactable = value > min;
        upButton.interactable = value < max;

        this.value = Mathf.Clamp(value, min, max);
        this.OnValueChange.Invoke(this.value);
        UpdateDisplay(this.value);
    }

    public void SetRange(int min, int max)
    {
        this.min = min;
        this.max = max;
        SetValue(this.value);
    }

    private void UpdateDisplay(int value)
    {
        displayValue.text = prefix + value.ToString();
    }
}
