using UnityEngine;
using TMPro;


public class BetEntry : MonoBehaviour
{
    public TextMeshProUGUI gamblerName;
    public TextMeshProUGUI winnings;

    public void Set(string gamblerName, string winnings)
    {
        this.gamblerName.text = gamblerName;
        this.winnings.text = winnings;
    }
}
