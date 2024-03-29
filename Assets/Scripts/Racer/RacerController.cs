using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using DG.Tweening;

public class RacerController : MonoBehaviour
{
    public Racer racer;
    public Animator animator;
    public TextMeshProUGUI nameText;
    public SpriteRenderer hair;
    public SpriteRenderer body;
    public RacerPresetsSO racerPresets;
    private bool isRacing = false;
    private bool isAccelerating = false;
    private float speed;
    public List<float> modifiers = new List<float>();

    public void Initialise(Racer racer)
    {
        this.racer = racer;
        this.racer.OnRace += Race;
        nameText.text = racer.name;
        this.hair.sprite = racerPresets.hairSprites[racer.hair].side;
        this.hair.color = racerPresets.hairColors[racer.hairColor];
        this.body.color = racerPresets.bodyColors[racer.bodyColor];
    }

    private void OnDestroy()
    {
        this.racer.OnRace -= Race;
    }

    private void Race()
    {
        this.isRacing = true;
        animator.SetBool("isRunning", true);
        animator.SetBool("isAccelerating", true);
    }

    private void Update()
    {
        if (this.racer != null && isRacing)
        {
            speed += racer.acceleration.GetValue() * Time.deltaTime;
            speed = Mathf.Min(racer.speed.GetValue() - speedModifier, speed);
            this.transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);

            animator.SetBool("isAccelerating", speed < racer.speed.GetValue());
        }
    }

    private float speedModifier
    {
        get => modifiers.Sum();
    }

    public void AddModifier(float value)
    {
        modifiers.Add(value);
    }

    public void RemoveModifier(float value)
    {
        modifiers.Remove(value);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FinishLine"))
        {
            DOVirtual.DelayedCall(Random.Range(1, 2.5f), () =>
            {
                isRacing = false;
                animator.SetBool("isRunning", false);
            });
        }
    }
}
