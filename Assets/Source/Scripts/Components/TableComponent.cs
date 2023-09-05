using UnityEngine;

namespace Source.Scripts.Components
{
    public class TableComponent : MonoBehaviour
    {
        [field:SerializeField] public Transform DeckPosition { get; private set; }
        [field:SerializeField] public Transform BoardPosition { get; private set; }
    }
}