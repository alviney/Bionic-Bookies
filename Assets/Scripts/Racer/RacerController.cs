using UnityEngine;

public class RacerController : MonoBehaviour
{
    public Racer racer;
    private bool isRacing = false;

    public void Initialise(Racer racer)
    {
        this.racer = racer;
        this.racer.OnRace += Race;
    }

    private void Race()
    {
        this.isRacing = true;
    }

    private void Update()
    {
        if (this.racer != null && isRacing)
        {
            this.transform.localPosition += new Vector3(this.racer.speed * Time.deltaTime * 0.5f, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FinishLine"))
        {
            isRacing = false;
        }
    }
}
