using UnityEngine;
using TMPro;

public class AccusationGambler : MonoBehaviour
{
    public TextMeshProUGUI gamblerNameText;

    public void SetGambler(Gambler gambler)
    {
        gamblerNameText.text = gambler.name;
    }
}
