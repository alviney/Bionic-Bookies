using UnityEngine;
using TMPro;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class TrafficLight : MonoBehaviour
{
    public Sprite grey;
    public Sprite red;
    public Sprite green;
    public SpriteRenderer[] spriteRenderers;


    private void OnEnable()
    {
        DOTween.Sequence()
            .AppendCallback(() =>
            {
                spriteRenderers[0].sprite = red;
                spriteRenderers[1].sprite = grey;
                spriteRenderers[2].sprite = grey;
            })
            .AppendInterval(1)
            .AppendCallback(() =>
            {
                spriteRenderers[0].sprite = red;
                spriteRenderers[1].sprite = red;
                spriteRenderers[2].sprite = grey;
            })
            .AppendInterval(1)
            .AppendCallback(() =>
            {
                spriteRenderers[0].sprite = red;
                spriteRenderers[1].sprite = red;
                spriteRenderers[2].sprite = red;
            })
            .AppendInterval(1)
            .AppendCallback(() =>
            {
                spriteRenderers[0].sprite = green;
                spriteRenderers[1].sprite = green;
                spriteRenderers[2].sprite = green;
            });
    }
}
