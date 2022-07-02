using UnityEngine;

public enum RacerModifierType { Percentage, Absolute }
[System.Serializable]
public class RacerModifier
{
    public RacerModifier(Racer racer, RacerStat stat, float value, RacerModifierType type = RacerModifierType.Percentage)
    {
        this.racer = racer;
        this.stat = stat;
        this.value = value;
        this.type = type;
    }

    [SerializeField]
    public Racer racer;
    [SerializeField]
    public RacerStat stat;
    [SerializeField]
    public float value;
    [SerializeField]
    public RacerModifierType type;
}