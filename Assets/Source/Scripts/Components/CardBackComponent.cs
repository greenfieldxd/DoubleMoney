using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBackComponent : MonoBehaviour
{
    [SerializeField, BoxGroup("GameObject")] GameObject activePanel;
    [SerializeField, BoxGroup("GameObject")] GameObject closePanel;

    [SerializeField, BoxGroup("Button")] Button cardButton;
    [SerializeField, BoxGroup("Button")] Button buyButton;
    [SerializeField, BoxGroup("Button")] Button adButton;

    [SerializeField, BoxGroup("Image")] Image iconImage;

    [SerializeField, BoxGroup("Text")] TMP_Text priceText;

    public GameObject ActivePanel => activePanel;
    public GameObject ClosePanel => closePanel;
    public Button CardButton => cardButton;
    public Button BuyButton => buyButton;
    public Button AdButton => adButton;
    public Image IconImage => iconImage;
    public TMP_Text PriceText => priceText;
}