using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsEntry : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI nameText;
    public Color selectedColor;
    public Color defaultColor;

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetSelected(bool isSelected)
    {
        image.color = isSelected ? selectedColor : defaultColor;
    }
}
