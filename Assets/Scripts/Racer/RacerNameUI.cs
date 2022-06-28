using UnityEngine;
using TMPro;

public class RacerNameUI : MonoBehaviour
{
    public TextMeshProUGUI racerNameText;

    public void Present(Racer racer)
    {
        racerNameText.text = racer.name;
    }
}
