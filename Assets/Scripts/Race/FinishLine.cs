using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Racer"))
        {
            GameManager.race.AddRacerToFinished(other.GetComponent<RacerController>()?.racer);
        }
    }
}
