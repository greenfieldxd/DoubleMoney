using System;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class HandComponent : MonoBehaviour
    {
        [field:SerializeField] public TurnType TurnType { get; private set; }
        [field:SerializeField] public Animator Animator { get; private set; }

        public Vector3 StartPosition { get; private set; }
        public Vector3 StartRotation { get; private set; }
        public bool IsMoving { get; set; }
        
        private void Start()
        {
            var tr = transform;
            StartRotation = tr.eulerAngles;
            StartPosition = tr.position;
        }
    }
}