using UnityEngine;

namespace Source.Scripts.Components
{
    public class BoardPositionComponent : MonoBehaviour
    {
        [SerializeField] private BoardPositionComponent[] dependencePositions;
        
        public bool IsEmpty => _card == null;
        
        private CardComponent _card;
        
        public void SetCard(CardComponent card)
        {
            _card = card;
        }
    }
}