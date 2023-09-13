using Kuhpik;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUIScreen : UIScreen
{
    [SerializeField, BoxGroup("Button")] Button soundButton;

    [SerializeField, BoxGroup("Image")] Image soundImage;

    [SerializeField, BoxGroup("Sprite")] List<Sprite> soundSpriteList;

    public Button SoundButton => soundButton;
    public Image SoundImage => soundImage;
    public List<Sprite> SoundSpriteList => soundSpriteList;
}