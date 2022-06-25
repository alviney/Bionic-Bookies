using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RacerBettingCard : MonoBehaviour
{
    public Action<Racer, Transform> OnClickHandler;
    public TextMeshProUGUI racerName;
    public Animator animator;

    public Image hair;
    public Image body;
    public RacerPresetsSO racerPresets;

    public Racer racer;

    private void OnEnable()
    {
        this.racer = Store.racers[this.transform.GetSiblingIndex()];
        racerName.text = this.racer?.name;
        this.hair.sprite = racerPresets.hairSprites[racer.hair].front;
        this.hair.color = racerPresets.hairColors[racer.hairColor];
        this.body.color = racerPresets.bodyColors[racer.bodyColor];

        Invoke("StartAnimation", UnityEngine.Random.Range(0, 0.3f));


        if (this.transform.GetSiblingIndex() == 2)
        {
            Invoke("OnClick", 0.1f);
        }
    }

    private void StartAnimation()
    {
        animator.enabled = true;
    }

    public void OnClick()
    {
        this.OnClickHandler?.Invoke(racer, this.transform);
    }
}
