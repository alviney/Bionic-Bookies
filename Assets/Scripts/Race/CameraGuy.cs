using UnityEngine;
using DG.Tweening;

public class CameraGuy : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public float endX;
    public SpriteRenderer flash;
    private bool isRunning;
    private Transform target;
    private bool tweenRunning = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target && transform.position.x < endX)
        {
            animator.SetBool("isRunning", true);
            this.transform.localPosition += new Vector3(1f * Time.deltaTime, 0, 0);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Racer"))
        {
            if (!tweenRunning)
            {
                tweenRunning = true;
                DOTween.Sequence()
                    .SetLoops(-1)
                    .Append(flash.transform.DORotate(new Vector3(0, 0, Random.Range(-180, 180)), 0))
                    .Append(flash.DOFade(1, 0.1f))
                    .Append(flash.DOFade(0, 0.5f))
                    .AppendInterval(Random.Range(2, 4));
            }

            isRunning = true;
            target = other.gameObject.transform;
        }
    }
}
