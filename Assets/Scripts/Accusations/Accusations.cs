using UnityEngine;
using UnityEngine.Events;

public class Accusations : MonoBehaviour
{
    public UnityEvent OnSessionFinished;

    public void OnNext()
    {
        if (Store.session.isFinished)
        {
            this.OnSessionFinished.Invoke();
        }
        else
        {
            SessionManager.instance.NextState();
        }
    }
}
