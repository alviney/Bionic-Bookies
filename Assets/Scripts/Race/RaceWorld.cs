using UnityEngine;


public class RaceWorld : MonoBehaviour
{
    public Transform trackPositionsParent;
    public GameObject racerPrefab;

    private void Awake()
    {
        SpawnRacers();
    }

    private void SpawnRacers()
    {
        int i = 0;
        foreach (Racer racer in Store.racers)
        {
            GameObject instance = Instantiate(racerPrefab, trackPositionsParent.GetChild(i));
            instance.transform.localPosition = Vector3.zero;
            RacerController controller = instance.GetComponent<RacerController>();
            controller.Initialise(racer);
            i++;
        }
    }
}
