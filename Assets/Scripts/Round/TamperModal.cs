using UnityEngine;

public class TamperModal : MonoBehaviour
{
    public Transform content;
    public Racer racer;
    private TamperModalItem[] items;

    private void OnEnable()
    {
        items = content.GetComponentsInChildren<TamperModalItem>();
        AddListeners();
    }

    private void OnDisable()
    {
        RemoveListeners();
        racer = null;
    }

    private void AddListeners()
    {
        foreach (TamperModalItem item in items) item.OnSelect += HandleItemSelect;
    }

    private void RemoveListeners()
    {
        foreach (TamperModalItem item in items) item.OnSelect -= HandleItemSelect;
    }

    private void HandleItemSelect(TamperModalItem item)
    {
        if (racer != null)
        {
            racer.AddTamper(item.tamper);
            Store.activeGambler.UpdateCash(-item.cost);
        }
    }

    public void OnClose()
    {
        Destroy(this.gameObject);
    }
}
