using UnityEngine;

public class TrackAreaModifier : MonoBehaviour
{
    public float value = 0f;
    public Vector2 xRange;

    private void Awake()
    {
        this.transform.localPosition = new Vector3(Random.Range(xRange.x, xRange.y), this.transform.localPosition.y, this.transform.localPosition.z);
    }

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
