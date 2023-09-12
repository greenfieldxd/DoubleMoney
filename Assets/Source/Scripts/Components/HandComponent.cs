using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class HandComponent : MonoBehaviour
    {
        [field:SerializeField] public TurnType TurnType { get; private set; }
        [field:SerializeField] public Animator Animator { get; private set; }
    }
}