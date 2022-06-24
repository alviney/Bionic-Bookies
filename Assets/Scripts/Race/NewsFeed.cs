using UnityEngine;
using TMPro;

public class NewsFeed : MonoBehaviour
{
    public TextMeshProUGUI textOne;
    public TextMeshProUGUI textTwo;
    public Vector3 startPos;
    public float speed;
    public float moveTo;

    private void OnEnable()
    {
    }

    private void Update()
    {

        textOne.transform.localPosition += Vector3.right * speed * Time.deltaTime;
        if (textOne.transform.localPosition.x > moveTo)
        {
            textOne.transform.localPosition = startPos;
        }

        textTwo.transform.localPosition += Vector3.right * speed * Time.deltaTime;
        if (textTwo.transform.localPosition.x > moveTo)
        {
            textTwo.transform.localPosition = startPos;
        }

    }
}
