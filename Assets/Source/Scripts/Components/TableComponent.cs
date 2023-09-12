using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class TableComponent : MonoBehaviour
    {
        [field:SerializeField] public CardComponent Card { get; private set; }
        [field:SerializeField] public Transform DeckPosition { get; private set; }
        [field:SerializeField] public Transform BoardPosition { get; private set; }
        [field:SerializeField] public TextMeshProUGUI MyTextField { get; private set; }
        [field:SerializeField] public TextMeshProUGUI OpponentTextField { get; private set; }
        [field:SerializeField] public StackComponent MyStack { get; private set; }
        [field:SerializeField] public StackComponent OpponentStack { get; private set; }
        [field:SerializeField] public HandComponent[] Hands { get; private set; }

        [Button]
        public void ClearBoard()
        {
            var cards = GetComponentsInChildren<CardComponent>();

            foreach (var card in cards)
            {
                DestroyImmediate(card.gameObject);
            }
        } 
        
        [Button]
        public void CreateCards()
        {
            var boardPositions = GetComponentsInChildren<BoardPointComponent>();

            foreach (var position in boardPositions)
            {
                var card = Instantiate(Card, position.transform);
                card.transform.localPosition = Vector3.zero;
                card.transform.localRotation = Quaternion.Euler(0,0,0);
            }
        }
    }
}