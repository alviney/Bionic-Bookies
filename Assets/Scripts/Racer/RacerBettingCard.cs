using System;
using UnityEngine;
using TMPro;

public class RacerBettingCard : MonoBehaviour
{
    public Action<Racer, Transform> OnClickHandler;
    public TextMeshProUGUI racerName;
    public Racer racer;

    private void OnEnable()
    {
        this.racer = GameManager.racers.all[this.transform.GetSiblingIndex()];
        racerName.text = this.racer?.name;

        if (this.transform.GetSiblingIndex() == 2)
        {
            Invoke("OnClick", 0.1f);
        }
    }

    public void OnClick()
    {
        this.OnClickHandler?.Invoke(racer, this.transform);
    }
}
