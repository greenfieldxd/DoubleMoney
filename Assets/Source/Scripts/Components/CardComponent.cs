using NaughtyAttributes;
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

        private CardConfig _config;

        public CardConfig Config => _config;
        public bool IsAvailable => isAvailable;

        public void Init(CardConfig config, Sprite sprite)
        {
            _config = config;
            text.text = config.GetCardText();
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