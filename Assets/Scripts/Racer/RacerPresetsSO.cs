using UnityEngine;

[System.Serializable]
public struct InterchangeableSprite
{
    public Sprite front;
    public Sprite side;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RacerPresets", order = 1)]
public class RacerPresetsSO : ScriptableObject
{
    [SerializeField]
    public InterchangeableSprite[] hairSprites;
    public Color[] hairColors;
    [SerializeField]
    public InterchangeableSprite[] bodySprites;
    public Color[] bodyColors;
}
