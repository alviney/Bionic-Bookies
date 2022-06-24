using UnityEngine;

public class TrackAreaModifier : MonoBehaviour
{
    public float value = 0f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Racer"))
        {
            other.SendMessage("AddModifier", value);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Racer"))
        {
            other.SendMessage("RemoveModifier", value);
        }
    }
}
