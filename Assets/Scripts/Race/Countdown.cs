using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Countdown : MonoBehaviour
{
    private TextMeshProUGUI countdownText;
    public AudioSource numberAudioSource;
    public AudioSource finishedAudioSource;

    private void Awake()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        countdownText.DOFade(1, 0);
        DOTween.Sequence()
            .Append(Tween("3", numberAudioSource))
            .Append(Tween("2", numberAudioSource))
            .Append(Tween("1", numberAudioSource))
            .Append(Tween("Go!", finishedAudioSource))
            .Join(countdownText.DOFade(0, 1));
    }

    private Tween Tween(string text, AudioSource audioSource, float duration = 1)
    {
        Sequence sequence = DOTween.Sequence()
            .AppendCallback(() =>
            {
                audioSource.Play();
                countdownText.text = text;
            })
            .Append(countdownText.transform.DOScale(Vector3.zero, 0))
            .Append(countdownText.transform.DOScale(Vector3.one, duration));


        return sequence;
    }
}
