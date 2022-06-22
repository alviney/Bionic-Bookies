using UnityEngine;

public class Standings : MonoBehaviour
{
    public Transform content;
    public GameObject entryPrefab;

    private void OnEnable()
    {
        foreach (Gambler gambler in Store.gamblersByStanding)
        {
            GameObject instance = Instantiate(entryPrefab, content);
            instance.GetComponent<Winnner>().Set(gambler.name, gambler.cashString, (instance.transform.GetSiblingIndex() + 1).ToString() + ". ");
        }
    }

    private void OnDisable()
    {
        foreach (Transform child in content) Destroy(child.gameObject);
    }
}

