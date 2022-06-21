using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class LifecycleEvents : MonoBehaviour
{
    public UnityEvent onAwake;
    public UnityEvent onEnable;
    public UnityEvent onDisable;
    public UnityEvent onStart;

    private void Awake()
    {
        onAwake.Invoke();
    }

    private void OnEnable()
    {
        onEnable.Invoke();
    }

    private void OnDisable()
    {
        onDisable.Invoke();
    }

    private void Start()
    {
        onStart.Invoke();
    }

    public void DisableIn(float time)
    {
        DOVirtual.DelayedCall(time, () => enabled = false);
    }

    public void EnableIn(float time)
    {
        DOVirtual.DelayedCall(time, () => enabled = true);
    }
}
