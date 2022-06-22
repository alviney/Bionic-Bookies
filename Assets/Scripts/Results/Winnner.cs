using UnityEngine;
using TMPro;


public class Winnner : MonoBehaviour
{
    public TextMeshProUGUI standing;
    public TextMeshProUGUI gamblerName;
    public TextMeshProUGUI winnings;

    public void Set(string name, string winnings, string standing = "")
    {
        this.standing.gameObject.SetActive(standing != "");
        this.standing.text = standing;

        gamblerName.text = name;
        this.winnings.text = winnings;
    }
}
