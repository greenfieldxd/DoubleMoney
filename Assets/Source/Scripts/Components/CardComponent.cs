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

        public void Init(CardConfig config)
        {
            
        }
    }
}