using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccusationModal : MonoBehaviour
{
    public TextMeshProUGUI racerNameText;
    public Transform content;
    public GameObject gamblerPrefab;

    public void Present(Racer racer)
    {
        racerNameText.text = racer.name;
        PopulateGamblers();
    }

    private void PopulateGamblers()
    {
        Store.gamblers.ForEach(g =>
        {
            GameObject instance = Instantiate(gamblerPrefab, content);
            instance.GetComponent<AccusationGambler>().SetGambler(g);
        });
    }

    private void HandleGamblerClick(Gambler gambler)
    {

    }

    public void OnClose()
    {
        Destroy(this.gameObject);
    }
}
