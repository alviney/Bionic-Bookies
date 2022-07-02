using UnityEngine;

public enum RacerModifierType { Percentage, Absolute }
[System.Serializable]
public class RacerModifier
{
    [SerializeField]
    public string racerName;
    [SerializeField]
    public RacerStat stat;
    [SerializeField]
    public float value;
    [SerializeField]
    public RacerModifierType type;

    public RacerModifier(string racerName, RacerStat stat, float value, RacerModifierType type = RacerModifierType.Percentage)
    {
        this.racerName = racerName;
        this.stat = stat;
        this.value = value;
        this.type = type;
    }

    public void AddToRacer()
    {
        Store.GetRacer(racerName).AddModifier(this);
    }
}