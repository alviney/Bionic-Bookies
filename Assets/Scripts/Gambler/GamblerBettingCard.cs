using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamblerBettingCard : MonoBehaviour
{
    public TextMeshProUGUI gamblerName;
    public TextMeshProUGUI gamblerRank;
    public TextMeshProUGUI gamblerCash;
    public Image readyStatus;
    public Sprite readySprite;
    public Sprite notReadySprite;
    private Gambler gambler;

    private void OnEnable()
    {
        int index = this.transform.GetSiblingIndex();
        gambler = GameManager.gamblers.allByStanding[index];
        if (gambler != null)
        {
            gambler.OnStatusChanged += SetReadyStatus;
            gamblerName.text = gambler.name;
            gamblerRank.text = (index + 1).ToString();
            gamblerCash.text = gambler.cashString;
        }
    }

    private void OnDisable()
    {
        gambler.OnStatusChanged -= SetReadyStatus;
    }

    private void SetReadyStatus(GamblerStatus status)
    {
        if (status == GamblerStatus.Ready)
        {
            readyStatus.sprite = readySprite;
        }
        else if (status == GamblerStatus.NotReady)
        {
            readyStatus.sprite = notReadySprite;
        }
    }
}