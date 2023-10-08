using Kuhpik;
using NaughtyAttributes;
using Source.Scripts.Enums;
using Source.Scripts.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class CardComponent : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private MeshRenderer meshBase;
        [SerializeField] private MeshRenderer meshColor;
        [SerializeField] private SpriteRenderer cardBack;
        [SerializeField] private Color tableColor;

        [SerializeField, BoxGroup("Debug"), ReadOnly] bool isAvailable;


        public CardType CardType { get; private set; }
        public int Value { get; private set; }
        public bool IsAvailable => isAvailable;

        public void Init(CardConfig config, Sprite sprite)
        {
            Value = config.CreateValue(Bootstrap.Instance.PlayerData.winsCount);
            CardType = config.CardType;
            text.text = config.GetCardText(Value);
            text.color = config.CardColor;
            cardBack.sprite = sprite;
            meshColor.material.color = config.CardColor;
            isAvailable = false;
        }
        public void SetAvailable(bool status, bool withColor = false)
        {
            if (withColor) meshBase.material.color = status ? Color.white :tableColor;
            isAvailable = status;
        }
    }
}