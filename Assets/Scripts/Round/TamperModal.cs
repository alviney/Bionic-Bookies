using UnityEngine;
using System;

public class TamperModal : MonoBehaviour
{
    public Action<RacerModifier> OnAddModifier;
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
            Store.activeGambler.UpdateCash(-item.cost);
            item.modifier.racerName = racer.name;
            OnAddModifier.Invoke(item.modifier);
        }
    }

    public void OnClose()
    {
        Destroy(this.gameObject);
    }
}
