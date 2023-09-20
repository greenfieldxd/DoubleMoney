using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(fileName = "CardBackConfig", menuName = "Config/CardBackConfig")]
public class CardBackConfig : ScriptableObject
{
    [SerializeField, BoxGroup("Settings")] int price;

    [SerializeField, BoxGroup("Sprite")] Sprite sprite;

    public int Price => price;
    public Sprite Sprite => sprite;
}