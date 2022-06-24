using UnityEngine;
using DG.Tweening;

public class CameraGuy : MonoBehaviour
{
    public float speed;
    private Animator animator;
    public float endX;
    private bool isRunning;
    private Transform target;

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
            isRunning = true;
            target = other.gameObject.transform;
        }
    }
}
