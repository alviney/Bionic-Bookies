using UnityEngine;
using UnityEngine.Events;
using System;

public class TamperModal : MonoBehaviour
{
    public UnityEvent<Racer> OnRacerAdded;
    public Action<RacerModifier> OnAddModifier;
    public Transform content;
    private Racer racer;
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

    public void Present(Racer racer)
    {
        this.racer = racer;
        this.OnRacerAdded.Invoke(racer);
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
