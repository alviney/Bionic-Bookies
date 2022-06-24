using UnityEngine;
using UnityEngine.Events;

public class FinishLine : MonoBehaviour
{
    public Sprite unbrokenSprite;
    public Sprite[] brokenSprites;
    public SpriteRenderer spriteRenderer;
    public UnityEvent OnLineBrake;
    public int unbrokenOrder;
    public int brokenOrder;
    private bool isBroken = false;

    private void OnEnable()
    {
        spriteRenderer.sprite = unbrokenSprite;
        spriteRenderer.sortingOrder = unbrokenOrder;
        isBroken = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Racer"))
        {
            if (!isBroken)
            {
                OnLineBrake.Invoke();
                spriteRenderer.sprite = brokenSprites[other.transform.parent.GetSiblingIndex()];
                spriteRenderer.sortingOrder = brokenOrder;
                isBroken = true;
            }
            Store.activeRace.AddRacerToFinished(other.GetComponent<RacerController>()?.racer);
        }
    }
}
