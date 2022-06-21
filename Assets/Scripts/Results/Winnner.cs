using UnityEngine;
using TMPro;


public class Winnner : MonoBehaviour
{
    public TextMeshProUGUI gamblerName;
    public TextMeshProUGUI winnings;

    public void Set(string name, string winnings)
    {
        gamblerName.text = name;
        this.winnings.text = winnings;
    }
}
