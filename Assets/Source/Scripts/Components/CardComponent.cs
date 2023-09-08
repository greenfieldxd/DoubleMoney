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

        [SerializeField, BoxGroup("Debug"), ReadOnly] bool isAvailable;

        private CardConfig _config;

        public CardConfig Config => _config;
        public bool IsAvailable => isAvailable;

        public void Init(CardConfig config)
        {
            _config = config;
            text.text = config.GetCardText();
            text.color = config.CardColor;
            meshColor.material.color = config.CardColor;
        }
        public void SetAvailable(bool status)
        {
            isAvailable = status;
        }
    }
}