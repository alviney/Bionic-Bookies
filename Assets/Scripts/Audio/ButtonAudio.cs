using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class ButtonAudio : MonoBehaviour, IPointerClickHandler, ISubmitHandler, ISelectHandler
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void OnSelect(BaseEventData baseEventData)
    {
        if (!baseEventData.used && button.interactable)
        {
            baseEventData.Use();
            // AudioManager.instance.PlayUIHoverEvent();
        }
    }

    // Submit states
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!pointerEventData.used && button.interactable)
        {
            pointerEventData.Use();
            AudioManager.instance.PlayAudio(AudioKey.UIClick);
        }
    }

    public void OnSubmit(BaseEventData baseEventData)
    {
        if (!baseEventData.used && button.interactable)
        {
            baseEventData.Use();
            AudioManager.instance.PlayAudio(AudioKey.UIClick);
        }
    }
}

