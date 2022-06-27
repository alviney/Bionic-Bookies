using UnityEngine;
using TMPro;

public class ModifiersList : MonoBehaviour
{
    public GameObject textPrefab;

    public void PopulateList(Racer racer)
    {
        foreach (Transform child in transform) Destroy(child.gameObject);

        foreach (float value in racer.speed.modifiers)
        {
            TextMeshProUGUI text = Instantiate(textPrefab, this.transform).GetComponent<TextMeshProUGUI>();
            text.text = ConvertToString(value) + " speed";
        }

        foreach (float value in racer.acceleration.modifiers)
        {
            TextMeshProUGUI text = Instantiate(textPrefab, this.transform).GetComponent<TextMeshProUGUI>();
            text.text = ConvertToString(value) + " acceleration";
        }
    }

    public string ConvertToString(float value)
    {
        return (Mathf.Sign(value) < 0 ? "-" : "+") + Mathf.Abs(value).ToString();
    }
}
