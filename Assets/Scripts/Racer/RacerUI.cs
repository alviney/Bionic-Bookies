using UnityEngine;
using UnityEngine.UI;

public class RacerUI : MonoBehaviour
{
    public Animator animator;

    public Image hair;
    public Image body;
    public RacerPresetsSO racerPresets;

    public void Present(Racer racer)
    {
        this.hair.sprite = racerPresets.hairSprites[racer.hair].front;
        this.hair.color = racerPresets.hairColors[racer.hairColor];
        this.body.color = racerPresets.bodyColors[racer.bodyColor];
    }
}
